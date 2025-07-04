using Cafe.EF.Models;
using Cafe.Services.Generic;
using Cafe.Services.MenuCafe;
using Cafe.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace Cafe.WEB.Controllers
{
    public class MenuCafeController : Controller
    {
        private readonly MenuCafeService MenuCafeServes;

        public MenuCafeController(MenuCafeService _MenuCafeServes)
        {
           
            MenuCafeServes = _MenuCafeServes;
        }

        //    private static List<Category> MenuItems = new List<Category>
        //{

        //        new Category { Id = 1, Name = "أكل" },
        //        new Category { Id = 2, Name = "مشروبات" },
        //        new Category { Id = 3, Name = "حلويات" },
        //        new Category { Id = 4, Name = "مقبلات" }


        //};

        public IActionResult Index()
        {
            //return View(MenuItems);
            return View();
        }

        public IActionResult IndexNew()
        {
            var lst = MenuCafeServes.List();
            return View(lst);
        }

        //public IActionResult AddToCart(int id)
        //{
        //    var item = MenuItems.FirstOrDefault(x => x.Id == id);
        //    if (item != null)
        //    {
        //        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
        //        var existing = cart.FirstOrDefault(x => x.MenuItemId == id);
        //        if (existing != null)
        //            existing.Quantity++;
        //        else
        //            cart.Add(new CartItem { MenuItemId = item.Id, Name = item.Name, Price = item.Price, Quantity = 1 });

        //        HttpContext.Session.SetObjectAsJson("cart", cart);
        //    }

        //    return RedirectToAction("Index");
        //}


        //[HttpPost]
        //public IActionResult SubmitOrder([FromBody] List<OrderItem> items)
        //{
        //    if (items == null || !items.Any())
        //        return Json(new { status = "error", message = "السلة فارغة" });

        //    // هنا يتم حفظ البيانات (مثلاً في قاعدة بيانات)
        //    // بإمكانك تنفيذ منطق التخزين كما يناسبك

        //    // مثال فقط: طباعة في اللوج
        //    foreach (var item in items)
        //    {
        //        Console.WriteLine($"✅ صنف: {item.Name} - كمية: {item.Qty} - السعر: {item.Price}");
        //    }

        //    return Json(new { status = "success" });
        //}


        [HttpPost]
        public IActionResult SubmitOrder(List<OrderItemVM> items)
        {
            var filteredItems = items.Where(i => i.Quantity > 0).ToList();

            if (!filteredItems.Any())
            {
                ModelState.AddModelError("", "يجب اختيار صنف واحد على الأقل.");
                return View(); // أو إعادة توجيه لنفس الصفحة مع عرض رسالة الخطأ
            }

            // مثال على الحفظ - يمكنك التعديل حسب قاعدة البيانات الخاصة بك
            foreach (var item in filteredItems)
            {
                // احفظ بيانات الطلب هنا
                // dbContext.Orders.Add(new Order { SubcategoryId = item.SubcategoryId, Quantity = item.Quantity });
            }

            // dbContext.SaveChanges();

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View(); // صفحة نجاح الطلب
        }
        public class OrderItemVM
        {
            public int SubcategoryId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; } // ✅ أضف السعر
        }


    }
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }

   
   
}
