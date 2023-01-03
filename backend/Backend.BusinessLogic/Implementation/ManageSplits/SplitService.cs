using Backend.BusinessLogic.Base;
using Backend.Common.Exceptions;
using Backend.Common.Extensions;
using Backend.Entities;
using Backend.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Splits
{
    public class SplitService : BaseService
    {
        private readonly SplitValidator SplitValidator;
        private readonly EditSplitValidator EditSplitValidator;
        public SplitService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            SplitValidator = new SplitValidator(UnitOfWork);
            EditSplitValidator = new EditSplitValidator(UnitOfWork);
        }

        public async Task<List<SplitListItem>> GetSplits()
        {
            var splits = await UnitOfWork.Splits.Get()
                .Include(s => s.Workouts)
                .Include(s => s.IdcreatorNavigation)
                    .ThenInclude(c => c.UserPointsHistories)
                .Include(s => s.UserSplits)
                .ToListAsync();

            var splitList = new List<SplitListItem>();
            if (splits.Count > 0)
            {
                foreach (var split in splits)
                {
                    if (!split.IsPrivate)
                    {
                        var noOfWorkouts = split.Workouts.Count;
                        var splitListItem = Mapper.Map<Split, SplitListItem>(split);

                        var rating = float.Parse("0");
                        foreach (var userSplit in split.UserSplits)
                        {
                            if (userSplit.Rating != null)
                            {
                                rating += (int)userSplit.Rating;
                            }
                        }

                        rating /= split.UserSplits.Where(us => us.Rating != null).Count();

                        splitListItem.Rating = float.Parse(rating.ToString("0.00"));
                        splitListItem.WorkoutsNo = noOfWorkouts;
                        splitList.Add(splitListItem);
                    }
                }
            }
            return splitList.OrderByDescending(s => s.Rating).ToList();
        }

        public ViewSplitModel GetSplit(Guid id)
        {
            var split = UnitOfWork.Splits.Get()
                .Include(s => s.Workouts)
                    .ThenInclude(w => w.WorkoutExercises)
                    .ThenInclude(we => we.IdexerciseNavigation)
                .Include(s => s.IdcreatorNavigation)
                .Include(s => s.Comments)
                    .ThenInclude(s => s.InverseIdparentCommNavigation)
                .FirstOrDefault(s => s.Idsplit == id);

            if (split == null)
            {
                throw new NotFoundErrorException("this split does not exist!");
            }

            var workoutsListModel = new List<ListWorkoutModel>();

            foreach (var workout in split.Workouts)
            {
                var exercises = workout.WorkoutExercises
                        .Select(w => w.IdexerciseNavigation)
                        .Select(e => e.Name)
                        .ToList();
                workoutsListModel.Add(new ListWorkoutModel()
                {
                    WorkoutName = workout.Name,
                    ExercisesList = exercises
                });
            }

            var comments = new List<CommentModel>();
            var comms = split.Comments
                            .Where(c => c.IdparentComm == null)
                            .ToList();

            foreach (var comment in comms)
            {
                var commentReplys = comment.InverseIdparentCommNavigation.ToList();
                var replys = new List<CommentModel>();

                foreach (var reply in commentReplys)
                {
                    var user = UnitOfWork.Users.Get()
                               .Include(u => u.Idroles)
                            .FirstOrDefault(u => u.Iduser == reply.Iduser);

                    if (user == null)
                    {
                        throw new NotFoundErrorException("This user does not exist!");
                    }

                    var username = user.Username;

                    var replyModel = Mapper.Map<Comment, CommentModel>(reply);

                    replyModel.ParentCommentId = reply.IdparentComm;
                    replyModel.Author = username;
                    replyModel.AuthorRole = user.Idroles.LastOrDefault().Name;
                    replys.Add(replyModel);
                }

                var author = UnitOfWork.Users.Get()
                                .Include(u => u.Idroles)
                            .FirstOrDefault(u => u.Iduser == comment.Iduser);

                if (author == null)
                {
                    throw new NotFoundErrorException("This user does not exist!");
                }

                var authorUsername = author.Username;

                var commentModel = Mapper.Map<Comment, CommentModel>(comment);

                commentModel.CommentReplys = replys;
                commentModel.Author = authorUsername;
                commentModel.AuthorRole = author.Idroles.LastOrDefault().Name;

                comments.Add(commentModel);
            }

            var viewSplitModel = Mapper.Map<Split, ViewSplitModel>(split);

            viewSplitModel.Workouts = workoutsListModel;
            viewSplitModel.Comments = comments;
            return viewSplitModel;
        }


        //add split async 
        /*public async Task AddSplit(SplitModel model)
        {
            await ExecuteInTransactionAsync(async uow =>
            {
                var validationRes = await SplitValidator.ValidateAsync(model);
                if (!validationRes.IsValid)
                {
                    var muscleGroups = Enum.GetValues(typeof(MuscleGroups)).Cast<MuscleGroups>()
                        .Select(v => new System.Web.Mvc.SelectListItem()
                        {
                            Text = v.ToString(),
                            Value = ((int)v).ToString(),
                        }).ToList();
                    model.MusclesGroups = muscleGroups;
                    validationRes.ThenThrow(model);

                }

                var split = Mapper.Map<SplitModel, Split>(model);

                var workouts = new List<Workout>();
                for (var i = 0; i < model.Workouts.Count; i++)
                {
                    var currentWorkout = model.Workouts[i];
                    if (!currentWorkout.IsDeleted)
                    {
                        var workout = Mapper.Map<Split, Workout>(split);
                        workout.Name = currentWorkout.WorkoutName;

                        currentWorkout.Exercises.ForEach(e =>
                        {
                            var exercise = uow.Exercises
                                            .Get()
                                            .Where(ex => ex.Idexercise == e)
                                            .FirstOrDefault();
                            
                            if(exercise == null)
                            {
                                throw new NotFoundErrorException("the exercise does not exist!");
                            }

                            workout.WorkoutExercises.Add(new WorkoutExercise()
                            {
                                Idworkout = workout.Idworkout,
                                Idexercise = e,
                                IdworkoutNavigation = workout,
                                IdexerciseNavigation = exercise
                            });
                        });
                        split.Workouts.Add(workout);
                    }
                }
                var user = await uow.Users.Get()
                            .FirstOrDefaultAsync(u => u.Iduser == split.Idcreator);
                user.UserSplits.Add(new UserSplit()
                {
                    Idsplit = split.Idsplit,
                    IdsplitNavigation = split,
                    Iduser = user.Iduser,
                    IduserNavigation = user
                });

                uow.Splits.Insert(split);
                await uow.SaveChangesAsync();
            });
        }*/
        /*
                public void AddSplit(SplitModel model)
                {
                    ExecuteInTransaction(uow =>
                    {
                        var validationRes = SplitValidator.Validate(model);

                        if (!validationRes.IsValid)
                        {
                            var muscleGroups = Enum.GetValues(typeof(MuscleGroups)).Cast<MuscleGroups>()
                                .Select(v => new System.Web.Mvc.SelectListItem()
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString(),
                                }).ToList();
                            model.MusclesGroups = muscleGroups;
                            validationRes.ThenThrow(model);

                        }

                        var split = Mapper.Map<SplitModel, Split>(model);

                        var workouts = new List<Workout>();
                        for (var i = 0; i < model.Workouts.Count; i++)
                        {
                            var currentWorkout = model.Workouts[i];
                            if (!currentWorkout.IsDeleted)
                            {
                                var workout = Mapper.Map<Split, Workout>(split);
                                workout.Name = currentWorkout.WorkoutName;

                                currentWorkout.Exercises.ForEach(e =>
                                {
                                    var exercise = uow.Exercises
                                                    .Get()
                                                    .FirstOrDefault(ex => ex.Idexercise == e);

                                    if (exercise == null)
                                    {
                                        throw new NotFoundErrorException("the exercise does not exist!");
                                    }

                                    workout.WorkoutExercises.Add(new WorkoutExercise()
                                    {
                                        Idworkout = workout.Idworkout,
                                        Idexercise = e,
                                        IdworkoutNavigation = workout,
                                        IdexerciseNavigation = exercise
                                    });
                                });
                                split.Workouts.Add(workout);
                            }
                        }
                        var user = uow.Users.Get().FirstOrDefault(u => u.Iduser == split.Idcreator);
                        user.UserSplits.Add(new UserSplit()
                        {
                            Idsplit = split.Idsplit,
                            IdsplitNavigation = split,
                            Iduser = user.Iduser,
                            IduserNavigation = user
                        });
                        uow.Splits.Insert(split);
                        uow.SaveChanges();
                    });
                }



                

                public SplitModel PopulateSplitModel(Guid id)
                {
                    var split = UnitOfWork.Splits.Get()
                        .Include(s => s.IdcreatorNavigation)
                        .FirstOrDefault(s => s.Idsplit == id);

                    if (split == null)
                    {
                        throw new NotFoundErrorException("This split does not exist!");
                    }

                    *//*var muscleGroups = Enum.GetValues(typeof(MuscleGroups)).Cast<MuscleGroups>()
                                .Select(v => new System.Web.Mvc.SelectListItem()
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString(),
                                }).ToList();

                    var workouts = new List<WorkoutModel>();

                    foreach (var workout in split.Workouts)
                    {
                        var exercises = workout.WorkoutExercises
                                .Select(w => w.IdexerciseNavigation)
                                .Select(e => e.Idexercise)
                                .ToList();
                        var selectedMuscles = workout.WorkoutExercises
                                .Select(w => w.IdexerciseNavigation)
                                .SelectMany(e => e.Idgroups)
                                .Distinct()
                                .Select(g => g.Idgroup)
                                .ToList();

                        workouts.Add(new WorkoutModel()
                        {
                            Id = workout.Idworkout,
                            WorkoutName = workout.Name,
                            Exercises = exercises,
                            SelectedMuscleGroups = selectedMuscles
                        });
                    }*//*

                    var editModel = Mapper.Map<Split, SplitModel>(split);
                    *//*editModel.MusclesGroups = muscleGroups;
                    editModel.Workouts = workouts;*//*

                    return editModel;
                }

                public void EditSplit(SplitModel model)
                {
                    ExecuteInTransaction(uow =>
                    {
                        var validationRes = EditSplitValidator.Validate(model);

                        if (!validationRes.IsValid)
                        {
                            //solutie temporara pentru edit doar cu informatiile simple

                            var ok = true;
                            var res = validationRes.Errors.Select(s => s.ErrorMessage).Contains("You cannot create a split without adding at least 1 workout!");
                            if(res && validationRes.Errors.Count == 1)
                            {
                                ok = false;
                            }
                            if (ok)
                            {
                                var returnModel = PopulateSplitModel(model.SplitId);
                                validationRes.ThenThrow(returnModel);
                            }
                        }

                        var split = UnitOfWork.Splits.Get()
                                    .Include(s => s.Workouts)
                                        .ThenInclude(w => w.WorkoutExercises)
                                        .ThenInclude(we => we.IdexerciseNavigation)
                                    .Include(s => s.IdcreatorNavigation)
                                    .FirstOrDefault(s => s.Idsplit == model.SplitId);

                        if (split == null)
                        {
                            throw new NotFoundErrorException("this split does not exist!");
                        }

                        var isPrivate = !(model.IsPrivate.HasValue && model.IsPrivate == false);

                        split.Name = model.Name;
                        split.IsPrivate = isPrivate;
                        split.Description = model.Description;

                        *//*var oldWorkouts = split.Workouts.ToList();

                        foreach (var workout in oldWorkouts)
                        {
                            var modelWorkouts = model.Workouts;
                            if (modelWorkouts.Select(w => w.Id).Contains(workout.Idworkout))
                            {
                                var modelWorkout = modelWorkouts.FirstOrDefault(w => w.Id == workout.Idworkout);
                                if (modelWorkout.IsDeleted == true)
                                {
                                    //daca workout-ul nu mai exista in model il stergem
                                    var we = workout.WorkoutExercises.ToList();
                                    uow.WorkoutExercises.DeleteRange(we);
                                    split.Workouts.Remove(workout);
                                }
                                else
                                {
                                    //verificam daca un exercitiu vechi mai exista in workoutul nou trimis, daca nu, il stergem
                                    foreach (var we in workout.WorkoutExercises)
                                    {
                                        var modelExercises = modelWorkouts.FirstOrDefault(w => w.Id == we.Idworkout).Exercises;
                                        if (!modelExercises.Contains(we.Idexercise))
                                        {
                                            var workoutToUpdate = we.IdworkoutNavigation;
                                            workoutToUpdate.WorkoutExercises.Remove(we);
                                        }
                                    }

                                    //verificam daca in model este trimis un exercitiu care nu exista deja in workoutul curent
                                    var exercises = workout.WorkoutExercises.Select(we => we.Idexercise).ToList();
                                    foreach (var modelExercise in modelWorkout.Exercises)
                                    {
                                        if (!exercises.Contains(modelExercise))
                                        {
                                            var exercise = uow.Exercises.Get().FirstOrDefault(e => e.Idexercise == modelExercise);
                                            var newWE = new WorkoutExercise()
                                            {
                                                Idexercise = modelExercise,
                                                IdexerciseNavigation = exercise,
                                                Idworkout = workout.Idworkout,
                                                IdworkoutNavigation = workout
                                            };
                                            workout.WorkoutExercises.Add(newWE);
                                        }
                                    }
                                }
                            }

                        }

                        for (var i = 0; i < model.Workouts.Count; i++)
                        {
                            var currentWorkout = model.Workouts[i];
                            if (currentWorkout.Exercises != null
                                && !currentWorkout.IsDeleted
                                && currentWorkout.Id == Guid.Empty)
                            {
                                var workout = Mapper.Map<Split, Workout>(split);
                                workout.Name = currentWorkout.WorkoutName;

                                currentWorkout.Exercises.ForEach(e =>
                                {
                                    var exercise = uow.Exercises
                                                    .Get()
                                                    .Where(ex => ex.Idexercise == e)
                                                    .First();
                                    workout.WorkoutExercises.Add(new WorkoutExercise()
                                    {
                                        Idworkout = workout.Idworkout,
                                        Idexercise = e,
                                        IdworkoutNavigation = workout,
                                        IdexerciseNavigation = exercise
                                    });
                                });
                                split.Workouts.Add(workout);
                            }
                        }*//*

                        uow.Splits.Update(split);
                        uow.SaveChanges();
                    });
                }

                public bool DeleteSplit(Guid id)
                {
                    var ok = true;
                    ExecuteInTransaction(uow =>
                    {
                        var split = UnitOfWork.Splits.Get()
                                    .Include(s => s.Workouts)
                                        .ThenInclude(w => w.WorkoutExercises)
                                        .ThenInclude(we => we.IdexerciseNavigation)
                                    .Include(s => s.IdcreatorNavigation)
                                    .Include(s => s.UserSplits)
                                        .ThenInclude(us => us.UserWorkouts)
                                    .FirstOrDefault(s => s.Idsplit == id);
                        if(split == null)
                        {
                            throw new NotFoundErrorException("this split does not exist!");
                        }

                        var oldWorkouts = uow.WorkoutExercises.Get()
                                                .Include(we => we.IdworkoutNavigation)
                                                    .ThenInclude(w => w.IdsplitNavigation)
                                                .Where(w => w.IdworkoutNavigation.IdsplitNavigation.Idsplit == id)
                                                .ToList();
                        //if(oldWorkouts == null)
                        //{
                        //    throw new NotFoundErrorException("this split does not have any exercises");
                        //}

                        if (!split.UserSplits.Any(us => us.UserWorkouts.Count > 0)) 
                        {
                            foreach (var w in oldWorkouts)
                            {
                                uow.WorkoutExercises.Delete(w);
                            }
                            split.UserSplits.Clear();
                            split.Workouts.Clear();
                            uow.Splits.Delete(split);

                            uow.SaveChanges();
                        }
                        else
                        {
                            ok = false;
                        }
                    });
                    return ok;
                }

                public bool AddToUserSplits(Guid SplitId, Guid UserId)
                {
                    var isValid = true;
                    ExecuteInTransaction(uow =>
                    {
                        var split = uow.Splits.Get()
                                    .FirstOrDefault(s => s.Idsplit == SplitId);
                        var user = uow.Users.Get()
                                    .Include(u => u.UserSplits)
                                    .FirstOrDefault(u => u.Iduser == UserId);

                        if (split == null)
                        {
                            throw new NotFoundErrorException("the split does not exist!");
                        }

                        if (user == null)
                        {
                            throw new NotFoundErrorException("the user does not exist!");
                        }

                        if (!user.UserSplits.Any(us => us.Idsplit == split.Idsplit))
                        {
                            user.UserSplits.Add(new UserSplit()
                            {
                                Idsplit = split.Idsplit,
                                IdsplitNavigation = split,
                                Iduser = user.Iduser,
                                IduserNavigation = user
                            });
                            uow.Users.Update(user);
                            uow.SaveChanges();
                        }
                        else
                        {
                            isValid = false;
                        }
                    });
                    return isValid;
                }*/
    }
}
