using Library.Core.Enums;
using Library.Core.Models;
using Library.Core.Repositories;
using Library.Data.Repositories;
using Library.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Service.Services.Implementations
{
    public class BooksService : IBooksService
    {
        IBooksRepository booksRepository = new BooksRepository();
        public async Task<string> CreateAsync(string title, string description, int pageCount, BookGenre bookGenre)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "Title can't be empty!";
            if (pageCount == 0)
                return "Page count can't be zero!";

            BooksModel book = new BooksModel(title, description, pageCount, bookGenre);
            book.CreatedAt = DateTime.UtcNow.AddHours(4);

            await booksRepository.AddAsync(book);
            return "Book Created";
        }

        public async Task<string> GetAllAsync()
        {
            await Console.Out.WriteLineAsync("Do you want a specific list?");
            await Console.Out.WriteLineAsync("1.Yes");
            await Console.Out.WriteLineAsync("2.No");
            int.TryParse(Console.ReadLine(), out int condition);
            if (condition == 1)
            {
                await Console.Out.WriteLineAsync("Input Id:");
                string? id = Console.ReadLine();
                List<BooksModel> books = await booksRepository.GetAllAsync(x => x.Id == id);
                if (books.Count == 0)
                    return "No books found";
                foreach (BooksModel book in books)
                {
                    await Console.Out.WriteLineAsync($"Id:{book.Id} Title:{book.Title} Description:{book.Description} Page Count:{book.PageCount} Genre:{book.BookGenre} CreatedAt:{book.CreatedAt} UpdatedAt:{book.UpdatedAt}");
                }
                return $"\n{books.Count} books found";
            }
            else if (condition == 2)
            {
                List<BooksModel> books = await booksRepository.GetAllAsync();
                if (books.Count == 0)
                    return "No books found";

                foreach (BooksModel book in books)
                {
                    await Console.Out.WriteLineAsync($"Id:{book.Id} Title:{book.Title} Description:{book.Description} Page Count:{book.PageCount} Genre:{book.BookGenre} CreatedAt:{book.CreatedAt} UpdatedAt:{book.UpdatedAt}");
                }
                return $"\n{books.Count} books found";
            }
            return "Invalid option!";

        }

        public async Task<string> GetByIdAsync(string id)
        {
            BooksModel book = await booksRepository.GetAsync(x => x.Id == id);
            if (book == null)
                return "Book not found";

            await Console.Out.WriteLineAsync($"Id:{book.Id} Title:{book.Title} Description:{book.Description} Page Count:{book.PageCount} Genre:{book.BookGenre} CreatedAt:{book.CreatedAt} UpdatedAt:{book.UpdatedAt}");
            return "Book found successfully";
        }

        public async Task<string> RemoveAsync(string id)
        {
            BooksModel book = await booksRepository.GetAsync(x => x.Id == id);
            if (book == null)
                return "Book not found";

            await booksRepository.RemoveAsync(book);
            return "Book removed";
        }

        public async Task<string> UpdateAsync(string id, string title, string description, int pageCount, BookGenre bookGenre)
        {
            BooksModel book = await booksRepository.GetAsync(x => x.Id == id);
            if (book == null)
                return "Book not found";
            if (string.IsNullOrWhiteSpace(title))
                return "Title can't be empty!";
            if (pageCount == 0)
                return "Page count can't be zero!";

            book.Title = title;
            book.Description = description;
            book.PageCount = pageCount;
            book.BookGenre = bookGenre;
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);
            return "Book Updated";
        }
    }
}
