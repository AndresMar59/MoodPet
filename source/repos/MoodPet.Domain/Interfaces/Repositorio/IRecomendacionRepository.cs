using Moodpet.Domain.Entities;
using MoodPet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Domain.Interfaces.Repositorio
{
    public interface IRecomendacionRepository
    {
            Task<Mascota?> GetMascotaConRazaYEspecie(int mascotaId);

            Task<List<Recomendacion>> GetPlantillasPorRaza(int razaId);

            Task<List<Recomendacion>> GetPlantillasPorEspecie(int especieId);


            Task<HistorialRecomendaciones> CrearRecomendacion(HistorialRecomendaciones recomendacion);

            Task<List<HistorialRecomendaciones>> GetHistorialPorMascota(int mascotaId);
        

    }
}
