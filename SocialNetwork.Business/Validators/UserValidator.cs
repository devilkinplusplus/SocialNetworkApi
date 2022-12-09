using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(Messages.NullReference);
            RuleFor(x => x.Name).MinimumLength(2).MaximumLength(20).WithMessage(Messages.FailMessage);

            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage(Messages.NullReference);
            RuleFor(x => x.Surname).MinimumLength(2).MaximumLength(25).WithMessage(Messages.FailMessage);

            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage(Messages.NullReference);
            RuleFor(x => x.UserName).MinimumLength(2).MaximumLength(20).WithMessage(Messages.FailMessage);

            // RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(Messages.NullReference);
            // RuleFor(x=>x.Email).EmailAddress().WithMessage(Messages.InvalidEmail);


        }
    }
}