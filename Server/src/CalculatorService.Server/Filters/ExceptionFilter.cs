using CalculatorService.Server.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CalculatorService.Server.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = ErrorResponse.InternalErrorResponse(context.Exception);

            var result = new ObjectResult(response)
            {
                StatusCode = response.ErrorStatus
            };

            context.Result = result;
        }
    }
}
