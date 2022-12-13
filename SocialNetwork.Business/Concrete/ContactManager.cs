using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Business.Validators;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public IDataResult<IEnumerable<Contact>> GetMessages()
        {
            try
            {
                var values = _contactDal.GetAll();
                if (values != null)
                {
                    return new SuccessDataResult<IEnumerable<Contact>>(values);
                }
                return new ErrorDataResult<IEnumerable<Contact>>(Messages.NullReference);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<IEnumerable<Contact>>(e.Message);
            }
        }

        public IResult SendMessage(Guid userId, string message)
        {
            try
            {
                ContactValidator validationRules = new ContactValidator();
                ValidationResult result = validationRules.Validate(new Contact
                {
                    Message = message,
                    UserId = userId,
                    SentDate = DateTime.Now
                });
                if (result.IsValid)
                {
                    _contactDal.Add(new Contact
                    {
                        Message = message,
                        UserId = userId,
                        SentDate = DateTime.Now
                    });
                    return new SuccessResult(Messages.SuccessMessage);
                }
                return new ErrorResult(Messages.UnknownError);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}