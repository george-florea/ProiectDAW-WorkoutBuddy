using AutoMapper;
using Backend.Entities;
using System;

namespace Backend.BusinessLogic.Splits
{
    public class SplitProfile : Profile
    {
        public SplitProfile()
        {
            CreateMap<SplitModel, Split>()
                .ForMember(a => a.Idsplit, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Idcreator, a => a.MapFrom(s => s.CreatorId))
                .ForMember(a => a.Description, a => a.MapFrom(s => s.Description))
                .ForMember(a => a.IsPrivate, a => a.MapFrom(s => !(s.IsPrivate.HasValue && s.IsPrivate == false)))
                .ForMember(s => s.Workouts, s => s.Ignore());

            CreateMap<Split, Workout>()
                .ForMember(a => a.Idworkout, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Idsplit, a => a.MapFrom(s => s.Idsplit))
                .ForMember(a => a.IdsplitNavigation, a => a.MapFrom(s => s));

            CreateMap<Split, SplitListItem>()
                .ForMember(a => a.SplitId, a => a.MapFrom(s => s.Idsplit))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Description, a => a.MapFrom(s => s.Description))
                .ForMember(a => a.Rating, a => a.MapFrom(s => 0))
                .ForMember(a => a.CreatorName, a => a.MapFrom(s => s.IdcreatorNavigation.Username))
                .ForMember(a => a.Workouts, a => a.MapFrom(s => s.Workouts.Select(w => w.Name).ToList()));

            CreateMap<Comment, CommentModel>()
                .ForMember(a => a.CommentId, a => a.MapFrom(s => s.Idcomment))
                .ForMember(a => a.CommentText, a => a.MapFrom(s => s.CommentText))
                .ForMember(a => a.ParentSplitID, a => a.MapFrom(s => s.Idsplit))
                .ForMember(a => a.ParentCommentId, a => a.MapFrom(s => Guid.Empty));

            CreateMap<Split, ViewSplitModel>()
                .ForMember(s => s.SplitId, a => a.MapFrom(s => s.Idsplit))
                .ForMember(s => s.Name, s => s.MapFrom(s => s.Name))
                .ForMember(s => s.Description, s => s.MapFrom(s => s.Description))
                .ForMember(s => s.CreatorName, s => s.MapFrom(s => s.IdcreatorNavigation.Username))
                .ForMember(s => s.CreatorId, s => s.MapFrom(s => s.Idcreator))
                .ForMember(s => s.Rating, s => s.MapFrom(s => 0))
                //.ForMember(s => s.Comments, s => s.Ignore())
                .ForMember(s => s.Workouts, s => s.Ignore());

            CreateMap<Split, SplitModel>()
                .ForMember(s => s.SplitId, a => a.MapFrom(s => s.Idsplit))
                .ForMember(s => s.Name, s => s.MapFrom(s => s.Name))
                .ForMember(s => s.Description, s => s.MapFrom(s => s.Description))
                .ForMember(s => s.CreatorId, s => s.MapFrom(s => s.Idcreator))
                .ForMember(s => s.IsPrivate, s => s.MapFrom(s => s.IsPrivate))
                .ForMember(s => s.MusclesGroups, s => s.Ignore())
                .ForMember(s => s.Workouts, s => s.Ignore());

        }
    }
}
