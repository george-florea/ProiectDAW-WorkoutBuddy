using Backend.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Splits
{
    public class EditSplitValidator : AbstractValidator<SplitModel>
    {
        private readonly UnitOfWork uow;
        public EditSplitValidator(UnitOfWork uow)
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Required field!")
                .Must(IsSameName).WithMessage("There cannot be 2 splits with the same name!");
            this.uow = uow;
        }

        private bool IsSameName(SplitModel model, string? Name)
        {
            if (Name == null)
            {
                return false;
            }
            var listOfNames = uow.Splits.Get()
                                .Where(e => e.Idsplit != model.SplitId)
                                .Select(e => e.Name.ToLower())
                                .ToList();

            return !listOfNames.Contains(Name.ToLower());
        }
    }
}
