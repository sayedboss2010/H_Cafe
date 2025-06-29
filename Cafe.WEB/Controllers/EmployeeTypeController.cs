using Cafe.Services.OrderType;
using Cafe.VM.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WEB.Controllers
{
    public class EmployeeTypeController : Controller
    {

        private readonly IEmployeeTypeService _EmployeeTypeService;

        public EmployeeTypeController(IEmployeeTypeService EmployeeTypeService)
        {
            _EmployeeTypeService = EmployeeTypeService;
        }


        public IActionResult Index()
        {
            ViewBag.Search = "";
            var lst = _EmployeeTypeService.List();

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return isAjax
                ? PartialView("_PartialAllEmployeeType", lst)
                : View(lst);
        }

        public IActionResult Add(string nameAr, string nameEn)
        {
            EmployeeTypeVm vm = new()
            {
                EmployeeTypeID = 0,
                NameEn = nameEn,
                NameAr = nameAr,
                CreationUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            if (!_EmployeeTypeService.CheckExist(vm))
            {
                return Json(-1);
            }

            return Json(_EmployeeTypeService.Add(vm));
        }

        public IActionResult GetById(int id)
        {
            return Json(_EmployeeTypeService.Find(id));
        }

        public IActionResult Edit(int id, string nameEn, string nameAr)
        {
            EmployeeTypeVm vm = new()
            {
                EmployeeTypeID = id,
                NameEn = nameEn,
                NameAr = nameAr,
                UpdateUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            if (!_EmployeeTypeService.CheckExist(vm))
            {
                return Json(-1);
            }

            return Json(_EmployeeTypeService.Update(vm));
        }

        public IActionResult Delete(int id)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            return Json(_EmployeeTypeService.Delete(id, userId));
        }

        public IActionResult Search(string term = "")
        {
            ViewBag.Search = term;
            return PartialView("_PartialAllEmployeeType", _EmployeeTypeService.Search(term));
        }

        public IActionResult PrintData(string term = "")
        {
            if (string.IsNullOrEmpty(term))
                return Json(_EmployeeTypeService.List());

            return Json(_EmployeeTypeService.Search(term));
        }

        public IActionResult ActivateOrderType(long id, int isActive)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            var active = isActive != 0;

            return Json(_EmployeeTypeService.ActivateEntity(id, active, userId));
        }

        //*********************************************************************//
        public IActionResult ExportToExcel(string term = "")
        {
            var lst = string.IsNullOrEmpty(term) ? _EmployeeTypeService.List() : _EmployeeTypeService.Search(term);

            if (!lst.Any())
            {
                return Redirect(Request.GetTypedHeaders().Referer?.ToString() ?? "/");
            }

            var dt = GetDataTableData(lst.ToList());

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "EmployeeTypes");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeTypes.xlsx");
                }
            }
        }

        private System.Data.DataTable GetDataTableData(List<EmployeeTypeVm> lst)
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
