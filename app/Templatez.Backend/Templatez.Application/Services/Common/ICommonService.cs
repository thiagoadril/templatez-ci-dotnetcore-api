using Templatez.Domain.Core.Commands;

namespace Templatez.Application.Services.Common
{
    public interface ICommonService
    {
        bool HasInvalidCommand(CommandValidation commandValidation);
    }
}
