using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book BookName { get; set; }

        public int? CategoryId { get; set; }
        public Category? CategoryName { get; set; }


        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategoryName { get; set; }

    }
}
