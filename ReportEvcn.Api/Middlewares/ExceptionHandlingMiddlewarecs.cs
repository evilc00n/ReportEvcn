using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Result;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ReportEvcn.Api.Middlewares
{
    public class ExceptionHandlingMiddlewarecs
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddlewarecs(RequestDelegate next, ILogger logger)
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
            _logger.Error(exception, exception.Message);

            var errorMeassage = exception.Message;

            var responce = exception switch
            {
                UnauthorizedAccessException _ => new BaseResult()
                {
                    ErrorMessage = errorMeassage,
                    ErrorCode = (int)ErrorCodes.UnauthorizedAccess
                },
                _  => new BaseResult()
                {
                    ErrorMessage = "Internal Server Error. Please retry later",
                    ErrorCode = (int)ErrorCodes.InternarServerError
                },
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = responce.ErrorCode;
            await httpContext.Response.WriteAsJsonAsync(responce);

        }


    }
}
