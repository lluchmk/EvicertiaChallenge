using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Net;

namespace CalculatorService.Server.Responses
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }

        public static ErrorResponse BadRequestErrorResponse(ActionContext actionContext)
        {
            var validationErrors = actionContext.ModelState
                .Values
                .Where(v => v.ValidationState == ModelValidationState.Invalid)
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            var errorMessage = string.Join("; ", validationErrors);

            return new ErrorResponse
            {
                ErrorCode = HttpStatusCode.BadRequest.ToString(),
                ErrorStatus = (int)HttpStatusCode.BadRequest,
                ErrorMessage = errorMessage
            };
        }

        public static ErrorResponse InternalErrorResponse(Exception exception)
        {
            return new ErrorResponse
            {
                ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                ErrorStatus = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message
            };
        }
    }
}
