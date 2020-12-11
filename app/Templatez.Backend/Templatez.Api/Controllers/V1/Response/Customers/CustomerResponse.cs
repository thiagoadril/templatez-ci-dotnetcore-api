using System;

namespace Templatez.Api.Controllers.V1.Response.Customers
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
