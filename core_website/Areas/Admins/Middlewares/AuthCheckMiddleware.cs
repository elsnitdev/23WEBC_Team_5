namespace core_website.Areas.Admins.Middlewares
{
  public class AuthCheckMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly HashSet<SkippedRoute> _skippedRoutes;
    public record SkippedRoute(string Method, string Path, bool ExactMatch);
    public AuthCheckMiddleware(RequestDelegate next)
    {
      _next = next;
      _skippedRoutes = new HashSet<SkippedRoute>
      {
        new SkippedRoute("GET", "/Admins/Auth/Login", true),
        new SkippedRoute("POST", "/Admins/Auth/Login", true),
      };
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
      var requestPath = httpContext.Request.Path.Value?.TrimEnd('/') ?? string.Empty;
      var requestMethod = httpContext.Request.Method;

      // Ngoại lệ không kiểm tra cho 1 số route được khai báo trong _skippedRoutes
      bool isSkipped = _skippedRoutes.Any(route =>
            route.Method.Equals(requestMethod, StringComparison.OrdinalIgnoreCase) &&
            (route.ExactMatch
                ? string.Equals(requestPath, route.Path, StringComparison.OrdinalIgnoreCase)
                : requestPath.StartsWith(route.Path, StringComparison.OrdinalIgnoreCase)));

      if (isSkipped)
      {
        Console.WriteLine("Passed - Skipped Route");
        await _next(httpContext);
        return;
      }
      // Kiểm tra nếu người dùng chưa đăng nhập và đang truy cập vào trang trong khu vực Admins
      // Ở đây kiểm tra session
      if (
        !httpContext.User.Identity.IsAuthenticated &&
         httpContext.Request.Path.StartsWithSegments("/Admins")
      ) {
        // Chuyển hướng người dùng đến trang đăng nhập
        httpContext.Response.Redirect("/Admins/Auth/Login");
        return; // Kết thúc middleware để ngăn chặn tiếp tục xử lý yêu cầu
      }
      // Nếu đã đăng nhập hoặc không phải truy cập khu vực Admins, tiếp tục xử lý yêu cầu
      await _next(httpContext);
    }
  }
    public static class AuthCheckMiddlewareExtensions
    {
      public static IApplicationBuilder UseAuthCheckMiddleware(this IApplicationBuilder builder)
      {
        return builder.UseMiddleware<AuthCheckMiddleware>();
      }
    }
}
