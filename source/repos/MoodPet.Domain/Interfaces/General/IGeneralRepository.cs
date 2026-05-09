using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using MoodPet.Domain.Entities;


namespace MoodPet.Domain.Interfaces.General
{
    public interface IGeneralRepository<TEntity> where TEntity : GeneralEntity
    {
    
    
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity?> FindAsync(int id);

        Task<bool> Delete(int id);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);


    }
}
