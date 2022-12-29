using AutoMapper;
using System;
using Backend.Entities;

namespace Backend.BusinessLogic.Exercises
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            /*CreateMap<Exercise, ExercisesModel>()s
                .ForMember(a => a.Description, a => a.MapFrom(s => s.Description))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.IdImage, a => a.MapFrom(s => s.Idimage))
                .ForMember(a => a.MuscleGroups, a => a.MapFrom(s => s.Idgroups.Select(g => g.Name).ToList()));
*/
            CreateMap<Exercise, ExerciseAsListItemModel>()
                .ForMember(a => a.ExerciseId, a => a.MapFrom(s => s.Idexercise))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.IdImage, a => a.MapFrom(s => s.Idimage));
/*
            CreateMap<AddExerciseModel, Exercise>()
                .ForMember(a => a.Idexercise, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Name, a => a.MapFrom(a => a.Name))
                .ForMember(a => a.IsPending, a => a.MapFrom(a => true))
                .ForMember(a => a.Description, a => a.MapFrom(s => s.Description))
                .ForMember(a => a.Idtype, a => a.MapFrom(s => s.SelectedType));

            CreateMap<Exercise, EditExerciseModel>()
                .ForMember(a => a.ExerciseId, a => a.MapFrom(s => s.Idexercise))
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Description, a => a.MapFrom(s => s.Description))
                .ForMember(a => a.SelectedType, a => a.MapFrom(s => s.IdtypeNavigation.Idtype));*/

        }
    }
}
