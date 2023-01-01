using Backend.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Backend.BusinessLogic.Exercises
{
    public class InsertExerciseValidator : AbstractValidator<InsertExerciseModel>
    {
        private readonly UnitOfWork uow;
        public InsertExerciseValidator(UnitOfWork uow)
        {
            this.uow = uow;
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Required field!")
                .Must(IsSameName).WithMessage("There cannot be 2 exercises with the same name!");
            RuleFor(r => r.SelectedType)
                .NotNull().WithMessage("Required field!");
            RuleFor(r => r.Image)
                .Must(IsImageExtensionCorrect).WithMessage("Please enter a image!");
            RuleFor(r => r.SelectedMuscleGroups)
                .NotNull().WithMessage("Please select a muscle group!");
        }

        private bool IsSameName(InsertExerciseModel model, string? Name)
        {
            if (Name == null)
            {
                return false;
            }
            var listOfNames = uow.Exercises.Get()
                                .Where(e => (model.ExerciseId != Guid.Empty) && (e.Idexercise != model.ExerciseId))
                                .Select(e => e.Name.ToLower())
                                .ToList();
            return !listOfNames.Contains(Name.ToLower());
        }

        private bool IsImageExtensionCorrect(InsertExerciseModel model, IFormFile Image)
        {
            if (model.ExerciseId == Guid.Empty && Image == null)
            {
                return false;
            }
            else if (model.ExerciseId != Guid.Empty && Image == null)
            {
                return true;
            }

            var acceptedContentTypes = new List<string>()
            {
                "image/gif",
                "image/jpeg",
                "image/jpg",
                "image/png"
            };
            return acceptedContentTypes.Contains(Image.ContentType);
        }

    }
}
