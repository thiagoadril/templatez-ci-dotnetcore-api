using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Templatez.Domain.Core.Settings.Database;
using Templatez.Domain.Entites;
using Templatez.Infra.Data.Core.EntityConfiguration;

namespace Templatez.Infra.Data.Core.Contexts
{
    public class AppDbContext : DbContext
    {
        private IOptions<DatabaseSettings> _dbOptions;

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DatabaseSettings> dbOptions) : base(options)
        {
            _dbOptions = dbOptions;
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(_dbOptions.Value.PostgreSQL.ConnectionString, x => x.MigrationsAssembly("Templatez.Infra.Data.Core"));
            return new AppDbContext(builder.Options, _dbOptions);
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbOptions.Value.PostgreSQL.ConnectionString, x => x.MigrationsAssembly("Templatez.Infra.Data.Core"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}