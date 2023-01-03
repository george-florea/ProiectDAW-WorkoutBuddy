using Backend.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Backend.BusinessLogic.Splits
{
    public class SplitValidator : AbstractValidator<SplitModel>
    {
        private readonly UnitOfWork uow;
        public SplitValidator(UnitOfWork uow)
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Required field!")
                .Must(IsSameName).WithMessage("There cannot be 2 splits with the same name!")
                ;
            RuleFor(r => r.Workouts)
                .NotNull().WithMessage("You cannot create a split without adding at least 1 workout!")
                .Must(ContainsExercises).WithMessage("You cannot add a workout without Exercises");
            this.uow = uow;
        }

        private bool IsSameName(string? Name)
        {
            if (Name == null)
            {
                return false;
            }
            var listOfNames = uow.Splits.Get()
                                .Select(e => e.Name.ToLower())
                                .ToList();
            return !listOfNames.Contains(Name.ToLower());
        }

        private bool ContainsExercises(List<WorkoutModel>? Workouts)
        {
            if(Workouts == null)
            {
                return false;
            }
            foreach(var workout in Workouts)
            {
                if(workout.Exercises == null || workout.Exercises.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
