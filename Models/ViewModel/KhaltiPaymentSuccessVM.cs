namespace ecom.Models.ViewModel
{
    public class KhaltiPaymentSuccessVM
    {
        public string Pidx { get; set; }
        public string payment_url { get; set; }
        public string expires_at { get; set; }
        public int expires_in { get; set; }
    }
}
