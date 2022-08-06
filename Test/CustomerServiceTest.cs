using System;
using AutoMapper;
using BackgroundJobs.Abstract;
using Bussines.Concrete;
using Bussines.Configuration.Mapper;
using DAL.Abstract;
using DTO.Customer;
using FluentAssertions;
using Models.Entities;
using Moq;
using Xunit;

namespace Test
{
    public class CustomerServiceTest
    {
        [Fact]
        public void CustomerServiceCreate_Success()
        {
            //arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>()));

            var jobsMock = new Mock<IJobs>();
            jobsMock.Setup(x => x.FireAndForget(It.IsAny<int>(), It.IsAny<string>()));
            jobsMock.Setup(x => x.DelayedJob(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<TimeSpan>()));

            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);
           // mapper.ConfigurationProvider.AssertConfigurationIsValid();

            
            var customerService =
                new CustomerService(customerRepositoryMock.Object, mapper, jobsMock.Object);

            var customerRequest = new CreateCustomerRequest()
            {
                Email = "asd@asd.com",
                Phone = "234",
                Name = "Murat",
                Surname = "asd"
            };
            //act


            var response = customerService.Insert(customerRequest);

            response.Status.Should().BeTrue();

        }
    }
}
