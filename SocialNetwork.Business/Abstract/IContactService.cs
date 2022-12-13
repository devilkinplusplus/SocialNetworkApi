using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.Business.Abstract
{
    public interface IContactService
    {
        IResult SendMessage(Guid userId,string message);
        IDataResult<IEnumerable<Contact>> GetMessages();
    }
}