
using Library.Core.Enums;
using Library.Core.Models.BaseModels;

namespace Library.Core.Models
{
    public class BooksModel : BaseModel
    {

        private static int _id;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }
        public BookGenre BookGenre { get; set; }
        public BooksModel(string title, string description, int pageCount, BookGenre bookGenre)
        {
            _id++;
            Title = title;
            Description = description;
            PageCount = pageCount;
            BookGenre = bookGenre;
            string enumName = BookGenre.ToString();
            Id = $"{enumName[0]}-{_id}";
        }
    }
}
