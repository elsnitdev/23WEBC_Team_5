// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Admins.Middlewares;
using core_website.MiddleWares;
using core_website.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
// KhoaTr - END

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Tinsle code
builder.Services.AddSingleton<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<IRequestLogger, RequestLogger>();
builder.Services.AddScoped<IUserService, UserService>();
// Tinsle code end
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
//app.UseMiddleware<ReadingJsonData>();
app.UseMiddleware<ProductFormValidation>();

// KhoaTr - 28/09/2025: Sửa lại route controller
app.MapControllerRoute(  
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
