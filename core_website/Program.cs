// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Areas.Admins.Middlewares;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Services;
using core_website.Services;
// KhoaTr - END

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Tinsle code
builder.Services.AddTransient<IImageProcessingService, ImageProcessingService>();
builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddControllers();  

// Thêm HttpClient để MVC gọi API
builder.Services.AddHttpClient("ProductApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5299/");  //thay bang địa chỉ API 
    client.DefaultRequestHeaders.Add("Accept", "application/json");  // Nhận JSON
});
//Huy - 10/10/25: đăng ký DI NguoiDung
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();
//Huy END
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
// Tinsle : cap nhat duong dan Login
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Auth}/{action=Login}/{id?}"); //Huy - 10/10/25: mặc định mở trang login khi vào /admins
// KhoaTr - 28/09/2025: Sửa lại route controller
app.MapControllerRoute(  
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
