using Moodpet.Domain.Entities;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Application.Service
{
    public class RecomendacionService
    {
        private readonly IRecomendacionRepository _recomendacionRepository;
        private readonly Random _random = new Random();

        public RecomendacionService(IRecomendacionRepository recomendacionRepository)
        {
            _recomendacionRepository = recomendacionRepository;
        }
        public async Task<HistorialRecomendaciones> GenerarRecomendacionPorMascota(int mascotaId)
        {
            var mascota = await _recomendacionRepository.GetMascotaConRazaYEspecie(mascotaId);

            if (mascota == null)
            {
                throw new ArgumentException($"No se encontró la mascota con el ID {mascotaId}");
            }

            if (mascota.Raza == null)
            {
                throw new ArgumentException("La mascota no tiene una raza asignada.");
            }

            var plantillas = await _recomendacionRepository.GetPlantillasPorRaza(mascota.RazaId);

            if (!plantillas.Any())
            {
                var especieId = mascota.Raza.EspecieId;
                plantillas = await _recomendacionRepository.GetPlantillasPorEspecie(especieId);
            }

            if (!plantillas.Any())
            {
                throw new ArgumentException("No hay recomendaciones disponibles para esta raza o especie.");
            }

            var plantillaSeleccionada = plantillas[_random.Next(plantillas.Count)];

            var recomendacion = new HistorialRecomendaciones
            {
                MascotaId = mascota.Id,
                Tipo = plantillaSeleccionada.Tipo,
                Descripcion = plantillaSeleccionada.Descripcion,
                FechaGeneracion = DateTime.Now
            };

            return await _recomendacionRepository.CrearRecomendacion(recomendacion);
        }

        public async Task<List<HistorialRecomendaciones>> GetHistorialPorMascota(int mascotaId)
        {
            return await _recomendacionRepository.GetHistorialPorMascota(mascotaId);
        }





    }
}
