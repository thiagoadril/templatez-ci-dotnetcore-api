using Templatez.Domain.Validations.Customers.Messages;
using System.ComponentModel.DataAnnotations;

namespace Templatez.Api.Controllers.V1.Request.Customers
{
    public class UpdateCustomerRequest
    {
        [MinLength(3, ErrorMessage = CustomerValidationMessages.NameMinimumLength)]
        [MaxLength(100, ErrorMessage = CustomerValidationMessages.NameMaximumLength)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = CustomerValidationMessages.EmailInvalid)]
        public string Email { get; set; }
    }
}
