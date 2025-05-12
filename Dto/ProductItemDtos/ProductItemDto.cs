namespace ecom.Dto.ProductItemDtos
{
    public class ProductItemDto
    {
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public string ProductItemCode { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string Thumbnail { get; set; }
    }
}
