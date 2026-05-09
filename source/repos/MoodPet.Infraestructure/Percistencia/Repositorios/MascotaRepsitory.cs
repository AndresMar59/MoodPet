using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;
using MoodPet.Infraestructure.Percistencia.Repositorios.General;

namespace MoodPet.Infraestructure.Percistencia.Repositorios
{
    public class MascotaRepsitory : GeneralRepository<Mascota>, IMascotaRepository
    {

        public readonly ApplicationDbContext _context;
        public MascotaRepsitory(ApplicationDbContext context):
            base(context)
        {
            _context = context;
        }

        public async Task<Mascota> AddAsync(Mascota entity)
        {
            await _context.Set<Mascota>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            if (result == 0 || entity.Id == 0)
            {
                return null;
            }
            return entity; 
        }

        public async Task<Mascota> GetById(int mascotaId)
        {
            return await _context.Mascotas
                .Include(r => r.Raza)
                .ThenInclude(r => r.Especie)
                .Where(r => !r.IsDeleted && r.Id == mascotaId)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<Mascota>().FindAsync(id);
            if (entity == null)
                return false;

            _context.Set<Mascota>().Remove(entity); // Esto si elimina completamente el animal
            var result = await _context.SaveChangesAsync();
            return result >= 1;
        }

        public async Task<Mascota> FindAsync(int id)
        {
            var entity = await _context.Set<Mascota>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        } // Devuelve una tarea segun el id de esta, si no existe devuelve null

        public async Task<Mascota> FindAsyncByUsuario(string id)
        {
            var entity = await _context.Set<Mascota>().FirstOrDefaultAsync(e => e.UserId == id);
            return entity;
        } // Devuelve una tarea segun el id de la mascota, si no existe devuelve null

        public async Task<List<Mascota>> GetAllAsyncbyUsuario(string Usuario_id)
        {
            return await _context.Mascotas
                .Include(r => r.Raza)
                .ThenInclude(r => r.Especie)
                .Where(r => !r.IsDeleted && r.UserId == Usuario_id)
                .ToListAsync();
        }

        public async Task<Mascota> UpdateMascota(Mascota mascota)
        {
            var existingMascota = await FindAsync(mascota.Id);
            if (existingMascota == null)
            {
                return null;
            }

            // Actualizar las propiedades de la mascota existente con los valores de la nueva mascota
            existingMascota.Nombre = mascota.Nombre;
            existingMascota.Peso = mascota.Peso;

            try
            {
                await _context.SaveChangesAsync();
                return existingMascota;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
