using System;

namespace Templatez.Domain.Models
{
    public class Customer : Entity
    {
        //For EntityFramework
        public Customer() { }
        public Customer(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
