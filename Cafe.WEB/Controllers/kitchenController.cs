using Cafe.EF.Models;
using Cafe.Services.Generic;
using Cafe.Services.MenuCafe;
using Cafe.VM.ViewModels;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WEB.Controllers
{
    public class kitchenController : Controller
    {
        private readonly IGenericService<kitchenVM> order;

        public kitchenController(IGenericService<kitchenVM> _order)
        {

            order = _order;
        }
        public IActionResult Index()
        {
            string EntityId = "1";
            var data=order.Search(EntityId);
            return View(data);
        }

        [HttpPost]
        public IActionResult MarkOrderAsDone(int orderId)
        {
             

           
            kitchenVM data = new kitchenVM();
            data.OrderID= orderId;
            var res=order.Update(data);
            return Json(res);
        }

      
    }
}
