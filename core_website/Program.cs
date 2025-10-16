// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Admins.Middlewares;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Services;
using core_website.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
// KhoaTr - END

var builder = WebApplication.CreateBuilder(args);

// Sử dụng dịch vụ xác thực cookie
// Admins
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
      options.LoginPath = "/Admins/Auth/Login";
      options.LogoutPath = "/Admins/Auth/Logout";
      options.Cookie.HttpOnly = true; // Chống XSS
      options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Hạn cookie
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
// Tinsle code
builder.Services.AddTransient<IImageProcessingService, ImageProcessingService>();
builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<IDanhMucService, DanhMucService>();
// Tinsle code end
//Huy - 10/10/25: đăng ký DI NguoiDung
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();
//Huy END
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//Custom MiddleWares
app.UseAuthCheckMiddleware();
app.UseProductFormValidation();
// KhoaTr - 28/09/2025: Sửa lại route controller
app.MapControllerRoute(  
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
