namespace ecom.Models.ViewModel
{
    public class KhaltiPaymentSuccessVM
    {
        public string pidx { get; set; }
        public string transaction_id { get; set; }
        public int amount { get; set; }
        public string purchase_order_id { get; set; }
        public string purchase_order_name { get; set; }
        public string status { get; set; }
        public string info { get; set; }

    }
}
