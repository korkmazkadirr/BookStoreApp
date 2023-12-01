using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Models
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
