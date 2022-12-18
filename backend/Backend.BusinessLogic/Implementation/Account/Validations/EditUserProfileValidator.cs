using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLogic.Account;
using Backend.DataAccess;

namespace Backend.BusinessLogic.Account
{
    public class EditUserProfileValidator : AbstractValidator<EditUserModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public EditUserProfileValidator(UnitOfWork unitOfWork)
        {
            RuleFor(r => r.Email)
                .NotNull().WithMessage("Required field")
                .Must(EmailDoesntAlreadyExists).WithMessage("There is another account using this email, please use another one!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Required field");
            RuleFor(r => r.UserName)
                .NotNull().WithMessage("Required field")
                .Must(UsernameDoesntAlreadyExists).WithMessage("There is another account using this username, please use another one!");
            RuleFor(r => r.Roles)
                .NotNull().WithMessage("Required field")
                .Must(r => r.Count > 0).WithMessage("Required field");
            _unitOfWork = unitOfWork;
        }


        public bool EmailDoesntAlreadyExists(EditUserModel model, string email)
        {
            var user = _unitOfWork.Users.Get().FirstOrDefault(u => u.Iduser == model.UserId);
            if (email == user.Email)
            {
                return true;
            }
            var x = !_unitOfWork.Users.Get().Any(u => u.Email == email);
            return x;
        }

        public bool UsernameDoesntAlreadyExists(EditUserModel model, string username)
        {
            var user = _unitOfWork.Users.Get().FirstOrDefault(u => u.Iduser == model.UserId);
            if (username == user.Username)
            {
                return true;
            }
            return !_unitOfWork.Users.Get().Any(u => u.Username == username);
        }

    }
}
