using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.DataAcces;
using personapi_dotnet.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace personapi_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaDAO _personaDAO;

        public PersonaController(PersonaDAO personaDAO)
        {
            _personaDAO = personaDAO;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las personas.")]
        [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(List<Persona>))]
        public async Task<ActionResult<List<Persona>>> ObtenerTodas()
        {
            var personas = await _personaDAO.ObtenerTodasAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una persona por su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(Persona))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No encontrado")]
        public async Task<ActionResult<Persona>> ObtenerPorId(int id)
        {
            var persona = await _personaDAO.ObtenerPorIdAsync(id);
            if (persona == null)
                return NotFound();

            return Ok(persona);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva persona.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Creado", typeof(Persona))]
        public async Task<ActionResult<Persona>> Crear(Persona persona)
        {
            await _personaDAO.CrearAsync(persona);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = persona.Id }, persona);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza los datos de una persona existente.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Sin contenido")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitud incorrecta")]
        public async Task<IActionResult> Actualizar(int id, Persona persona)
        {
            if (id != persona.Id)
                return BadRequest();

            await _personaDAO.ActualizarAsync(persona);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una persona por su ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Sin contenido")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _personaDAO.EliminarAsync(id);
            return NoContent();
        }
    }
}
