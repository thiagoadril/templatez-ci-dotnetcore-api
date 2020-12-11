using System;
using System.Text.Json.Serialization;

namespace Templatez.Domain.Core.Commands
{
    public abstract class Command
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public abstract CommandValidation Validate();
    }
}
