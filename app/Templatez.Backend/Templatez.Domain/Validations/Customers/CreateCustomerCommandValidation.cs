
using Templatez.Domain.Commands.Customers;

namespace Templatez.Domain.Validations.Customers
{
    public class CreateCustomerCommandValidation : CustomerValidation<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidation()
        {
            ValidateEmailCreate();
            ValidateNameCreate();
        }
    }
}
