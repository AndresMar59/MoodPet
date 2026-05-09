using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Application.Service
{
    public class TipoEventoService
    {
        public readonly ITipoEventoRepository _Tipo;
        public TipoEventoService(ITipoEventoRepository tipoEvento)
        {
            _Tipo = tipoEvento;
        }

        public async Task<IEnumerable<TipoEvento>> GetAllTipoEvento()
        {
            return await _Tipo.GetAllAsync(); // Te devuelve una lista de objetos tipo especie (No se si es necesario pasarlo a string)
        }

        public async Task<TipoEvento> GetByID(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var tipoEvento = await _Tipo.FindAsync(id);
            if (tipoEvento == null)
            {
                throw new ArgumentException($"No se encontro el tipo de evento con el id {id}");
            }
            return tipoEvento; // Te devuelve un objeto tipo tipo de evento (No se si es necesario pasarlo a string)
        }

        public async Task<string> DeleteById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var tipoEvento = await _Tipo.Delete(id);
            if (tipoEvento == false)
            {
                throw new ArgumentException($"No se encontro el tipo de evento con el id {id}");
            }
            return $"Eliminacion del tipo de evento con id {id} completada";
        }


        public async Task<string> CreateAsync(TipoEvento valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var Id_tipoEvento  = valor.Id;
            var existe = await _Tipo.FindAsync(Id_tipoEvento);
            if (existe == null)
            {
                await _Tipo.AddAsync(valor);
                return $"Creacion del evento con id {Id_tipoEvento} completada";
            }
            return $"Evento con id: {Id_tipoEvento} ya existe";
        }


    }
}
