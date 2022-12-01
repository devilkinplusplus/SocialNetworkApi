using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Business.Validators;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.DataAccess.Concrete;
using static SocialNetwork.Entities.DTOs.RoleDTO;

namespace SocialNetwork.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _roleDal;
        private readonly IMapper _mapper;
        public RoleManager(IRoleDal roleDal, IMapper mapper)
        {
            _roleDal = roleDal;
            _mapper = mapper;
        }

     
        public IResult Create(CreateRoleDTO role)
        {
            try
            {
                var model = _mapper.Map<Role>(role);
                RoleValidator validationRules = new RoleValidator();
                ValidationResult result = validationRules.Validate(model);

                if (result.IsValid)
                {
                    _roleDal.Add(model);
                    return new SuccessResult(Messages.Created);
                }
                return new ErrorResult(Messages.UnknownError);

            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<IEnumerable<Role>> ListAllRoles()
        {
            try
            {
                using var context = new AppDbContext();
                var values = context.Roles.ToList();
                if (values != null)
                {
                    return new SuccessDataResult<IEnumerable<Role>>(values);
                }
                return new ErrorDataResult<IEnumerable<Role>>(Messages.NullReference);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<IEnumerable<Role>>(e.Message);
            }
        }

        public IResult Update(CreateRoleDTO role, Guid roleId)
        {
            try
            {
                var model = _mapper.Map<Role>(role);
                RoleValidator validationRules = new RoleValidator();
                ValidationResult result = validationRules.Validate(model);
                var data = _roleDal.Get(x => x.Id == roleId);

                if (data != null)
                {
                    if (result.IsValid)
                    {
                        data.RoleName = role.roleName;
                        _roleDal.Update(data);
                        return new SuccessResult(Messages.Updated);
                    }
                    return new ErrorResult(Messages.UnknownError);
                }
                return new ErrorResult(Messages.NullReference);

            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}