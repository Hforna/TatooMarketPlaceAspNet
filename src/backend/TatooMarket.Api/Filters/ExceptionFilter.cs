using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TatooMarket.Communication.Responses;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BaseException baseException)
            {
                context.HttpContext.Response.StatusCode = (int)baseException.GetStatusCode();
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(baseException.GetErrorMessage()));
            }
        }
    }
}
