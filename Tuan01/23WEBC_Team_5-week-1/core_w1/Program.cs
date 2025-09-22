var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Thêm file customappsettings vào cấu hình
builder.Configuration.AddJsonFile("customappsettings.json");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Dùng middleware để xử lý các mã trạng thái, điều hướng về view error
app.UseStatusCodePagesWithReExecute("/error/{0}");


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Định tuyến về trang error phù hợp với mã lỗi
app.MapControllerRoute(
    name: "error",
    pattern: "error/{statusCode}",
    defaults: new { controller = "Error", action = "HandleError" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
