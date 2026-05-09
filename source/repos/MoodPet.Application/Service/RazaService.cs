using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Application.Service
{
    public class RazaService
    {
        public readonly IEspecieRepository _especie;
        public readonly IRazaRepository _raza;
        public RazaService(IEspecieRepository especie, IRazaRepository raza)
        {
            _especie = especie;
            _raza = raza;
        }

        public async Task<IEnumerable<Raza>> GetAllRaza()
        {
            return await _raza.GetAllAsync(); // Te devuelve una lista de objetos tipo raza (No se si es necesario pasarlo a string)
        }

        public async Task<Raza> GetByID(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var raza = await _raza.FindAsync(id);
            if (raza == null)
            {
                throw new ArgumentException($"No se encontro la raza con el id {id}");
            }
            return raza; // Te devuelve un objeto tipo raza (No se si es necesario pasarlo a string)
        }

        public async Task<string> DeleteById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var raza = await _raza.Delete(id);
            if (raza == false)
            {
                throw new ArgumentException($"No se encontro la raza con el id {id}");
            }
            return $"Eliminacion de la raza con id {id} completada";
        }


        public async Task<string> CreateAsync(Raza valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var especie = await _especie.FindAsync(valor.EspecieId);
            if (especie == null)
            {
                return  $"Especie con id: {valor.EspecieId} no existe. Digite una especie valida";
            }
            var Id_raza = valor.Id;
            var existe = await _raza.FindAsync(Id_raza);
            if (existe == null)
            {
                await _raza.AddAsync(valor);
                return $"Creacion de la raza con id {Id_raza} completada";
            }
            return $"Raza con id: {Id_raza} ya existe";
        }

    }
}
