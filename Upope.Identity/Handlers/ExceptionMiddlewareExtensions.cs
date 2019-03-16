using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using Upope.Identity.Models;

namespace Upope.Identity.Handlers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var httpResponseModel = new HttpResponseModel(contextFeature.Error.ToString(), context.Response.StatusCode.ToString());
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(httpResponseModel));
                    }
                });
            });
        }
    }
}
