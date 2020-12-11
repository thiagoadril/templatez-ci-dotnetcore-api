
using Templatez.Domain.Commands.Customers;

namespace Templatez.Domain.Validations.Customers
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateEmailUpdate();
            ValidateNameUpdate();
        }
    }
}
