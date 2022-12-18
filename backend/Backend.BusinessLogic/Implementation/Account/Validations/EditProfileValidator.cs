
using FluentValidation;
using System.Text.RegularExpressions;
using Backend.BusinessLogic.Account;
using Backend.DataAccess;

namespace Backend.BusinessLogic.Account
{
    public class EditProfileValidator : AbstractValidator<EditProfileModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public EditProfileValidator(UnitOfWork unitOfWork)
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Required field")
                .Must(EmailDoesntAlreadyExists).WithMessage("There is another account using this email, please use another one!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Required field");
            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Required field")
                .Must(UsernameDoesntAlreadyExists).WithMessage("There is another account using this username, please use another one!");
            RuleFor(r => r.BirthDate)
                .NotEmpty().WithMessage("Required field")
                .Must(IsOldEnough).WithMessage("You have to be 18 years old or older!")
                .Must(IsNotTooOld).WithMessage("You cant be older than 150 years!");
            _unitOfWork = unitOfWork;
        }


        public bool EmailDoesntAlreadyExists(EditProfileModel model, string email)
        {
            if(email == model.Email)
            {
                return true;
            }
            return !_unitOfWork.Users.Get().Any(u => u.Email == email);
        }

        public bool UsernameDoesntAlreadyExists(EditProfileModel model, string username)
        {
            if (username == model.Username)
            {
                return true;
            }
            return !_unitOfWork.Users.Get().Any(u => u.Username == username);
        }

        public bool IsOldEnough(DateTime? date)
        {
            return date == null ? false : (DateTime.Now - (DateTime)date).TotalDays > 365 * 18;
        }
        public bool IsNotTooOld(DateTime? date)
        {
            return date == null ? false : (DateTime.Now - (DateTime)date).TotalDays < 365 * 150;
        }

    }
}
