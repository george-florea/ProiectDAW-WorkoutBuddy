using Backend.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Backend.BusinessLogic.Exercises
{
    
    public class EditExerciseValidator : AbstractValidator<EditExerciseModel>
    {
        private readonly UnitOfWork uow;
        public EditExerciseValidator(UnitOfWork uow)
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Required field!")
                .Must(IsSameName).WithMessage("There cannot be 2 exercises with the same name!")
                ;
            RuleFor(r => r.SelectedType)
                .NotNull().WithMessage("Required field!");
            RuleFor(r => r.Image)
                .Must(IsImageExtensionCorrect).WithMessage("Please enter a image!");
            RuleFor(r => r.SelectedMuscleGroups)
                .NotNull().WithMessage("Please select a muscle group!");
            this.uow = uow;
        }

        private bool IsSameName(EditExerciseModel model, string? Name)
        {
            if (Name == null)
            {
                return false;
            }
            var listOfNames = uow.Exercises.Get()
                                .Where(e => e.Idexercise != model.ExerciseId)
                                .Select(e => e.Name.ToLower())
                                .ToList();

            return !listOfNames.Contains(Name.ToLower());
        }

        private bool IsImageExtensionCorrect(IFormFile Image)
        {
            if(Image == null)
            {
                return true;
            }
            var acceptedContentTypes = new List<string>()
            {
                "image/gif",
                "image/jpeg",
                "image/png"
            };
            return acceptedContentTypes.Contains(Image.ContentType);
        }
    }
}
