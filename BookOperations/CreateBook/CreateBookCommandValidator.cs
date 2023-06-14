using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
           RuleFor(command => command.Model.GenreId).GreaterThan(0);
           RuleFor(command => command.Model.PageCount).GreaterThan(0);
           RuleFor(command => command.Model.PublishDate.Date).NotEmpty();
           RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
        
    }
}