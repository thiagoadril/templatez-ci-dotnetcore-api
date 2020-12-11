using AutoMapper;
using Templatez.Application.Core.Results;
using Templatez.Domain.Commands.Customers;
using Templatez.Domain.Entites;
using Templatez.Domain.Interfaces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Templatez.Application.Services.Customers
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomersService _service;
        public CustomerAppService(ICustomersService service)
        {
            _service = service;
        }

        public async Task<IResult<List<Customer>>> GetAllCustomers()
            => await Result<List<Customer>>.SuccessAsync(await _service.GetCustomers());

        public async Task<IResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null)
                return await Result<Customer>.FailAsync("customer not found");

            return await Result<Customer>.SuccessAsync(customer);
        }

        public async Task<IResult<Guid>> CreateCustomer(CreateCustomerCommand createCommand)
        {
            if (createCommand == null)
                return await Result<Guid>.FailAsync("customer fields");

            var validator = createCommand.Validate();
            if (!validator.IsValid)
                return await Result<Guid>.FailValidationAsync(validator.Errors);

            var customer = await _service.GetCustomerByEmail(createCommand.Email);
            if (customer != null)
                return await Result<Guid>.FailAsync("customer already exists");

            var id = await _service.CreateCustomer(createCommand);
            if (id == Guid.Empty)
                return await Result<Guid>.FailAsync("unable to create customer");

            return await Result<Guid>.CreatedAsync(id);
        }

        public async Task<IResult<bool>> UpdateCustomer(Guid id, UpdateCustomerCommand updateCommand)
        {
            if (updateCommand == null)
                return await Result<bool>.FailAsync("customer empty");

            var validator = updateCommand.Validate();
            if (!validator.IsValid)
                return await Result<bool>.FailValidationAsync(validator.Errors);

            var customer = await _service.GetCustomer(id);
            if (customer == null)
                return await Result<bool>.FailAsync("customer not found");

            var updated = await _service.UpdateCustomer(customer, updateCommand);
            if (!updated)
                return await Result<bool>.FailAsync("unable to update customer");

            return await Result<bool>.SuccessAsync(updated);
        }

        public async Task<IResult<bool>> DeleteCustomer(Guid id)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null)
                return await Result<bool>.FailAsync("customer not found");

            var removed = await _service.DeleteCustomer(customer);
            if (!removed)
                return await Result<bool>.FailAsync("unable to delete customer");

            return await Result<bool>.SuccessAsync(removed);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}