using FluentValidation;
using System.Text.RegularExpressions;
using Backend.DataAccess;

namespace Backend.BusinessLogic.Account
{
    public class RegisterUserValidator : AbstractValidator<RegisterModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public RegisterUserValidator(UnitOfWork unitOfWork)
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Required field")
                .Must(EmailDoesntAlreadyExists).WithMessage("There is another account using this email, please use another one!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.PasswordString)
                .Must(PasswordRegexTest).WithMessage("The password must contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number")
                .NotEmpty().WithMessage("Required field");
            RuleFor(r => r.Name)
                .Must(IsNameValid).WithMessage("Please enter a valid name!")
                .NotEmpty().WithMessage("Required field");
            RuleFor(r => r.Username)
                .Must(UsernameDoesntAlreadyExists).WithMessage("There is another account using this username, please use another one!")
                .NotEmpty().WithMessage("Required field");
            RuleFor(r => r.BirthDay)
                .Must(IsOldEnough).WithMessage("You have to be 18 years old or older!")
                .Must(IsNotTooOld).WithMessage("You cant be older than 150 years!")
                .NotEmpty().WithMessage("Required field");
            RuleFor(r => r.Weight)
                .Must(w => w > 0 && w < 350).WithMessage("Please enter a valid weight")
                .NotNull().WithMessage("Required field!");
            _unitOfWork = unitOfWork;
        }

        private bool IsNameValid(string name)
        {
            if(name == null)
            {
                return false;
            }
            return name.All(ch => Char.IsLetter(ch) || ch == ' ');
        }

        private bool PasswordRegexTest(string password)
        {
            if(password == null)
            {
                return false;
            }
            var pattern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}";
            Regex re = new Regex(pattern);
            var res = re.IsMatch(password);
            return res;
        }

        public bool EmailDoesntAlreadyExists(string email)
        {
            return !_unitOfWork.Users.Get().Any(u => u.Email == email);
        }

        public bool UsernameDoesntAlreadyExists(string username)
        {
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
