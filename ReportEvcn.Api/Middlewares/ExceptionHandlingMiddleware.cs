using Microsoft.IdentityModel.Tokens;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Result;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ReportEvcn.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            Console.WriteLine(exception.GetType());
            _logger.Error(exception, exception.Message);

            var errorMeassage = exception.Message;


            var responce = exception switch
            {
                UnauthorizedAccessException _ => new BaseResult()
                {
                    ErrorMessage = errorMeassage,
                    ErrorCode = (int)ErrorCodes.UnauthorizedAccess
                },
                SecurityTokenException _ => new BaseResult()
                {
                    ErrorMessage = errorMeassage,
                    ErrorCode = (int)ErrorCodes.InvalidToken
                },
                _  => new BaseResult()
                {
                    ErrorMessage = "An unexpected error has occurred",
                    ErrorCode = (int)ErrorCodes.InternarServerError
                }
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = responce.ErrorCode;
            await httpContext.Response.WriteAsJsonAsync(responce);

        }


    }
}
