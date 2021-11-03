using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SecurrencyTDS.Domain.Logging;
using SecurrencyTDS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SecurrencyTDS.Domain.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
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
                        logger.LogError($"An exception has happened: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal Server Error. -  {contextFeature.Error?.Message} {contextFeature.Error?.StackTrace}"
                        }.ToString());
                    }
                });
            });
        }


    }
}
