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

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> FindAsync(int id);

        Task<IQueryable<TEntity>> FindWhere(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);


        Task<bool> Delete(int id);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);


    }
}
