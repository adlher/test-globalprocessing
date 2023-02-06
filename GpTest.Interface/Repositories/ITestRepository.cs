using Gp.Test.Entity;

namespace Gp.Test.Interface.Repositories
{
    public interface ITestRepository
    {
        IQueryable<Persona>? GetAll();
        Task<Persona?> GetById(Guid id);
        Task<Persona> Insert(Persona persona);
        Task<Persona> Update(Persona persona);
    }   
}
