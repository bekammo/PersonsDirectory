using System.Globalization;

namespace PersonsDirectory.Api.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                try
                {
                    var culture = new CultureInfo(cultureQuery.Split(',').FirstOrDefault());
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
                catch (CultureNotFoundException)
                {
                    var defaultCulture = new CultureInfo("en-US");
                    CultureInfo.CurrentCulture = defaultCulture;
                    CultureInfo.CurrentUICulture = defaultCulture;
                }
            }

            await _next(context);
        }
    }

}
