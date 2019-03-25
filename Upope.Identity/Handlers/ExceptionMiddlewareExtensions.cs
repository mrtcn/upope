using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using NLog.Web;
using System.Net;

namespace Upope.Identity.Handlers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error(contextFeature.Error);
                        //var httpResponseModel = new HttpResponseModel(contextFeature.Error.ToString(), context.Response.StatusCode.ToString());
                        //await context.Response.WriteAsync(JsonConvert.SerializeObject(httpResponseModel));
                    }
                });
            });
        }
    }
}
