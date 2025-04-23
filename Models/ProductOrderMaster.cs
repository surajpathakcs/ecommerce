using System.ComponentModel.DataAnnotations;


namespace ecom.Models
{
    public class ProductOrderMaster
    {
        [Key]
        public int ProductOrderMasterID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public decimal GrandTotal { get; set; }
    }
    public class ProductOrderMasterVM
    {
        [Key]
        public int ProductOrderMasterID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal GrandTotal { get; set; }
        public string PaymentStatus { get; set; }  
        public string TransactionId { get; set; }
        public int ItemCount { get; set; }
    }
}
