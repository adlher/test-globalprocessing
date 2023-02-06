using Gp.Test.Api.DTO;
using Gp.Test.Entity;
using Gp.Test.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GP.Test.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository testRepository;

        public TestController(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<PersonaDTO>?> Get([Required] Guid id)
        {
            var persona = await testRepository.GetById(id);

            if (persona == null)
                return null;

            return MapEntityToDto(persona);
        }

        [HttpGet("GetAll")]
        public ActionResult<List<PersonaDTO>>? GetAll([FromQuery] PersonaRequestDTO input)
        {
            var query = testRepository.GetAll();
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (input == null)
                return query.Select(MapEntityToDto).ToList();

            if (input.Id.HasValue)
                query = query.Where(x => x.Id == input.Id);

            if (!string.IsNullOrEmpty(input.NombreCompleto))
                query = query.Where(x => x.NombreCompleto == input.NombreCompleto);

            if (!string.IsNullOrEmpty(input.Edad))
                query = query.Where(x => x.Edad == input.Edad);

            if (!string.IsNullOrEmpty(input.Domicilio))
                query = query.Where(x => x.Domicilio == input.Domicilio);

            if (!string.IsNullOrEmpty(input.Telefono))
                query = query.Where(x => x.Telefono == input.Telefono);

            if (!string.IsNullOrEmpty(input.Profesion))
                query = query.Where(x => x.Profesion == input.Profesion);

            if (!string.IsNullOrEmpty(input.DNI))
                query = query.Where(x => x.DNI == input.DNI);

            return query.Select(MapEntityToDto).ToList();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<PersonaDTO>?> Create([Required] PersonaDTO input)
        {
            var query = testRepository.GetAll();
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var exists = await query.AnyAsync(x => x.DNI == input.DNI && x.NombreCompleto == input.NombreCompleto);

            if (exists)
                throw new Exception($"Ya existe la persona {input.NombreCompleto} con DNI: {input.DNI}");

            var entity = await testRepository.Insert(MapDtoToEntity(input));

            return MapEntityToDto(entity);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<PersonaDTO>?> Update([Required] PersonaDTO input)
        {
            var entity = await testRepository.Update(MapDtoToEntity(input));

            return MapEntityToDto(entity);
        }

        private PersonaDTO MapEntityToDto(Persona entity)
        {
            return new PersonaDTO()
            {
                Id = entity.Id,
                DNI = entity.DNI!,
                Domicilio = entity.Domicilio!,
                Edad = entity.Edad!,
                NombreCompleto = entity.NombreCompleto!,
                Profesion = entity.Profesion,
                Telefono = entity.Telefono
            };
        }

        private Persona MapDtoToEntity(PersonaDTO dto)
        {
            return new Persona()
            {
                Id = dto.Id ?? Guid.Empty,
                DNI = dto.DNI,
                Domicilio = dto.Domicilio,
                Edad = dto.Edad,
                NombreCompleto = dto.NombreCompleto,
                Profesion = dto.Profesion,
                Telefono = dto.Telefono
            };
        }
    }
}
