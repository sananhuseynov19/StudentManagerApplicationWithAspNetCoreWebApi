using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace StudentManagerApplicationWithWebApi.Repository
{
    public interface IRepository<TEntity> where TEntity : class

    {
        public Task<ICollection<TEntity>> GetAll();
        public Task<TEntity> GetById(int id);
        public Task<TEntity> Create(TEntity entity);
        public Task<TEntity> Update(TEntity entity);
        public Task<TEntity> GetByName(string Name);
        public Task Delete(int id);
        public void Commit();
        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
    }
}