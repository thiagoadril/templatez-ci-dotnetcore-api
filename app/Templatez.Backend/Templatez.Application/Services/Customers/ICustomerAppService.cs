using Templatez.Application.Core.Results;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Templatez.Application.Services.Customers
{
    public interface ICustomerAppService
    {
        Task<IResult<Customer>> GetCustomer(Guid id);
        Task<IResult<List<Customer>>> GetAllCustomers();
        Task<IResult<Guid>> CreateCustomer(CreateCustomerCommand createCommand);
        Task<IResult<bool>> UpdateCustomer(Guid id, UpdateCustomerCommand updateCommand);
        Task<IResult<bool>> DeleteCustomer(Guid id);
    }
}
