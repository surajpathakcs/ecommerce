using System.ComponentModel.DataAnnotations;

namespace ecom.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
