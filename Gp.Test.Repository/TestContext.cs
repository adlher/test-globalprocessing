using Gp.Test.Entity;
using Gp.Test.Repository.Config;
using Microsoft.EntityFrameworkCore;

namespace Gp.Test.Repository
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new PersonaConfig(builder.Entity<Persona>());
        }

        public DbSet<Persona> Personas { get; set; } = default!;
    }
}
