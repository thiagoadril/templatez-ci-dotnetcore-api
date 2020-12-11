using AutoMapper;
using Templatez.Api.Controllers.V1.Response.Common;
using Templatez.Api.Controllers.V1.Response.Customers;
using Templatez.Application.Core.Results;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Entites;
using System;
using System.Collections.Generic;

namespace Templatez.Api.Controllers.Mappers.Customers
{
    public class CustomerApplicationToControllerMapper : Profile
    {
        public CustomerApplicationToControllerMapper()
        {
            CreateMap<Customer, CustomerResponse>();

            CreateMap<IResult<List<Customer>>, Result<IEnumerable<CustomerResponse>>>().ForPath(e => e.Data, x => x.MapFrom(r => r.Data));
            CreateMap<IResult<Guid>, Result<CreateResponse>>().ForPath(e => e.Data.Id, x => x.MapFrom(r => r.Data));
            CreateMap<IResult<bool>, Result<UpdateResponse>>().ForPath(e => e.Data.Updated, x => x.MapFrom(r => r.Data));
            CreateMap<IResult<bool>, Result<DeleteResponse>>().ForPath(e => e.Data.Deleted, x => x.MapFrom(r => r.Data));

            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();
        }
    }
}
