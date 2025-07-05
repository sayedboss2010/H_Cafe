using Cafe.Services.Location;
using Cafe.Services.Table;
using Cafe.VM.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cafe.WEB.Controllers
{
    public class TableController : Controller
    {

        private readonly ITableService _ITableService;
        private readonly ILocationSerivce _ILocationSerivce;
        public TableController(ITableService ITableService, ILocationSerivce ILocationSerivce)
        {
            _ITableService = ITableService;
            _ILocationSerivce = ILocationSerivce;
        }


        public IActionResult Index()
        {
            ViewBag.Search = "";
            var lst = _ITableService.ListByEntityID(1);
            ViewBag.LocationID = 0;

            ViewBag.Locations = new SelectList(_ILocationSerivce.GetListDropByEntityID(1), "Id", "NameAr");

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return isAjax
                ? PartialView("_PartialAllTable", lst)
                : View(lst);
        }

        public IActionResult Add(string Notes, string mainDrpLocations)
        {
            TableVm vm = new()
            {

                Notes = Notes,
                LocationID = int.Parse(mainDrpLocations),
                CreationUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };  

            //if (!_ITableService.CheckExist(vm))
            //{
            //    return Json(-1);
            //}

            return Json(_ITableService.Add(vm));
        }

        public IActionResult GetById(int id)
        {
            return Json(_ITableService.Find(id));
        }

        public IActionResult Edit(int id,string Notes, string mainDrpLocations)
        {
            TableVm vm = new()
            {
                TabelID = id,
                Notes=Notes,
                LocationID = int.Parse(mainDrpLocations),
              
                UpdateUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            //if (!_ITableService.CheckExist(vm))
            //{
            //    return Json(-1);
            //}

            return Json(_ITableService.Update(vm));
        }

        public IActionResult Delete(int id)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            return Json(_ITableService.Delete(id, userId));
        }

        public IActionResult Search(string term = "")
        {
            ViewBag.Search = term;
            return PartialView("_PartialAllTable", _ITableService.Search(term));
        }

        public IActionResult PrintData(string term = "")
        {
            if (string.IsNullOrEmpty(term))
                return Json(_ITableService.List());

            return Json(_ITableService.Search(term));
        }

        public IActionResult ActivateOrderType(long id, int isActive)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            var active = isActive != 0;

            return Json(_ITableService.ActivateEntity(id, active, userId));
        }

        //*********************************************************************//
        public IActionResult ExportToExcel(string term = "")
        {
            var lst = string.IsNullOrEmpty(term) ? _ITableService.ListByEntityID(1) : _ITableService.Search(term);

            if (!lst.Any())
            {
                return Redirect(Request.GetTypedHeaders().Referer?.ToString() ?? "/");
            }

            var dt = GetDataTableData(lst.ToList());

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Tabels");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
                }
            }
        }

        private System.Data.DataTable GetDataTableData(List<TableVm> lst)
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
                row[0] = item.TabelID;
                row[1] = item.Notes;
                row[2] = item.IsActive == true ? (lang == "ar" ? "مفعل" : "Active") : (lang == "ar" ? "غير مفعل" : "Inactive");
                dt.Rows.Add(row);
            }

            return dt;
        }

    }
}
