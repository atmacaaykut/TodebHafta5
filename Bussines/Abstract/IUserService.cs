using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines.Configuration.Response;
using DTO.User;
using Models.Entities;

namespace Bussines.Abstract
{
    public interface IUserService
    {
        CommandResponse Register(CreateUserRegisterRequest register);
        IEnumerable<User> GetAll();
    }
}
