using GameRateApp.Business.Operations.Setting;

namespace GameRateApp.WebApi.Middlewares
{
    public class MaintenenceModeMiddleware
    {
        private readonly RequestDelegate _next;

        public MaintenenceModeMiddleware(RequestDelegate next)
        {
            _next = next;
   
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenenceMode = settingService.GetMaintenenceState();

            if(context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _next(context);
                return;
            }
                

            if (maintenenceMode)
            {
                await context.Response.WriteAsync("We are currently unable to provide service.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
