using System.Collections.Generic;
using Models.Entities;

namespace DAL.Abstract
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetAll();
        public void Insert(Customer customer);
        public void Update(Customer customer);
        public void Delete(Customer customer);
        public Customer Get(int id);
    }
}
