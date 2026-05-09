using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Interfaces.General;
using MoodPet.Domain.Entities;


namespace MoodPet.Application.Service
{
    public class EspecieService
    {
        public readonly IEspecieRepository _especie;
        public EspecieService(IEspecieRepository especie) 
        {
            _especie = especie;       
        }

        public async Task<IEnumerable<Especie>> GetAllEspecies()
        {
           return await _especie.GetAllAsync(); // Te devuelve una lista de objetos tipo especie (No se si es necesario pasarlo a string)
           
        }

        public async Task<Especie> GetByID(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var especie = await _especie.FindAsync(id);
            if (especie == null)
            {
                  throw new ArgumentException($"No se encontro la especie con el id {id}");
            }
            return especie; // Te devuelve un objeto tipo especie (No se si es necesario pasarlo a string)
        }

        public async Task<string> DeleteById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var especie = await _especie.Delete(id);
            if (especie == false)
            {
                throw new ArgumentException($"No se encontro la especie con el id {id}");
            }
            return $"Eliminacion de la especie con id {id} completada";
        }

        public async Task<string> UpdateEspecieAsync(Especie valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var Id_especie = valor.Id;
            var existe = await _especie.FindAsync(Id_especie);
            if (existe != null)
            {
                var result = await _especie.UpdateAsync(valor);
                return $"Actualizacion de la especie con id {Id_especie} completada";
            }
            return $"Especie con id: {Id_especie} no existe";
        }


        public async Task<string> CreateSpeciesAsync(Especie valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var Id_especie = valor.Id;
            var existe = await _especie.FindAsync(Id_especie);
            if (existe == null)
            {
                var especimen = await _especie.AddAsync(valor);
                return $"Creacion de la especie con id {especimen.Id} completada";
            }
            return $"Especie con id: {Id_especie} ya existe";
        }


    }
}
