using baseProject.Domain.Models;
using baseProject.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace baseProject.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            var config = new ContextConfig();
            _configuration.Bind("ConnectionStrings", config);

            options.UseSqlServer($"Server={config.DatabaseServer},{config.DatabasePort};Database={config.DatabaseName};User ID={config.DatabaseUser};Password={config.DatabaseUserPassword};Trusted_Connection=False; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // // configures one-to-many relationship
            // builder.entity<datebudget>()
            // .hasmany(c => c.exits)
            // .withone(e => e.datebudget)
            // .isrequired();

            // builder.entity<datebudget>()
            //.hasmany(c => c.entries)
            //.withone(e => e.datebudget)
            //.isrequired();
        }


        public DbSet<User> Users { get; set; }

    }
}
