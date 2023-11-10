
using Library.Core.Models.BaseModels;
using Library.Core.Repositories;

namespace Library.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        List<T> _values = new List<T>();
        public async Task AddAsync(T entity)
        {
            _values.Add(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return _values;
        }

        public async Task<List<T>> GetAllAsync(Func<T, bool> func)
        {
            return _values.Where(func).ToList();
        }

        public async Task<T> GetAsync(Func<T, bool> func)
        {
            return _values.FirstOrDefault(func);
        }

        public async Task RemoveAsync(T entity)
        {
            _values.Remove(entity);
        }
    }
}
