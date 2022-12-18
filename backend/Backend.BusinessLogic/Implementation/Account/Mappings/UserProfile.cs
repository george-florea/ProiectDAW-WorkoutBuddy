using AutoMapper;
using System;
using Backend.BusinessLogic.Account;
using Backend.Entities;

namespace WorkoutBuddy.BusinessLogic.Account
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(a => a.Iduser, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Salt, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Password, a => a.Ignore())
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.BirthDate, a => a.MapFrom(s => s.BirthDay))
                .ForMember(a => a.Username, a => a.MapFrom(s => s.Username));

            CreateMap<AddWeightModel, UserWeightHistory>()
                .ForMember(a => a.Weight, a => a.MapFrom(s => s.Weight))
                .ForMember(a => a.WeighingDate, a => a.MapFrom(s => DateTime.Now))
                .ForMember(a => a.Iduser, a => a.MapFrom(s => s.UserId))
                .ForMember(a => a.IduserNavigation, a => a.Ignore());

            CreateMap<User, UserInfoModel>()
                .ForMember(a => a.UserId, a => a.MapFrom(a => a.Iduser))
                .ForMember(a => a.Name, a => a.MapFrom(a => a.Name))
                .ForMember(a => a.Username, a => a.MapFrom(a => a.Username))
                .ForMember(a => a.Email, a => a.MapFrom(a => a.Email))
                .ForMember(a => a.BirthDate, a => a.MapFrom(a => a.BirthDate))
                .ForMember(a => a.Roles, a => a.MapFrom(a => a.Idroles.Select(s => s.Name).ToList()))
                .ForMember(a => a.PointsNo, a => a.MapFrom(a => a.UserPointsHistories.Select(u => u.PointsNo).Sum()))
                ;

            CreateMap<User, EditProfileModel>()
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Username, a => a.MapFrom(s => s.Username))
                .ForMember(a => a.BirthDate, a => a.MapFrom(s => s.BirthDate))
                ;

            CreateMap<EditProfileModel, User>()
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Username, a => a.MapFrom(s => s.Username))
                .ForMember(a => a.BirthDate, a => a.MapFrom(s => s.BirthDate))
                ;

            CreateMap<User, EditUserModel>()
                .ForMember(a => a.UserId, a => a.MapFrom(s => s.Iduser))
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.UserName, a => a.MapFrom(s => s.Username))
                .ForMember(a => a.Roles, a => a.Ignore())
                ;

            CreateMap<EditUserModel, User>()
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Username, a => a.MapFrom(s => s.UserName))
                ;
        }
    }
}
