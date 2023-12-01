using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public decimal Fiyat { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }

    }
}
