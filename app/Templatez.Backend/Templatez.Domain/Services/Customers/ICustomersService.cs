using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Entites;

namespace Templatez.Domain.Interfaces.Services.Customers
{
    public interface ICustomersService
    {
        Task<Customer> GetCustomer(Guid id);
        Task<Customer> GetCustomerByEmail(string email);
        Task<List<Customer>> GetCustomers();
        Task<Guid> CreateCustomer(CreateCustomerCommand command);
        Task<bool> UpdateCustomer(Customer customer, UpdateCustomerCommand updateCommand);
        Task<bool> DeleteCustomer(Customer customer);
    }
}
