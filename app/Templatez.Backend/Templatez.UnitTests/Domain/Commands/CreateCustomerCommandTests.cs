using Templatez.Domain.Commands.Customers;
using Shouldly;
using Xunit;


namespace Templatez.UnitTests.Domain.Commands
{
    public class CreateCustomerCommandTests
    {
        [Fact]
        public void ValidateCreateCustomer_ShouldBeSuccess()
        {
            new CreateCustomerCommand("Customer", "customer@company.com")
                .Validate()
                .IsValid
                .ShouldBeTrue();
        }
    }
}
