using AutoMapper;
using DTO.Customer;
using DTO.User;
using Models.Entities;

namespace Bussines.Configuration.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<UpdateCustomerRequest, Customer>();
            CreateMap<Customer, SearchCustomerResponse>();
            CreateMap<Customer, SearchCustomerResponse2>();
            CreateMap<CreateUserRegisterRequest, User>();
        }
    }
}
