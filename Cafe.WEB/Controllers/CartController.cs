using Cafe.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WEB.Controllers
{
    public class CartController : Controller
    {


        public IActionResult IndexQRCode()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ReceiveQrData([FromBody] QrRequest request)
        {
            if (request != null && !string.IsNullOrWhiteSpace(request.QrData))
            {
                // رد يحتوي على رابط لإعادة التوجيه
                return Json(new
                {
                    status = "success",
                    redirectUrl = Url.Action("Index", "MenuCafe"),
                    data = request.QrData
                });
                //return Json(new { status = "success", data = request.QrData });
            }

            return Json(new { status = "error", message = "البيانات المدخلة غير صالحة." });
        }

        public class QrRequest
        {
            public string QrData { get; set; }
        }
      
        public IActionResult Index()
        {
            
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
                return View(cart); 
                //return Json(new { status = "success", data = request.QrData });
       
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult PaymentSuccess()
        {
            HttpContext.Session.Remove("cart");
            return View();
        }
    }
}
