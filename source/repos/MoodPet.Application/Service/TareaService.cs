using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;

namespace MoodPet.Application.Service
{
    public class TareaService
    {
        public readonly ITareaRepository _tarea;
        public readonly IHistorialRepository _historial;
        public readonly IMascotaRepository _mascota;
        public TareaService(ITareaRepository tarea, IHistorialRepository historial, IMascotaRepository mascota)
        {
            _historial = historial;
            _mascota = mascota;
            _tarea = tarea;
        }

        public async Task<string> CreateAsync(TareaDiaria tarea) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var Mascota = await _mascota.FindAsync(tarea.MascotaId);
            if (Mascota == null)
            {
                return $"Mascota con id: {tarea.MascotaId} no existe. Digite una mascota valida";
            }

            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            if (tarea.Fecha < fechaActual)
            {
                return $"La fecha de la tarea no puede ser anterior a la fecha actual. Digite una fecha valida";
            }

            var semanas = tarea.Semanas;
            if (semanas > 12 || semanas < 1)
            {
                return $"eEl rango se semanas permitidas son entre 1-12. Faovr ingresa un numero de semanas en el rago permitido";
            }

            var estado = tarea.Recurrente;
            if (estado == true)
            {
                var semnas = tarea.Semanas;

                for (int i = 0; i < semnas; i++)
                {
                    var historial = new HistorialTarea
                    {
                        TareaId = tarea.Id,
                        Fecha = tarea.Fecha.AddDays(i * 7),
                        Hora = tarea.Hora
                    };

                    await _historial.AddAsync(historial);
                }

                await _tarea.AddAsync(tarea);
                return $"Creacion de la tarea con id {tarea.Id} completada";
            }

            var historial1 = new HistorialTarea
            {
                TareaId = tarea.Id,
                Fecha = tarea.Fecha,
                Hora = tarea.Hora
            };

            await _historial.AddAsync(historial1);
            await _tarea.AddAsync(tarea);
            return $"Creacion de la tarea con id {tarea.Id} completada";
        }

        public async Task<TareaDiaria> GetById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var tarea = await _tarea.FindAsync(id);
            if (tarea == null)
            {
                throw new ArgumentException($"No se encontro la tarea con el id {id}");
            }
            return tarea;
        }

        public async Task<TareaDiaria> FindByMascota(int id)
        {
            var tarea = await _tarea.FindAsyncByMascota(id);
            if (tarea == null)
            {
                throw new ArgumentException($"No se encontro la tarea con el id de mascota {id}");
            }
            return tarea;
        }


        public async Task<List<TareaDiaria>> GetAllbyMascot(int Id)
        {
            var tarea = await _tarea.GetAllAsyncbyMascota(Id);
            if (tarea == null)
            {
                throw new ArgumentException($"No se encontro la tarea con el id de mascota {Id}");
            }
            return tarea;
        }

        public async Task<bool> CompleteTareaBySemana(int Semana, int Id)

        {
            var historial = await _historial.GetAllAsyncbyTarea(Id);
            if (Semana > historial.Count)
            {
                throw new ArgumentException($"La semana {Semana} no existe para la tarea con id {Id}");
            }

            var tarea = historial[Semana - 1];
            var fecha = tarea.Fecha;
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            if (fecha > fechaActual)
            {
                throw new ArgumentException($"No se puede completar la tarea antes de su fecha programada. La fecha programada es {fecha}");
            }
            else if (tarea.Completada == true)
            {
                throw new ArgumentException($"La tarea de la semana {Semana} ya ha sido completada");
            }

            var completeTarea = await _historial.Complete(tarea.Id);
            return completeTarea;
        }

        public async Task<bool> CompleteTareaByHistorial(int Id_Historial, int Id_Tarea)
        {
            var tarea = await _tarea.FindAsync(Id_Tarea);
            if (tarea == null)
            {
                throw new ArgumentException($"No se encontro la tarea con el id {Id_Tarea}");
            }

            var historial = await _historial.FindAsync(Id_Historial);
            if (historial == null)
            {
                throw new ArgumentException($"No se encontro la tarea con el id {Id_Historial}");
            }

            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            var fechaTarea = historial.Fecha;
            if (fechaTarea > fechaActual)
            {
                throw new ArgumentException($"No se puede completar la tarea antes de su fecha programada. La fecha programada es {fechaTarea}");
            }
            return await _historial.Complete(Id_Historial);

        }

        public async Task<string> deleteById(int id) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var tarea = await _tarea.Delete(id);
            if (tarea == false)
            {
                throw new ArgumentException($"No se encontro la tarea con el id {id}");
            }

            var historial = await _historial.GetAllAsyncbyTarea(id);

            for (var i = 0; i < historial.Count; i++)
            {
                await _historial.Delete(historial[i].Id);
            }

            return $"Eliminacion de la tarea con id {id} completada";

        }

        public async Task<List<HistorialTarea>> GetHistorialByTarea(int valor) // La validacion del tipo de entrada deberia ser en la terminal
        {
            var existe = await _historial.GetAllAsyncbyTarea(valor);
            if (existe != null)
            {

                throw new ArgumentException($"No se encontro la tarea con el id {valor}");
            }
            return existe;
        }
    }
}

