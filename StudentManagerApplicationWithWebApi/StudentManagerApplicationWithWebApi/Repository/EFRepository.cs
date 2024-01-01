using Microsoft.EntityFrameworkCore;
using StudentManagerApplicationWithWebApi.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace StudentManagerApplicationWithWebApi.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity :class
    {
      private readonly  AppDbContext _appDbContext;

        public EFRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            await _appDbContext.Set<TEntity>().AddAsync(entity);
          await   _appDbContext.SaveChangesAsync();
            return entity;

        }

        public async Task Delete(int id)
        {
            var ent = await _appDbContext.Set<TEntity>().FindAsync(id);
            _appDbContext.Set<TEntity>().Remove(ent);
           await  _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            var ents = await _appDbContext.Set<TEntity>().ToListAsync();
            return ents;
        }

        public async void Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _appDbContext.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public async Task<TEntity> GetById(int id)
        {
          var ent=await _appDbContext.Set<TEntity>().FindAsync(id);
            return ent;
        }

        public async Task<TEntity> GetByName(string Name)
        {
            var entity = await _appDbContext.Set<TEntity>().FindAsync(Name);
            return entity;
        }
    }
}
