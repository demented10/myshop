using Microsoft.AspNetCore.Mvc.Filters;

namespace eshop.Filters
{
    public class IncomingRequestFilter : IAsyncActionFilter
    {
        private readonly ILogger<IncomingRequestFilter> _logger;
        public IncomingRequestFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<IncomingRequestFilter>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogDebug("{date:g}: \nroute: {route} \nreferer: {referer}", DateTime.UtcNow, context.HttpContext.Request.Path,
                context.HttpContext.Request.Headers["Referer"]);
            await next();
        }
    }
}
