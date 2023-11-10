
using Library.Core.Models.BaseModels;

namespace Library.Core.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        public Task AddAsync(T entity);
        public Task RemoveAsync(T entity);
        public Task<T> GetAsync(Func<T, bool> func);
        public Task<List<T>> GetAllAsync();
        public Task<List<T>> GetAllAsync(Func<T, bool> func);
    }
}
