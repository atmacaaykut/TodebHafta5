using System.Collections.Generic;
using System.Linq;
using DAL.Abstract;
using DAL.DbContexts;
using DAL.EfBase;
using Models.Entities;

namespace DAL.Concrete.EF
{
    public class EFCustomerRepository:EfBaseRepository<Customer, TodebCampDbContext>,ICustomerRepository
    {

        public EFCustomerRepository(TodebCampDbContext context):base(context)
        {
        }

    }
}
