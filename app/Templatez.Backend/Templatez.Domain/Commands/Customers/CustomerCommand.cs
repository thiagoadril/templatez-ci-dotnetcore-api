using Templatez.Domain.Core.Commands;

namespace Templatez.Domain.Commands.Customers
{
    public abstract class CustomerCommand : Command
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
