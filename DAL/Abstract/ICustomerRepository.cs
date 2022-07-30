using System.Collections.Generic;
using DAL.EfBase;
using Models.Entities;

namespace DAL.Abstract
{
    public interface ICustomerRepository:IEfBaseRepository<Customer>
    {
    }
}
