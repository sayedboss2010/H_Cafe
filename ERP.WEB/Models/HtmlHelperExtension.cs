using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.WEB;

public static class HtmlHelperExtension
{
    public static string isActive(this IHtmlHelper<dynamic> html, string action = null, string controller = null)
    {
        string activeClass = "bg-gradient-primary active"; // change here if you another name to activate sidebar items
                                                           // detect current app state
        string actualAction = (string)html.ViewContext.RouteData.Values["action"];
        string actualController = (string)html.ViewContext.RouteData.Values["controller"];

        if (String.IsNullOrEmpty(controller))
            controller = actualController;

        if (String.IsNullOrEmpty(action))
            action = actualAction;

        return (controller == actualController && action == actualAction) ? activeClass : String.Empty;
    }
}