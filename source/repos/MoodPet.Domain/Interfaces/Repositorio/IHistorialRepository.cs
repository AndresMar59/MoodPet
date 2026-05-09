using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces.Repositorio
{
    public interface IHistorialRepository
    {
        Task<HistorialTarea> AddAsync(HistorialTarea entity);

        Task<HistorialTarea> FindAsync(int id);

        Task<bool> Delete(int id);

        Task<bool> Complete(int id);

        Task<HistorialTarea> FindAsyncByTarea(int id);

        Task<List<HistorialTarea>> GetAllAsyncbyTarea(int userId);

    }
}
