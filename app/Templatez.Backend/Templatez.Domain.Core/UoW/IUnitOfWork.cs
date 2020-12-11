using System;
namespace Templatez.Domain.Core.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
