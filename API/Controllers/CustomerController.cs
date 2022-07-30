using Bussines.Abstract;
using DTO.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _service.GetAll();
            return Ok(data);
        }

        [HttpGet("GetAllForReport")]

        public IActionResult GetAllForReport()
        {
            var data = _service.GetAllForReport();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post(CreateCustomerRequest customer)
        {
            var response = _service.Insert(customer);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put(UpdateCustomerRequest customer)
        {
            var response = _service.Update(customer);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(Customer customer)
        {
            var response = _service.Delete(customer);
            return Ok(response);
        }

    }
}
