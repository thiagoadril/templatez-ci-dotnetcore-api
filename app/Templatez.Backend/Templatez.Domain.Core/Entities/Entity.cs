using System;

namespace Templatez.Domain.Core.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
