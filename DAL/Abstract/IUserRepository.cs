using Models.Entities;
using System.Collections.Generic;
using DAL.EfBase;

namespace DAL.Abstract
{
    public interface IUserRepository:IEfBaseRepository<User>
    {
        User GetUserWithPassword(string email);
    }
}
