namespace ecom.Dto.Category
{
    public class CreateCategoryDto
    {
        public int hiddenId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
