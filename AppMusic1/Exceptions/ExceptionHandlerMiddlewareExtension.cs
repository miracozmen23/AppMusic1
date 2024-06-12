using AppMusic1.ErrorModel;
using AppMusic1.Logger;
using Microsoft.AspNetCore.Diagnostics;

namespace AppMusic1.Exceptions
{
    public static class ExceptionHandlerMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            UnAuthorizedException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            statusCode = context.Response.StatusCode,
                            message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
