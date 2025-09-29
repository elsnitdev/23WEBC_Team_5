using core_w2.Areas.Admins.Middlewares;
using core_w2.Areas.Admins.Services;
using core_w2.Areas.Admins.Controllers;
using core_w2.Areas.Users.MiddleWares;
using core_w2.Areas.Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Tinsle code

builder.Services.AddScoped<IRequestLogger, RequestLogger>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<core_w2.Areas.Users.Services.ISanPhamService,
                           core_w2.Areas.Users.Services.SanPhamService>();
builder.Services.AddScoped<core_w2.Areas.Admins.Services.ISanPhamService, core_w2.Areas.Admins.Services.SanPhamService>();
//builder.Services.AddScoped<ISanPhamService, SanPhamService>();
var app = builder.Build();
//app.UseMiddleware<SaveProductMiddleware>();

// Tinsle code end
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
//app.UseMiddleware<core_w2.Areas.Admins.Middlewares.ReadingJsonData>();
app.UseMiddleware<ProductFormValidation>();

// KhoaTr - 28/09/2025: Sửa lại route controller
app.MapControllerRoute(  
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "users",
    pattern: "users/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
