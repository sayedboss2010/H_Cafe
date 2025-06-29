using Cafe.Services.Employee;
using Cafe.Services.OrderType;
using Cafe.VM.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cafe.WEB.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _EmployeeService;
        private readonly IEmployeeTypeService _EmployeeTypeService;
        public EmployeeController(IEmployeeService EmployeeService, IEmployeeTypeService employeeTypeService)
        {
            _EmployeeService = EmployeeService;
            _EmployeeTypeService = employeeTypeService;
        }


        public IActionResult Index()
        {
            ViewBag.Search = "";
            var lst = _EmployeeService.List();
            ViewBag.EmployeeTypeID = 0;

            ViewBag.EmployeeTypes = new SelectList(_EmployeeTypeService.GetListDrop(), "Id", "NameAr");

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return isAjax
                ? PartialView("_PartialAllEmployee", lst)
                : View(lst);
        }

        public IActionResult Add(string nameAr, string nameEn, string Phone, string EmployeeTypeID)
        {
            EmployeeCafeVm vm = new()
            {
                
                NameEn = nameEn,
                NameAr = nameAr,
                Phone = Phone,
                EntityID = 1,
                EmployeeTypeID = int.Parse(EmployeeTypeID),
                CreationUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            if (!_EmployeeService.CheckExist(vm))
            {
                return Json(-1);
            }

            return Json(_EmployeeService.Add(vm));
        }

        public IActionResult GetById(int id)
        {
            return Json(_EmployeeService.Find(id));
        }

        public IActionResult Edit(int id, string nameEn, string nameAr, string Phone , string EmployeeTypeID)
        {
            EmployeeCafeVm vm = new()
            {
                EmployeeID=id,
                EmployeeTypeID = int.Parse(EmployeeTypeID),
                NameEn = nameEn,
                NameAr = nameAr,
                EntityID = 1,
                Phone = Phone,
                UpdateUserID = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0")
            };

            if (!_EmployeeService.CheckExist(vm))
            {
                return Json(-1);
            }

            return Json(_EmployeeService.Update(vm));
        }

        public IActionResult Delete(int id)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            return Json(_EmployeeService.Delete(id, userId));
        }

        public IActionResult Search(string term = "")
        {
            ViewBag.Search = term;
            return PartialView("_PartialAllEmployee", _EmployeeService.Search(term));
        }

        public IActionResult PrintData(string term = "")
        {
            if (string.IsNullOrEmpty(term))
                return Json(_EmployeeService.List());

            return Json(_EmployeeService.Search(term));
        }

        public IActionResult ActivateOrderType(long id, int isActive)
        {
            var userId = int.Parse(Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value ?? "0");
            var active = isActive != 0;

            return Json(_EmployeeService.ActivateEntity(id, active, userId));
        }

        //*********************************************************************//
        public IActionResult ExportToExcel(string term = "")
        {
            var lst = string.IsNullOrEmpty(term) ? _EmployeeService.List() : _EmployeeService.Search(term);

            if (!lst.Any())
            {
                return Redirect(Request.GetTypedHeaders().Referer?.ToString() ?? "/");
            }

            var dt = GetDataTableData(lst.ToList());

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Employee");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
                }
            }
        }

        private System.Data.DataTable GetDataTableData(List<EmployeeCafeVm> lst)
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
