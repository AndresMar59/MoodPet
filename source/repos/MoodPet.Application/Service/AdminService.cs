using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Application.Service
{
    public class AdminService
    {
        public readonly IEspecieRepository _especie;
        public readonly IRazaRepository _raza;
        public AdminService(IEspecieRepository especie, IRazaRepository raza)
        {
            _especie = especie;
            _raza = raza;
        }



        public async Task<IEnumerable<Raza>> GetAllRaza()
        {
            return await _raza.GetAllAsync(r => r.IsDeleted == false); // Te devuelve una lista de objetos tipo raza (No se si es necesario pasarlo a string)
        }

        public async Task<Raza> GetRazaByID(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var raza = await _raza.FindAsync(id);
            if (raza == null)
            {
                throw new ArgumentException($"No se encontro la raza con el id {id}");
            }
            return raza; // Te devuelve un objeto tipo raza (No se si es necesario pasarlo a string)
        }

        //Encuentra una raza por su id, pero solo si no esta eliminada, ademas de incluir la especie a la que pertenece
        public async Task<Raza> FindRazaByIdAsync(int razaId)
        {
            var raza = await _raza.FindFirstOrDefaultAsync(r => r.Id == razaId && !r.IsDeleted, r => r.Especie);
            if (raza == null)
            {
                throw new ArgumentException($"No se encontro la raza con el id {razaId}");
            }
            return raza;
        }

        public async Task<string> UpdateRazaAsync(Raza valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var Id_raza = valor.Id;
            var existe = await _raza.FindAsync(Id_raza);
            if (existe != null)
            {
                var result = await _raza.UpdateAsync(valor);
                return $"Actualizacion de la raza con id {Id_raza} completada";
            }
            return $"Raza con id: {Id_raza} no existe";
        }
        public async Task<string> DeleteRazaById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var raza = await _raza.Delete(id);
            if (raza == false)
            {
                throw new ArgumentException($"No se encontro la raza con el id {id}");
            }
            return $"Eliminacion de la raza con id {id} completada";
        }


        public async Task<string> CreateRazaAsync(Raza valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var especie = await _especie.FindAsync(valor.EspecieId);
            if (especie == null)
            {
                return $"Especie con id: {valor.EspecieId} no existe. Digite una especie valida";
            }
            var Id_raza = valor.Id;
            var existe = await _raza.FindAsync(Id_raza);
            if (existe == null)
            {
                var newraza = await _raza.AddAsync(valor);
                return $"Creacion de la raza con id {newraza.Id} completada";
            }
            return $"Raza con id: {Id_raza} ya existe";
        }







        //////////////////////////////////////                    ESPECIES                    /////////////////////////////////////////





        public async Task<IEnumerable<Especie>> GetAllEspecies()
            {
                return await _especie.GetAllAsync(e => e.IsDeleted == false); // Te devuelve una lista de objetos tipo especie (No se si es necesario pasarlo a string)

            }

            public async Task<Especie> GetEspecieByID(int id) // La validacion del tipo de entrada deberia ser en la terminal
            {
                var especie = await _especie.FindAsync(id);
                if (especie == null)
                {
                    throw new ArgumentException($"No se encontro la especie con el id {id}");
                }
                return especie; // Te devuelve un objeto tipo especie (No se si es necesario pasarlo a string)
            }

            public async Task<string> DeleteEspecieById(int id) // La validacion del tipo de entrada deberia ser en la terminal
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
