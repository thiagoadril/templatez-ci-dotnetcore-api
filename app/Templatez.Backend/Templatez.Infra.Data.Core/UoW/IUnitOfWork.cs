using System;
namespace Templatez.Infra.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
