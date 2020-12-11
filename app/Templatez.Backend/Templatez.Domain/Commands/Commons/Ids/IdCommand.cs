using Templatez.Domain.Core.Commands;
using Templatez.Domain.Validations.Commons.Ids;
using System;

namespace Templatez.Domain.Commands.Commons.Ids
{
    public class IdCommand : Command
    {
        public static IdCommand Create(Guid id) => new IdCommand(id);
        protected IdCommand(Guid id) => Id = id;
        public override CommandValidation Validate() => new CommandValidation(new IdCommandValidation().Validate(this));
    }
}
