using Gp.Test.Entity;
using Gp.Test.Interface.Repositories;
using Microsoft.Extensions.Configuration;

namespace Gp.Test.Repository
{
    public class TestRepository : ITestRepository
    {
        IConfiguration configuration;
        private readonly TestContext _dbContext;

        public TestRepository(TestContext context, IConfiguration configuration)
        {
            _dbContext = context;
            this.configuration = configuration;
        }

        public IQueryable<Persona>? GetAll()
        {
            return _dbContext.Personas.AsQueryable();
        }

        public async Task<Persona?> GetById(Guid id)
        {
            return await _dbContext.Personas.FindAsync(id);
        }

        public async Task<Persona> Insert(Persona? persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            _dbContext.Personas.Add(persona);
            await _dbContext.SaveChangesAsync();
            return persona;
        }

        public async Task<Persona> Update(Persona? persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            _dbContext.Personas.Update(persona);
            await _dbContext.SaveChangesAsync();
            return persona;
        }
    }
}