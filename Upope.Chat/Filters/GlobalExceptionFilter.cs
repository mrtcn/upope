using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;
using Upope.Chat.Controllers;

namespace Upope.Chat.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IStringLocalizer<ChatController> _localizer;
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(
            IStringLocalizer<ChatController> localizer,
            ILogger<GlobalExceptionFilter> logger)
        {
            _localizer = localizer;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var message = _localizer.GetString("GlobalException").Value;
            _logger.LogCritical(context.Exception.Message);

            //Checking for my custom exception type
            //var exceptionType = context.Exception.GetType();
            //if (exceptionType is UserNotAvailableException) 
            //{
            //    message = context.Exception.Message;
            //}

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new { Message = message });
        }
    }
}
