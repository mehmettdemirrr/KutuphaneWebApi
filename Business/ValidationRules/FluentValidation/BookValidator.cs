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
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.PageCount).GreaterThan(0).WithMessage(Messages.PageCount);

            RuleFor(b => b.UnitsInStock).GreaterThanOrEqualTo(0).WithMessage(MinimumStock);

            RuleFor(b => b.ISBN).NotEmpty().WithMessage(Messages.ISBNIsEmpty);
            RuleFor(b => b.ISBN).MinimumLength(2).WithMessage(Messages.ISBNIsShort);

            RuleFor(b => b.Title).NotEmpty().WithMessage(Messages.TitleIsEmpty);
            RuleFor(b => b.Title).MinimumLength(2).WithMessage(Messages.TitleIsShort);
        }

        //private bool StartWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}