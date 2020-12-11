using AutoMapper;
using Templatez.Api.Controllers.V1.Request.Customers;
using Templatez.Domain.Commands.Customers;

namespace Templatez.Api.Controllers.Mappers.Customers
{
    public class CustomerControllerToApplicationMapper : Profile
    {
        public CustomerControllerToApplicationMapper()
        {
            CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
            CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
        }
    }
}
