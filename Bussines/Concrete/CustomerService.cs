
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using BackgroundJobs.Abstract;
using Bussines.Abstract;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using Bussines.Configuration.Validator.FluentValidation;
using DAL.Abstract;
using DTO.Customer;
using FluentValidation;
using Models.Entities;

namespace Bussines.Concrete
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private IMapper _mapper;
        private IJobs _jobs;

        public CustomerService(ICustomerRepository repository,IMapper mapper, IJobs jobs)
        {
            _repository = repository;
            _mapper = mapper;
            _jobs = jobs;
        }

        public IEnumerable<SearchCustomerResponse> GetAll()
        {
            var data= _repository.GetAll();
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

            var validator = new CreateCustomerRequestValidator();
            validator.Validate(request).ThrowIfException();

            //bu kısmı extension metodun içine aldık.
            //if (valid.IsValid == false)
            //{
            //    var message = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
            //    throw new ValidationException(message);
            //}

            var entity = _mapper.Map<Customer>(request);

            //aşağıda ki mapleme işini Automapper ile yukarıda yaptık.
            //var entity = new Customer();
            //entity.Email = request.Email;
            //entity.Phone = request.Phone;
            //entity.Name = request.Name;
            //entity.Surname = request.Surname;

            

             _repository.Add(entity);
            _repository.SaveChanges();


             _jobs.FireAndForget(entity.Id,entity.Name);
             _jobs.DelayedJob(entity.Id,entity.Name,TimeSpan.FromSeconds(15));

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

            var entity = _repository.Get(x=>x.Id==request.Id);


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
