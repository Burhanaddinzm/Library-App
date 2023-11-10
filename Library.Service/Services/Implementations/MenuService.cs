using Library.Core.Enums;
using Library.Service.Services.Interfaces;
using System.ComponentModel.Design;

namespace Library.Service.Services.Implementations
{
    public class MenuService
    {
        IBooksService booksService = new BooksService();
        public async Task RunApp()
        {
            await GetMenuAsync();
            string? option = Console.ReadLine();
            while (option != "0")
            {
                switch (option)
                {
                    case "1":
                        await CreateBookAsync();
                        break;
                    case "2":
                        await GetAllBooksAsync();
                        break;
                    case "3":
                        await GetBookAsync();
                        break;
                    case "4":
                        await RemoveBookAsync();
                        break;
                    case "5":
                        await UpdateBookAsync();
                        break;
                    default:
                        await Console.Out.WriteLineAsync("Invalid option!");
                        break;
                }
                await GetMenuAsync();
                option = Console.ReadLine();
            }
        }

        async Task CreateBookAsync()
        {
            await Console.Out.WriteLineAsync("Input book title:");
            string? bookTitle = Console.ReadLine();
            await Console.Out.WriteLineAsync("Input book description:");
            string? bookDescription = Console.ReadLine();
            await Console.Out.WriteLineAsync("Input page count:");
            int.TryParse(Console.ReadLine(), out int pageCount);

            int i = 1;
            foreach (var item in Enum.GetValues(typeof(BookGenre)))
            {
                await Console.Out.WriteLineAsync($"{i}.{item}");
                i++;
            }

            await Console.Out.WriteLineAsync("Choose book genre:");
            int.TryParse(Console.ReadLine(), out int enumIndex);
            bool isExist = Enum.IsDefined(typeof(BookGenre), (BookGenre)enumIndex);
            while (!isExist)
            {
                await Console.Out.WriteLineAsync("Choose book genre:");
                int.TryParse(Console.ReadLine(), out enumIndex);
                isExist = Enum.IsDefined(typeof(BookGenre), (BookGenre)enumIndex);
            }

            string result = await booksService.CreateAsync(bookTitle, bookDescription, pageCount, (BookGenre)enumIndex);
            await Console.Out.WriteLineAsync(result);
        }

        async Task UpdateBookAsync()
        {
            await Console.Out.WriteLineAsync("Input Id:");
            string? bookId = Console.ReadLine();
            await Console.Out.WriteLineAsync("Input book title:");
            string? bookTitle = Console.ReadLine();
            await Console.Out.WriteLineAsync("Input book description:");
            string? bookDescription = Console.ReadLine();
            await Console.Out.WriteLineAsync("Input page count:");
            int.TryParse(Console.ReadLine(), out int pageCount);

            int i = 1;
            foreach (var item in Enum.GetValues(typeof(BookGenre)))
            {
                await Console.Out.WriteLineAsync($"{i}.{item}");
                i++;
            }

            await Console.Out.WriteLineAsync("Choose book genre:");
            int.TryParse(Console.ReadLine(), out int enumIndex);
            bool isExist = Enum.IsDefined(typeof(BookGenre), (BookGenre)enumIndex);
            while (!isExist)
            {
                await Console.Out.WriteLineAsync("Choose book genre:");
                int.TryParse(Console.ReadLine(), out enumIndex);
                isExist = Enum.IsDefined(typeof(BookGenre), (BookGenre)enumIndex);
            }

            string result = await booksService.UpdateAsync(bookId, bookTitle, bookDescription, pageCount, (BookGenre)enumIndex);
            await Console.Out.WriteLineAsync(result);
        }

        async Task GetAllBooksAsync()
        {
            string result = await booksService.GetAllAsync();
            await Console.Out.WriteLineAsync(result);
        }

        async Task GetBookAsync()
        {
            await Console.Out.WriteLineAsync("Input Id:");
            string? bookId = Console.ReadLine();
            string result = await booksService.GetByIdAsync(bookId);
            await Console.Out.WriteLineAsync(result);
        }

        async Task RemoveBookAsync()
        {
            await Console.Out.WriteLineAsync("Input Id:");
            string? bookId = Console.ReadLine();
            string result = await booksService.RemoveAsync(bookId);
            await Console.Out.WriteLineAsync(result);
        }

        async Task GetMenuAsync()
        {
            await Console.Out.WriteLineAsync("1.Add Book");
            await Console.Out.WriteLineAsync("2.Show Books");
            await Console.Out.WriteLineAsync("3.Show Book");
            await Console.Out.WriteLineAsync("4.Remove Book");
            await Console.Out.WriteLineAsync("5.Update Book");
            await Console.Out.WriteLineAsync("0.Close Application");
        }
    }
}
