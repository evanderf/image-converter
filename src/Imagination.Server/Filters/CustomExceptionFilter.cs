using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imagination.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string exceptionMessage;
            if (context.Exception.InnerException == null)
            {
                exceptionMessage = context.Exception.Message;
            }
            else
            {
                exceptionMessage = context.Exception.InnerException.Message;
            }

            context.Result = new ContentResult
            {
                Content = exceptionMessage,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

        }
    }
}
