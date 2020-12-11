using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Templatez.Domain.Entites;
using Templatez.Domain.Repositories;
using Templatez.Infra.Data.Core.Contexts;
using Templatez.Infra.Data.Core.Repositories;

namespace Templatez.Infra.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }

        public Task<Customer> GetByEmail(string email) => DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
    }
}
