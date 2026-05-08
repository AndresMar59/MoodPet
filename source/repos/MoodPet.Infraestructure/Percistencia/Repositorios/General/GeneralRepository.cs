using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Infraestructure.Percistencia.Repositorios.General
{
    public abstract class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : GeneralEntity
    {

        protected readonly ApplicationDbContext _context;
        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            if (result == 0 || entity.Id == 0)
            {
                return null;
            }
            return entity; 
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return false;

            entity.IsDeleted = true;

            var result = await _context.SaveChangesAsync();
            return result >= 1; ;
        }

        public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> Predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(Predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
    }
}
