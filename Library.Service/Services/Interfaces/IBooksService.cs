using Library.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Services.Interfaces
{
    public interface IBooksService
    {
        public Task<string> CreateAsync(string title, string description, int pageCount, BookGenre bookGenre);
        public Task<string> UpdateAsync(string id, string title, string description, int pageCount, BookGenre bookGenre);
        public Task<string> GetByIdAsync(string id);
        public Task<string> RemoveAsync(string id);
        public Task<string> GetAllAsync();
    }
}
