using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
           RuleFor(command => command.Id).NotEmpty().GreaterThan(0); 
        }
        
    }
}