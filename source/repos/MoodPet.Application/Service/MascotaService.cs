using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces;
using MoodPet.Domain.Interfaces.General;
using MoodPet.Domain.Interfaces.Repositorio;
using MoodPet.Infraestructure.Percistencia.Repositorios;

namespace MoodPet.Application.Service
{
    public class MascotaService
    {
        public readonly IMascotaRepository _mascota;
        public readonly UserRepository _userRepository;
        public readonly IRazaRepository _razaRepository;
        public MascotaService(IMascotaRepository mascota, UserRepository user, IRazaRepository raza)
        {
            _mascota = mascota;
            _userRepository = user;
            _razaRepository = raza;
        }

        public async Task<string> CreateAsync(Mascota animal) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var Usuario = await _userRepository.Userr(animal.UsuarioId);
            if (Usuario == null)
            {
                return $"Usario con id: {animal.UsuarioId} no existe. Digite un usuario valido";
            }
            var Id_raza = animal.RazaId;
            var existe = await _razaRepository.FindAsync(Id_raza);
            if (existe != null)
            {
                await _mascota.AddAsync(animal);
                return $"Creacion de la mascota con id {animal.Id} completada";
            }
            return $"Raza con id: {Id_raza} no existe";
        }

        public async Task<string> DeleteById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var raza = await _mascota.Delete(id);
            if (raza == false)
            {
                throw new ArgumentException($"No se encontro la mascota con el id {id}");
            }
            return $"Eliminacion de la mascota con id {id} completada";
        }

        public async Task<Mascota> GetByID(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var raza = await _mascota.FindAsync(id);
            if (raza == null)
            {
                throw new ArgumentException($"No se encontro la mascota con el id {id}");
            }
            return raza; // Te devuelve un objeto tipo raza (No se si es necesario pasarlo a string)
        }

        public async Task<IEnumerable<Mascota>> GetAllMascotasByUsuario(Guid id)
        {
            var mascotas = await _mascota.GetAllAsyncbyUsuario(id); // Te devuelve una lista de objetos tipo mascota (No se si es necesario pasarlo a string)
            if (mascotas == null)
            {
                throw new ArgumentException("No se encontraron mascotas");
            }
            return mascotas;
        }

        public async Task<Mascota> GetMascotaByUsuario(Guid id)
        {
            var mascotas = await _mascota.FindAsyncByUsuario(id); // Te devuelve una lista de objetos tipo mascota (No se si es necesario pasarlo a string)
            if (mascotas == null)
            {
                throw new ArgumentException("No se encontraro a mascota");
            }
            return mascotas;
        }

        public async Task<String> UpdateMascota(Mascota mascota)
        {
            var UpdateMascota = await _mascota.UpdateMascota(mascota);
            if (UpdateMascota == null)
            {
                throw new ArgumentException($"No se encontro la mascota con el id {mascota.Id}");
            }

            return $"Actualizacion de la mascota con id {mascota.Id} completada";
        }
    }
}
