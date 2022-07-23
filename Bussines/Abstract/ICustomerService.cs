using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines.Configuration.Response;
using DTO.Customer;

namespace Bussines.Abstract
{
    public interface ICustomerService
    {
        public IEnumerable<SearchCustomerResponse> GetAll();
        public CommandResponse Insert(CreateCustomerRequest request);
        public CommandResponse Update(UpdateCustomerRequest request);
        public CommandResponse Delete(Customer customer);
        IEnumerable<SearchCustomerResponse2> GetAllForReport();
    }
}
