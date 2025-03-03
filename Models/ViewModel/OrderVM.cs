namespace ecom.Models.ViewModel
{
    public class OrderVM
    {
        public List<ProductOrderDetail> detail { get; set; }
        public List<ProductOrderDetailVM> dtl { get; set; }
        public ProductOrderMaster mast { get; set; }
    }
}
