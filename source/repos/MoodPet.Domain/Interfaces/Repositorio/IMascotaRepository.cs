using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces.Repositorio
{
    public interface IMascotaRepository
    {

        Task<Mascota> AddAsync(Mascota entity);

        Task<Mascota> FindAsync(int id);

        Task<bool> Delete(int id);

        Task<Mascota> FindAsyncByUsuario(string id);

        Task<List<Mascota>> GetAllAsyncbyUsuario(string Usuario_id);

        Task<Mascota> UpdateMascota(Mascota mascota);

    }
}
