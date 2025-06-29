using Cafe.Services.Generic;
using Cafe.Services.Menu;
using Cafe.Services.OrderType;
using Cafe.VM.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WEB.Controllers
{
    public class OrderTypeController : Controller
    {
        
        private readonly IOrderTypeService _OrderTypeService;

        public OrderTypeController(IOrderTypeService OrderTypeService)
        {
            _OrderTypeService = OrderTypeService;
        }
        

        public IActionResult Index()
        {
            ViewBag.Search = "";
            var lst = _OrderTypeService.List();

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return isAjax
                ? PartialView("_PartialAllOrderTypes", lst)
                : View(lst);
        }

        public IActionResult Add(string nameAr, string nameEn)
        {
            OrderTypeVm vm = new()
            {
                OrderTypeID = 0,
                NameEn = nameEn,
                NameAr = nameAr,
                CreationUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            if (!_OrderTypeService.CheckExist(vm))
            {
                return Json(-1);
            }

            return Json(_OrderTypeService.Add(vm));
        }

        public IActionResult GetById(int id)
        {
            return Json(_OrderTypeService.Find(id));
        }

        public IActionResult Edit(int id, string nameEn, string nameAr)
        {
            OrderTypeVm vm = new()
            {
                OrderTypeID = id,
                NameEn = nameEn,
                NameAr = nameAr,
                UpdateUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            if (!_OrderTypeService.CheckExist(vm))
            {
                return Json(-1);
            }

            return Json(_OrderTypeService.Update(vm));
        }

        public IActionResult Delete(int id)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            return Json(_OrderTypeService.Delete(id, userId));
        }

        public IActionResult Search(string term = "")
        {
            ViewBag.Search = term;
            return PartialView("_PartialAllOrderTypes", _OrderTypeService.Search(term));
        }

        public IActionResult PrintData(string term = "")
        {
            if (string.IsNullOrEmpty(term))
                return Json(_OrderTypeService.List());

            return Json(_OrderTypeService.Search(term));
        }

        public IActionResult ActivateOrderType(long id, int isActive)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            var active = isActive != 0;

            return Json(_OrderTypeService.ActivateEntity(id, active, userId));
        }

        //*********************************************************************//
        public IActionResult ExportToExcel(string term = "")
        {
            var lst = string.IsNullOrEmpty(term) ? _OrderTypeService.List() : _OrderTypeService.Search(term);

            if (!lst.Any())
            {
                return Redirect(Request.GetTypedHeaders().Referer?.ToString() ?? "/");
            }

            var dt = GetDataTableData(lst.ToList());

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "OrderTypes");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderTypes.xlsx");
                }
            }
        }

        private System.Data.DataTable GetDataTableData(List<OrderTypeVm> lst)
        {
            var lang = Request.Cookies.FirstOrDefault(c => c.Key == "lang").Value ?? "ar";

            var dt = new System.Data.DataTable();

            dt.Columns.Add("");
            dt.Columns.Add("");
            dt.Columns.Add("");

            var row = dt.NewRow();
            row[0] = lang == "ar" ? "الاسم بالعربي" : "Name (AR)";
            row[1] = lang == "ar" ? "الاسم بالإنجليزي" : "Name (EN)";
            row[2] = lang == "ar" ? "التفعيل" : "Active";
            dt.Rows.Add(row);

            foreach (var item in lst)
            {
                row = dt.NewRow();
                row[0] = item.NameAr;
                row[1] = item.NameEn;
                row[2] = item.IsActive == true ? (lang == "ar" ? "مفعل" : "Active") : (lang == "ar" ? "غير مفعل" : "Inactive");
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}