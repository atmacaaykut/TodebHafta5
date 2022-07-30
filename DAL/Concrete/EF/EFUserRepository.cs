using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.DbContexts;
using DAL.EfBase;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Concrete.EF
{
    public class EFUserRepository:EfBaseRepository<User,TodebCampDbContext>,IUserRepository
    {
        public EFUserRepository(TodebCampDbContext context) : base(context)
        {
        }

        public User GetUserWithPassword(string email)
        {
            return Context.Users
                .Include(x => x.Password)
                .FirstOrDefault(x => x.Email == email);
        }

    }
}
