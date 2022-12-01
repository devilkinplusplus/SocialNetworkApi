using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.RoleDTO;

namespace SocialNetwork.Business.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage(Messages.NullReference);
            RuleFor(x => x.RoleName).NotNull().WithMessage(Messages.NullReference);
            RuleFor(x => x.RoleName).MaximumLength(16).WithMessage("You reached maximum character limit.");
        }
    }
}