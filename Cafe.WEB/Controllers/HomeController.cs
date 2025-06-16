using Cafe.Services.Account;
using Cafe.Services.Helper;
using Cafe.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WEB.Controllers
{
    [AreaAuthentication("All")]
    public class HomeController : Controller
    {
        private readonly IAccountService _account;
        private readonly IHelperService _helper;

        public HomeController(IAccountService account, IHelperService helper)
        {
            _account = account;
            _helper = helper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeLang(string lang)
        {
            Response.Cookies.Delete("Lang");
            Response.Cookies.Append("Lang", lang);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [AllowAnonymous]
        public IActionResult Login(int errorid = 0)
        {
            if (Request.Cookies.FirstOrDefault(x => x.Key == "UserId").Value == null)
            {
                if (errorid == 0)
                {
                    ViewBag.Error = "none";
                }
                else
                {
                    ViewBag.Error = "block";
                    ViewBag.ErrorType = "1";
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(PrUserVm userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin.UserName) || string.IsNullOrWhiteSpace(userLogin.Password))
                return RedirectToAction("Login", new { errorid = 99 });

            var data = _account.UserLogin(userLogin);
            if (data.Sts == 1)
            {
                CookieOptions option = new CookieOptions();
                option.IsEssential = true;
                Response.Cookies.Append("UserId", data.Id.ToString(), option);
                Response.Cookies.Append("UserRule", data.UserTypeId.ToString(), option);
                Response.Cookies.Append("UserName", data.UserName, option);
                Response.Cookies.Append("AuthKey", data.AuthKey, option);
                Response.Cookies.Append("Lang", "ar", option);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login", new { errorid = 99 });
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie != "Lang")
                    Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login");
        }

        //**************************************//
        public IActionResult ReadToExcel()
        {
            var tables = _helper.GetTablesToReadFrom();

            foreach (var item in tables)
            {
                var data = _helper.WriteDataToExcel(item);
            }

            return View();
        }
    }
}