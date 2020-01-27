using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BookStore.Domain.Entities;

namespace BookStore.Service.Validators
{
    public class BookValidator : AbstractValidator<BookStore.Domain.Entities.Book>
    {
        public BookValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Can't found the object.");
                    });

            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Is necessary to inform the title.")
                .NotNull().WithMessage("Is necessary to inform the title.");

            RuleFor(c => c.AuthorName)
                .NotEmpty().WithMessage("Is necessary to inform the author Name.")
                .NotNull().WithMessage("Is necessary to inform the author Name.");

             RuleFor(c => c.ReleaseDate)
                 .NotEmpty().WithMessage("Is necessary to inform the release date.")
                 .NotNull().WithMessage("Is necessary to inform the release date.");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Is necessary to inform the price.")
                .NotNull().WithMessage("Is necessary to inform the price.");
        }
    }
}