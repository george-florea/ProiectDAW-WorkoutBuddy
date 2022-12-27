using Microsoft.EntityFrameworkCore;
using Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.BusinessLogic.Base;
using Backend.Common.DTOs;
using Backend.Common.Extensions;
using Backend.Entities;
using Backend.Common.Exceptions;
using System.Text.RegularExpressions;
using WorkoutBuddy.Entities.Enums;

namespace Backend.BusinessLogic.Account
{
    public class UserAccountService : BaseService
    {
        private readonly RegisterUserValidator RegisterUserValidator;
        private readonly AddWeightValidator AddWeightValidator;
        private readonly EditProfileValidator EditProfileValidator;
        private readonly EditUserProfileValidator EditUserProfileValidator;

        public UserAccountService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            RegisterUserValidator = new RegisterUserValidator(UnitOfWork);
            AddWeightValidator = new AddWeightValidator();
            EditProfileValidator = new EditProfileValidator(UnitOfWork);
            EditUserProfileValidator = new EditUserProfileValidator(UnitOfWork);
        }

        public async Task<CurrentUserDto> Login(LoginModel model)
        {
            var user = await UnitOfWork.Users
                .Get()
                .Include(u => u.Idroles)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            if (user.IsDeleted == true)
            {
                return new CurrentUserDto { IsDisabled = true };
            }

            if (!user.Password.SequenceEqual(model.Password.HashPassword(user.Salt)))
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            var roles = user.Idroles.Select(r => r.Name).ToList();

            return new CurrentUserDto
            {
                Id = user.Iduser,
                Email = user.Email,
                Name = user.Name,
                Username = user.Username,
                IsAuthenticated = true,
                IsDisabled = user.IsDeleted ?? false,
                Roles = roles,
            };
        }

        public void RegisterNewUser(RegisterModel model)
        {
            ExecuteInTransaction(uow =>
            {
                RegisterUserValidator.Validate(model).ThenThrow(model);
                var userSalt = Guid.NewGuid();

                var user = new User()
                {
                    Iduser = Guid.NewGuid(),
                    Salt = Guid.NewGuid(),
                    Email = model.Email,
                    Name = model.Name,
                    BirthDate = (DateTime)model.BirthDay,
                    Username = model.Username
                };

                //Mapper.Map(model, user);
                user.Password = model.PasswordString.HashPassword(user.Salt);
                var userRole = uow.Roles.Get().FirstOrDefault(r => r.Idrole == (int)RoleTypes.User);
                user.UserWeightHistories.Add(new UserWeightHistory()
                {
                    Iduser = user.Iduser,
                    WeighingDate = DateTime.UtcNow,
                    Weight = (double)model.Weight,
                    IduserNavigation = user,
                });

                user.LastLoginDate = DateTime.Now;

                //var adminRole = uow.Roles.Get().FirstOrDefault(r => r.Idrole == (int)RoleTypes.Admin);
                user.Idroles.Add(userRole);

                //user.Idroles.Add(adminRole);

                uow.Users.Insert(user);

                uow.SaveChanges();
            });
        }

        /*
                public CurrentUserDto Login(string email, string password)
                {
                    var currentUser = new CurrentUserDto();
                    ExecuteInTransaction(uow =>
                    {
                        var user = UnitOfWork.Users.Get()
                        .FirstOrDefault(u => u.Email == email);

                        if (user == null)
                        {
                            currentUser = new CurrentUserDto { IsAuthenticated = false };
                            return;
                        }

                        if (user.IsDeleted == true)
                        {
                            currentUser = new CurrentUserDto { IsDisabled = true };
                            return;
                        }

                        var passwordHash = password.HashPassword(user.Salt);
                        if (!passwordHash.SequenceEqual(user.Password))
                        {
                            currentUser = new CurrentUserDto { IsAuthenticated = false };
                            return;
                        }

                        var userRoles = UnitOfWork.Users.Get()
                                        .Where(u => u.Iduser == user.Iduser)
                                        .SelectMany(u => u.Idroles.Select(r => r.Name))
                                        .ToList();

                        user.LastLoginDate = DateTime.Now;


                        uow.Users.Update(user);
                        uow.SaveChanges();

                        currentUser = new CurrentUserDto
                        {
                            Id = user.Iduser,
                            Email = user.Email,
                            Name = user.Name,
                            Username = user.Username,
                            IsAuthenticated = true,
                            IsDisabled = user.IsDeleted ?? false,
                            Roles = userRoles,
                        };
                    });
                    return currentUser;
                }*/
        /*
                public void RegisterNewUser(RegisterModel model)
                {
                    ExecuteInTransaction(uow =>
                    {
                        RegisterUserValidator.Validate(model).ThenThrow(model);
                        var userSalt = Guid.NewGuid();

                        var user = Mapper.Map<RegisterModel, User>(model);
                        user.Password = model.PasswordString.HashPassword(user.Salt);
                        var userRole = uow.Roles.Get().FirstOrDefault(r => r.Idrole == (int)RoleTypes.User);
                        user.UserWeightHistories.Add(new UserWeightHistory()
                        {
                            Iduser = user.Iduser,
                            WeighingDate = DateTime.UtcNow,
                            Weight = (double)model.Weight,
                            IduserNavigation = user,
                        });

                        user.LastLoginDate = DateTime.Now;

                        //var adminRole = uow.Roles.Get().FirstOrDefault(r => r.Idrole == (int)RoleTypes.Admin);
                        user.Idroles.Add(userRole);

                        //user.Idroles.Add(adminRole);

                        uow.Users.Insert(user);
                        // trigger mail notifi
                        // insert audit 

                        uow.SaveChanges();
                    });
                }

                public List<ListItemModel<string, Guid>> GetUsers()
                {
                    return UnitOfWork.Users.Get()
                        .Select(u => new ListItemModel<string, Guid>
                        {
                            Text = $"{u.Name}",
                            Value = u.Iduser
                        })
                        .ToList();
                }

                public bool IsUserOfTheWeek()
                {
                    var user = UnitOfWork.Users.Get()
                                .Include(u => u.Idroles)
                                .FirstOrDefault(u => u.Iduser == CurrentUser.Id);
                    var userRoles = user.Idroles
                                    .Select(r => r.Idrole)
                                    .ToList();
                    if (userRoles.Contains((int)RoleTypes.UserOfTheWeek) && !CurrentUser.Roles.Contains(RoleTypes.UserOfTheWeek.ToString()))
                    {
                        return true;
                    }
                    return false;
                }

                public bool ChangeAvailability(Guid id, bool isDeleted)
                {
                    var isAvailable = true;
                    ExecuteInTransaction(uow =>
                    {
                        var user = uow.Users.Get()
                                    .FirstOrDefault(u => u.Iduser == id);

                        if (user == null)
                        {
                            throw new NotFoundErrorException("this user does not exist!");
                        }

                        user.IsDeleted = isDeleted;
                        user.LastModifiedOn = DateTime.Now;
                        try
                        {
                            uow.Users.Update(user);

                            uow.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            isAvailable = false;
                        }
                    });
                    return isAvailable;
                }


                public bool VerifyOldPassword(string password)
                {
                    var user = UnitOfWork.Users.Get()
                        .FirstOrDefault(u => u.Iduser == CurrentUser.Id);

                    var passwordHash = password.HashPassword(user.Salt);

                    return passwordHash.SequenceEqual(user.Password);
                }

                public bool ChangePassword(PasswordChangeModel model)
                {
                    var isValid = true;

                    ExecuteInTransaction(uow =>
                    {
                        var user = uow.Users.Get()
                            .FirstOrDefault(u => u.Iduser == CurrentUser.Id);

                        var oldPasswordHash = model.OldPassword.HashPassword(user.Salt);

                        if (!PasswordRegexTest(model.OldPassword))
                        {
                            isValid = false;
                        }
                        else if (!PasswordRegexTest(model.NewPassword) || !oldPasswordHash.SequenceEqual(user.Password))
                        {
                            isValid = false;
                        }
                        else
                        {
                            var passwordHash = model.NewPassword.HashPassword(user.Salt);
                            user.Password = passwordHash;

                            uow.Users.Update(user);
                            uow.SaveChanges();
                        }

                    });
                    return isValid;
                }

                private bool PasswordRegexTest(string password)
                {
                    if (password == null)
                    {
                        return false;
                    }
                    var pattern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}";
                    Regex re = new Regex(pattern);
                    var res = re.IsMatch(password);
                    return res;
                }

                public void AddWeight(AddWeightModel model)
                {
                    ExecuteInTransaction(uow =>
                    {
                        var validationRes = AddWeightValidator.Validate(model);
                        if (!validationRes.IsValid)
                        {
                            var throwModel = GetWeightHistory(CurrentUser.Id);
                            validationRes.ThenThrow(throwModel);
                        }


                        var user = uow.Users.Get().FirstOrDefault(u => u.Iduser == model.UserId);
                        var weight = Mapper.Map<AddWeightModel, UserWeightHistory>(model);
                        weight.IduserNavigation = user;
                        uow.UserWeightHistorys.Insert(weight);
                        uow.SaveChanges();
                    });
                }

                public UserInfoModel GetUserInfo(Guid id)
                {
                    var user = UnitOfWork.Users.Get()
                                .Include(u => u.UserPointsHistories)
                                .Include(u => u.UserWeightHistories)
                                .Include(u => u.Idroles)
                                .FirstOrDefault(u => u.Iduser == id);

                    var userInfo = Mapper.Map<User, UserInfoModel>(user);
                    userInfo.CurrentWeight = (float)user.UserWeightHistories
                                                .OrderByDescending(u => u.WeighingDate)
                                                .FirstOrDefault(u => u.Iduser == user.Iduser)
                                                .Weight;

                    return userInfo;
                }

                public AddWeightModel GetWeightHistory(Guid userId)
                {
                    var user = UnitOfWork.Users.Get()
                        .Include(u => u.UserWeightHistories)
                        .FirstOrDefault(u => u.Iduser == userId);

                    var model = new AddWeightModel()
                    {
                        History = new List<WeightHistoryModel>()
                    };

                    if (user == null)
                    {
                        return model;
                    }

                    var historyRecords = user.UserWeightHistories.Take(10).ToList();
                    foreach (var history in historyRecords)
                    {
                        model.History.Add(new WeightHistoryModel()
                        {
                            Date = history.WeighingDate,
                            Weight = history.Weight
                        });
                    }
                    return model;
                }

                public EditProfileModel GetEditModel()
                {
                    var user = UnitOfWork.Users.Get()
                                .First(u => u.Iduser == CurrentUser.Id);

                    var model = Mapper.Map<User, EditProfileModel>(user);
                    return model;
                }

                public string EditProfile(EditProfileModel model)
                {
                    var newName = CurrentUser.Username;
                    ExecuteInTransaction(uow =>
                    {
                        EditProfileValidator.Validate(model).ThenThrow(model);

                        var user = UnitOfWork.Users.Get()
                                .First(u => u.Iduser == CurrentUser.Id);

                        Mapper.Map<EditProfileModel, User>(model, user);

                        newName = model.Username;

                        uow.Users.Update(user);

                        uow.SaveChanges();
                    });
                    return newName;
                }

                public EditUserModel GetUserEditModel(Guid id)
                {
                    var user = UnitOfWork.Users.Get()
                                .Include(u => u.Idroles)
                                .FirstOrDefault(u => u.Iduser == id);
                    if (user == null)
                    {
                        throw new NotFoundErrorException("this user does not exist!");
                    }
                    var userModel = Mapper.Map<User, EditUserModel>(user);
                    userModel.Roles = user.Idroles.Select(r => r.Idrole).ToList();
                    return userModel;
                }

                public void EditUserProfile(EditUserModel model)
                {
                    ExecuteInTransaction(uow =>
                    {
                        EditUserProfileValidator.Validate(model).ThenThrow(model);

                        var user = uow.Users.Get()
                                .Include(u => u.Idroles)
                                .FirstOrDefault(u => u.Iduser == model.UserId);

                        Mapper.Map<EditUserModel, User>(model, user);
                        user.Idroles.Clear();
                        foreach (var role in model.Roles)
                        {
                            var newRole = uow.Roles.Get().FirstOrDefault(r => r.Idrole == role);
                            user.Idroles.Add(newRole);
                        }

                        user.LastModifiedOn = DateTime.Now;
                        uow.Users.Update(user);
                        uow.SaveChanges();

                    });
                }

                public bool UserNeedsUpdate(CurrentUserDto currentUser)
                {
                    var user = UnitOfWork.Users.Get().FirstOrDefault(u => u.Iduser == currentUser.Id);

                    if (user == null)
                    {
                        return false;
                    }

                    if (user.LastLoginDate == null)
                    {
                        if (user.LastModifiedOn == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (user.LastModifiedOn == null)
                        {
                            return false;
                        }
                        else
                        {
                            return DateTime.Compare((DateTime)user.LastLoginDate, (DateTime)user.LastModifiedOn) <= 0 ? true : false;
                        }
                    }
                }

                public CurrentUserDto UpdateUserLoginDate(Guid id)
                {
                    var currentUser = new CurrentUserDto();
                    ExecuteInTransaction(uow =>
                    {
                        var user = uow.Users.Get()
                                    .Include(u => u.Idroles)
                                    .FirstOrDefault(u => u.Iduser == id);

                        user.LastLoginDate = DateTime.Now;

                        currentUser = new CurrentUserDto()
                        {
                            Username = user.Username,
                            Email = user.Email,
                            Id = user.Iduser,
                            IsAuthenticated = true,
                            IsDisabled = user.IsDeleted ?? false,
                            Name = user.Name,
                            Roles = user.Idroles.Select(r => r.Name).ToList()
                        };

                        uow.Users.Update(user);
                        uow.SaveChanges();
                    });
                    return currentUser;
                }*/
    }
}
