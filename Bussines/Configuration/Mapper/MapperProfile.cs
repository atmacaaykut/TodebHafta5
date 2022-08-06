using System.Linq;
using AutoMapper;
using DTO.Customer;
using DTO.User;
using Models.Entities;

namespace Bussines.Configuration.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<UpdateCustomerRequest, Customer>();
            CreateMap<Customer, SearchCustomerResponse>();
            CreateMap<Customer, SearchCustomerResponse2>();
            CreateMap<CreateUserRegisterRequest, User>().ForMember(x => x.Permissions,
                a => a.MapFrom(c => c.UserPermissions.Select(b =>
                    new UserPermission()
                    {
                        Permission = b
                    })));
        }
    }
}
