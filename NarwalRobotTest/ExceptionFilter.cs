using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;

namespace NarwalRobotTest
{
    public class ExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {

        }
    }
}
