using Gp.Test.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gp.Test.Repository.Config
{
    public class PersonaConfig
    {
        public PersonaConfig(EntityTypeBuilder<Persona> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.Id });
            entityBuilder.Property(x => x.NombreCompleto);
            entityBuilder.Property(x => x.Edad);
            entityBuilder.Property(x => x.Domicilio);
            entityBuilder.Property(x => x.Telefono);
            entityBuilder.Property(x => x.Profesion);
            entityBuilder.Property(x => x.DNI);
        }
    }
}
