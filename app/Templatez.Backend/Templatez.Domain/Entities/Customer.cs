using System;
using Templatez.Domain.Core.Entities;

namespace Templatez.Domain.Entites
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
