using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLogic.Account;

namespace Backend.BusinessLogic.Account
{
    public class AddWeightValidator : AbstractValidator<AddWeightModel>
    {
        public AddWeightValidator()
        {
            RuleFor(s => s.Weight)
                .Must(s => s > 0).WithMessage("Enter a valid weight!")
                .NotNull().WithMessage("This a required field, please enter a value!");
        }
    }
}
