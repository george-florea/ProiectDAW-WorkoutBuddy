using Backend.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Backend.BusinessLogic.Exercises
{
    public class AddExerciseValidator : AbstractValidator<AddExerciseModel>
    {
        private readonly UnitOfWork uow;
        public AddExerciseValidator(UnitOfWork uow)
        {
            this.uow = uow;
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Required field!")
                .Must(IsSameName).WithMessage("There cannot be 2 exercises with the same name!")
                ;
            RuleFor(r => r.SelectedType)
                .NotNull().WithMessage("Required field!");
            RuleFor(r => r.Image)
                .Must(IsImageExtensionCorrect).WithMessage("Please enter a image!")
                .NotNull().WithMessage("Required field!");
            RuleFor(r => r.SelectedMuscleGroups)
                .NotNull().WithMessage("Please select a muscle group!");
        }

        private bool IsSameName(string? Name)
        {
            if(Name == null)
            {
                return false;
            }
            var listOfNames = uow.Exercises.Get()
                                .Select(e => e.Name.ToLower())
                                .ToList();
            return !listOfNames.Contains(Name.ToLower());
        }

        private bool IsImageExtensionCorrect(IFormFile Image)
        {
            if(Image == null)
            {
                return false;
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
