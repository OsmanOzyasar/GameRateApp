namespace GameRateApp.WebApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
    
                _logger.LogError(ex, "An unhandled exception occurred.");

                if (_env.IsDevelopment())
                {
                    await HandleExceptionAsync(httpContext, ex, 500, true);
                }
                else
                {
                    await HandleExceptionAsync(httpContext, ex, 500, false);
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, bool includeDetail = false)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = includeDetail ? exception.Message : "An unexpected error occurred.",
                Detail = includeDetail ? exception.StackTrace : null 
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }



}
