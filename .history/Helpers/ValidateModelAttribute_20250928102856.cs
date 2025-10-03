using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Helpers
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorMessages = string.Join("\n", errors);
                var controller = context.Controller as Controller;

                if (controller != null)
                {
                    controller.TempData["Error"] = errorMessages;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}