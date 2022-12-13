using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SocialNetwork.Business.Constants;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.Business.Validators
{
    public class ContactValidator:AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x=>x.Message).NotNull().NotEmpty().WithMessage(Messages.NullReference);
            RuleFor(x=>x.Message).MaximumLength(255).WithMessage("You reached maximum character limit");
            RuleFor(x=>x.Message).MinimumLength(1).WithMessage("Minimum charcater limit is 1");
        }
    }
}