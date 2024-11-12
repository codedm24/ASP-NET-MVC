using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ASPNetCoreIntro
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
                //method 1 to send error data
                filterContext.Result = new ObjectResult("An error occured") 
                {
                    StatusCode = 500,
                    DeclaredType = typeof(string),
                };
                //method 2 to send error data
                filterContext.Result = new ViewResult
                {
                    ViewName = "CustomErrorPage"
                };
                //("~/CustomErrorPage");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
