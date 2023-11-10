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
        async Task resultColorAsync(bool b)
        {
            if (b)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;
        }
        public async Task<string> CreateAsync(string title, string description, int pageCount, BookGenre bookGenre)
        {
            Console.ResetColor();
            if (string.IsNullOrWhiteSpace(title))
            {
                await resultColorAsync(true);
                return "Title can't be empty!";
            }

            if (pageCount <= 0)
            {
                await resultColorAsync(true);
                return "Page count can't be zero or below!";
            }

            BooksModel book = new BooksModel(title, description, pageCount, bookGenre);
            book.CreatedAt = DateTime.UtcNow.AddHours(4);

            await booksRepository.AddAsync(book);
            await resultColorAsync(false);
            return "Book Created";
        }

        public async Task<string> GetAllAsync()
        {
            Console.ResetColor();
            await Console.Out.WriteLineAsync("Do you want a specific list?");
            await Console.Out.WriteLineAsync("1.Yes");
            await Console.Out.WriteLineAsync("2.No");
            int.TryParse(Console.ReadLine(), out int condition);
            if (condition == 1)
            {
                await Console.Out.WriteLineAsync("Input Id:");
                string? id = Console.ReadLine();
                List<BooksModel> books = await booksRepository.GetAllAsync(x => Convert.ToInt32(x.Id.Substring(2)) <= Convert.ToInt32(id.Substring(2)));
                if (books.Count == 0)
                {
                    await resultColorAsync(true);
                    return "No books found";
                }

                foreach (BooksModel book in books)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    await Console.Out.WriteLineAsync($"Id:{book.Id} Title:{book.Title} Description:{book.Description} Page Count:{book.PageCount} Genre:{book.BookGenre} CreatedAt:{book.CreatedAt} UpdatedAt:{book.UpdatedAt}");
                }
                Console.ResetColor();

                await resultColorAsync(false);
                return $"\n{books.Count} books found";
            }
            else if (condition == 2)
            {
                List<BooksModel> books = await booksRepository.GetAllAsync();
                if (books.Count == 0)
                {
                    await resultColorAsync(true);
                    return "No books found";
                }

                foreach (BooksModel book in books)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    await Console.Out.WriteLineAsync($"Id:{book.Id} Title:{book.Title} Description:{book.Description} Page Count:{book.PageCount} Genre:{book.BookGenre} CreatedAt:{book.CreatedAt} UpdatedAt:{book.UpdatedAt}");
                }
                Console.ResetColor();

                await resultColorAsync(false);
                return $"\n{books.Count} books found";
            }

            await resultColorAsync(true);
            return "Invalid option!";

        }

        public async Task<string> GetByIdAsync(string id)
        {
            Console.ResetColor();
            BooksModel book = await booksRepository.GetAsync(x => x.Id == id);
            if (book == null)
            {
                await resultColorAsync(true);
                return "Book not found";
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            await Console.Out.WriteLineAsync($"Id:{book.Id} Title:{book.Title} Description:{book.Description} Page Count:{book.PageCount} Genre:{book.BookGenre} CreatedAt:{book.CreatedAt} UpdatedAt:{book.UpdatedAt}");
            Console.ResetColor();

            await resultColorAsync(false);
            return "Book found successfully";
        }

        public async Task<string> RemoveAsync(string id)
        {
            Console.ResetColor();
            BooksModel book = await booksRepository.GetAsync(x => x.Id == id);
            if (book == null)
            {
                await resultColorAsync(true);
                return "Book not found";
            }

            await booksRepository.RemoveAsync(book);
            await resultColorAsync(false);
            return "Book removed";
        }

        public async Task<string> UpdateAsync(string id, string title, string description, int pageCount, BookGenre bookGenre)
        {
            Console.ResetColor();
            BooksModel book = await booksRepository.GetAsync(x => x.Id == id);
            if (book == null)
            {
                await resultColorAsync(true);
                return "Book not found";
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                await resultColorAsync(true);
                return "Title can't be empty!";
            }
            if (pageCount <= 0)
            {
                await resultColorAsync(true);
                return "Page count can't be zero or below!";
            }

            book.Title = title;
            book.Description = description;
            book.PageCount = pageCount;
            book.BookGenre = bookGenre;
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await resultColorAsync(false);
            return "Book Updated";
        }
    }
}
