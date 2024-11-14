using System.Globalization;

namespace TatooMarket.Api.Middlewares
{
    public class CultureInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureInfoMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var languages = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var requestLanguage = context.Request.Headers.AcceptLanguage;
            var currentCulture = new CultureInfo("us");

            if (string.IsNullOrEmpty(requestLanguage) == false && languages.Any(d => d.Equals(requestLanguage)))
                currentCulture = new CultureInfo(requestLanguage);

            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;

            await _next(context);   
        }
    }
}
