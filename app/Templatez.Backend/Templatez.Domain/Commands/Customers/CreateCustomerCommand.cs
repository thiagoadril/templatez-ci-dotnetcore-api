using Templatez.Domain.Core.Commands;
using Templatez.Domain.Validations.Customers;

namespace Templatez.Domain.Commands.Customers
{
    public class CreateCustomerCommand : CustomerCommand
    {
        public CreateCustomerCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override CommandValidation Validate() => new CommandValidation(new CreateCustomerCommandValidation().Validate(this));
    }
}