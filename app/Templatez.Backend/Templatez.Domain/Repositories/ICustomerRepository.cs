using System.Threading.Tasks;
using Templatez.Domain.Core.Repositories;
using Templatez.Domain.Entites;

namespace Templatez.Domain.Repositories
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Task<Customer> GetByEmail(string email);
    }
}
