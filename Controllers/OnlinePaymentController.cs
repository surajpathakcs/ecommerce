using ecom.DAO;
using ecom.Models;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace ecom.Controllers
{
    public class OnlinePaymentController : Controller
    {
        private ApplicationDbContext _db;
        public OnlinePaymentController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet, HttpPost]
        public IActionResult Success(string pidx,string transaction_id, string purchase_order_id,int amount,string status ,string purchase_order_name)
        {
            int orderId = int.Parse(purchase_order_id);

            var success = new KhaltiPaymentSuccessVM
            {

                pidx = pidx,
                amount = amount / 100, // Convert to proper amount (Khalti sends in paisa)
                purchase_order_id = purchase_order_id,
                status = status,
                purchase_order_name = purchase_order_name,
            };
            var DBContent = _db.ProductOrderMaster.Where(x => x.ProductOrderMasterID == orderId).FirstOrDefault();
            DBContent.PaymentStatus = status;
            _db.SaveChanges();
            return View(success);
        }

        [HttpPost]
        [Route("KhaltiPaymentInitiate")]
        public async Task<object> KhaltiPaymentInitiate([FromBody] KhaltiPaymentInitVM vm)
        {

            string khalti_key = "23c3db9b228f4b88ac1ca9c2ecfa3b95";
            var url = "https://dev.khalti.com/api/v2/epayment/initiate/";

            var payload = new
            {
                return_url = vm.RedirectUrl + "/OnlinePayment/Success",
                website_url = vm.RedirectUrl,
                amount = vm.Amount * 100,
                purchase_order_id = vm.PurchaseOrderId,
                purchase_order_name = Guid.NewGuid().ToString(),
                merchant_info = new
                {
                    name = vm.FullName,
                    email = vm.Email
                },
                customer_info = new
                {
                    name = "Himalayan Colz Ecom",
                    address = "Lalitpur "
                },
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var client = new HttpClient();


            client.DefaultRequestHeaders.Add("Authorization", "key " + khalti_key);

            var response = await client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = "OK",
                Data = responseContent
            });
        }


        public IActionResult Index(int orderId)
        {
            var orderMaster = _db.ProductOrderMaster.FirstOrDefault(o => o.ProductOrderMasterID == orderId);
            if (orderMaster == null)
            {
                return NotFound("Order not found.");
            }
            var orderDetails = _db.ProductOrderDetail
                                 .Where(d => d.ProductOrderMasterID == orderId)
                                 .Select(d => new ProductOrderDetailVM
                                 {
                                     ProductOrderDetailID = d.ProductOrderDetailID,
                                     ProductItemId = d.ProductItemId,
                                     ProductName = d.ProductItem.ProductItemName, // Get ProductName from ProductItem
                                     UnitPrice = d.UnitPrice,
                                     Quantity = d.Quantity,
                                     TotalPrice = d.UnitPrice * d.Quantity,
                                     ItemCount = d.Quantity
                                 })
                                 .ToList();

            var orderVM = new OrderVM
            {
                mast = orderMaster,
                dtl = orderDetails
            };

            return View(orderVM);
        }


    }
}
