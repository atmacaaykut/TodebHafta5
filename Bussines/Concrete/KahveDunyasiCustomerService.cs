using AutoMapper;
using Bussines.Abstract;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using Bussines.Configuration.Validator.FluentValidation;
using DAL.Abstract;
using DTO.Customer;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bussines.Concrete
{
    public class KahveDunyasiCustomerService:ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private IMapper _mapper;

        public KahveDunyasiCustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<SearchCustomerResponse> GetAll()
        {
            var data = _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<SearchCustomerResponse>(x)).ToList();
            return mappedData;
        }

        public IEnumerable<SearchCustomerResponse2> GetAllForReport()
        {
            var data = _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<SearchCustomerResponse2>(x)).ToList();
            return mappedData;
        }

        public CommandResponse Insert(CreateCustomerRequest request)
        {
            var validator = new CreateKDCustomerRequestValidator();
            validator.Validate(request).ThrowIfException();
            var entity = _mapper.Map<Customer>(request);

            _repository.Insert(entity);

            return new CommandResponse
            {
                Status = true,
                Message = $"Müşteri eklendi. "
            };
        }
    

        public CommandResponse Update(UpdateCustomerRequest request)
        {
            var validator = new UpdateCustomerRequstValidator();
            validator.Validate(request).ThrowIfException();

            var entity = _repository.Get(request.Id);
            if (entity == null)
            {
                return new CommandResponse()
                {
                    Status = false,
                    Message = "Veri tabanında bu Id de kayıt bulunmamaktadır"
                };
            }

            var mappedEntity = _mapper.Map(request, entity);

            _repository.Update(mappedEntity);

            return new CommandResponse
            {
                Status = true,
                Message = $"Müşteri Güncellendi"
            };
        }

        public CommandResponse Delete(Customer customer)
        {
            _repository.Delete(customer);

            return new CommandResponse
            {
                Status = true,
                Message = $"Müşteri eklendi. Id={customer.Id}"
            };
        }

       
    }
}
