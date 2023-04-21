using System.Net;
using System.Text.Json;
using static EmployeeManagement.Model.ResponseModel.ExceptionModel;

namespace EmployeeManagement.Configurations
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(NoDataFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.OK;
                //stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(EmployeeIDNotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                //stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                //stackTrace = exception.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new { Information = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
