using Templatez.Domain.Core.Commands;
using Templatez.Domain.Validations.Customers;

namespace Templatez.Domain.Commands.Customers
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public override CommandValidation Validate() => new CommandValidation(new UpdateCustomerCommandValidation().Validate(this));
    }
}
