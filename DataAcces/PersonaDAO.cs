using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models;

namespace personapi_dotnet.DataAcces
{
    public class PersonaDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonaDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Persona>> ObtenerTodasAsync()
        {
            return await _dbContext.Set<Persona>().ToListAsync();
        }

        public async Task<Persona> ObtenerPorIdAsync(int id)
        {
            return await _dbContext.Set<Persona>().FindAsync(id);
        }

        public async Task<int> CrearAsync(Persona persona)
        {
            _dbContext.Set<Persona>().Add(persona);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> ActualizarAsync(Persona persona)
        {
            _dbContext.Entry(persona).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(int id)
        {
            var persona = await _dbContext.Set<Persona>().FindAsync(id);
            if (persona == null)
                return 0;

            _dbContext.Set<Persona>().Remove(persona);
            return await _dbContext.SaveChangesAsync();
        }
    }

}
