using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
        }
        
    }
}