using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManagement.Helpers
{
    public static class NavigationHelper
    {
        public static string GeActiveClass(ViewContext viewContext, string controller, string action)
        {
            var currentController = viewContext.RouteData.Values["controller"]?.ToString();
            var currentAction = viewContext.RouteData.Values["action"]?.ToString();

            if (string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase)
                && string.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase))
            {
                return "active-link";
            }
            return string.Empty;
        }
    }
}