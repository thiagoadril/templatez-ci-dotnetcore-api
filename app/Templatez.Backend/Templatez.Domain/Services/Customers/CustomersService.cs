using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Entites;
using Templatez.Domain.Interfaces.Services.Customers;
using Templatez.Domain.Repositories;

namespace Templatez.Domain.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomerRepository _repository;

        public CustomersService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateCustomer(CreateCustomerCommand command)
        {
            var customer = new Customer()
            {
                Name = command.Name,
                Email = command.Email
            };

            await _repository.Add(customer);

            return await _repository.SaveChanges() == 0 ? Guid.Empty : customer.Id;
        }

        public async Task<Customer> GetCustomer(Guid id) => await _repository.GetById(id);

        public async Task<List<Customer>> GetCustomers() => await _repository.GetAll();

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            if (customer == null)
                await Task.FromResult(false);

            _repository.Remove(customer);

            return await _repository.SaveChanges() == 0
                ? await Task.FromResult(false)
                : await Task.FromResult(true);
        }

        public async Task<bool> UpdateCustomer(Customer customer, UpdateCustomerCommand updateCommand)
        {
            if (customer == null)
                await Task.FromResult(false);

            if (!string.IsNullOrEmpty(updateCommand.Name))
                customer.Name = customer.Name != updateCommand.Name ? updateCommand.Name : customer.Name;

            if (!string.IsNullOrEmpty(updateCommand.Email))
                customer.Email = customer.Email != updateCommand.Email ? updateCommand.Email : customer.Email;

            customer.UpdatedAt = DateTime.Now;

            _repository.Update(customer);

            return await _repository.SaveChanges() == 0
                ? await Task.FromResult(false)
                : await Task.FromResult(true);
        }

        public async Task<Customer> GetCustomerByEmail(string email) => await _repository.GetByEmail(email);

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
