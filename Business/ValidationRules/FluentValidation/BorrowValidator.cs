using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BorrowValidator : AbstractValidator<Borrow>
    {
        public BorrowValidator()
        {
            RuleFor(b => b.BorrowDate).LessThanOrEqualTo(b => b.ReturnDate).WithMessage(Messages.InvalidBorrowDate);
        }
    }
}
