using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text;

namespace MCVTask.Middleware
{
    public class ExceptionHandler
    {
        private RequestDelegate Next { get; }
        public ExceptionHandler(RequestDelegate next)
        {
            Next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandler> logger)
        {
            try
            {
                await Next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, logger, exception);
                await HandleExceptionResponse(context);
            }
        }

        private async Task HandleException(HttpContext httpContext, ILogger<ExceptionHandler> logger, Exception exception)
        {
            StringBuilder builder = new StringBuilder(Environment.NewLine);
            var headers = httpContext.Request.Headers;
            if (headers != null)
            {
                builder.AppendLine("Request Headers: ");
                foreach (KeyValuePair<string, StringValues> header in headers)
                {
                    builder.AppendLine($"Header Key: {header.Key}, Header Value :{header.Value}");
                }
            }

            using StreamReader reader = new StreamReader(httpContext.Request.Body);
            string requestBody = await reader.ReadToEndAsync();

            httpContext.Request.EnableBuffering();
            httpContext.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(requestBody))
                builder.AppendLine($"Request body: {requestBody}");

            logger.LogError(exception, builder.ToString());
        }

        private async Task HandleExceptionResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            await httpContext.Response.WriteAsync("Global Exception, Check ExceptionHandler class");
        }
    }
}
