using Cafe.Services.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe.WEB;

public class AreaAuthenticationAttribute : TypeFilterAttribute
{
    public AreaAuthenticationAttribute(string Role) : base(typeof(AreaAuthenticationFilter))
    {
        Arguments = new object[] { Role };
    }
}

public class AreaAuthenticationFilter : IAuthorizationFilter
{
    public string UserId { get; set; }
    public string UserRule { get; set; }
    public string AutKey { get; set; }

    private readonly int _role;

    private readonly IHelperService _helper;
    public AreaAuthenticationFilter(string role, IHelperService helper)
    {
        if (role.Equals("Stockes")) _role = 1;
        else if (role.Equals("Engineer")) _role = 2;
        else if (role.Equals("HR")) _role = 3;
        else if (role.Equals("Accounting")) _role = 4;
        else if (role.Equals("Treasury")) _role = 5;
        else if (role.Equals("All")) _role = 6;
        else if (role.Equals("DataEntry")) _role = 7;
        else if (role.Equals("Tender")) _role = 8;
        else if (role.Equals("Purchase")) _role = 9;
        else if (role.Equals("Bills")) _role = 10;
        else _role = 0;

        _helper = helper;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        UserId = context.HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "UserId").Value;
        AutKey = context.HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "AuthKey").Value;
        UserRule = context.HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "UserRule").Value;

        if (((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
        {
            //do nothing
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(UserId) && !string.IsNullOrWhiteSpace(AutKey) && !string.IsNullOrWhiteSpace(UserRule) && AutKey == _helper.HashMd5(UserId.ToString() + UserRule.ToString())
                && (_role == 6 || int.Parse(UserRule) == _role || int.Parse(UserRule) == 6))
            {
                //do nothing
            }
            else
                context.Result = new RedirectResult("~/Home/Logout");
        }
    }
}