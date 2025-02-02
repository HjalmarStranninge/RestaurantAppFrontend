namespace RestaurantAppFrontend
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ILogger<AdminAuthorizationFilter> _logger;

        public AdminAuthorizationFilter(ILogger<AdminAuthorizationFilter> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAdmin = context.HttpContext.Session.GetString("isAdmin");

            _logger.LogInformation($"Session contains isAdmin: {isAdmin}");

            if (string.IsNullOrEmpty(isAdmin) || isAdmin != "true")
            {
                _logger.LogWarning("Unauthorized access attempt.");
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
            else
            {
                _logger.LogInformation("Admin access granted.");
            }
        }


    }
}
