using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Domain.Interfaces.Repositorio
{
    public interface ITareaRepository
    {
        Task<TareaDiaria> AddAsync(TareaDiaria entity);

        Task<TareaDiaria> FindAsync(int id);

        Task<bool> Delete(int id);

        Task<TareaDiaria> FindAsyncByMascota(int id);

        Task<List<TareaDiaria>> GetAllAsyncbyMascota(int MascotId);


    }
}
