namespace core_w2.Areas.Admins.Middlewares
{
    public class ReadingJsonData
    {
        private readonly RequestDelegate _next;
        private readonly core_w2.Areas.Users.Services.ISanPhamService _sanPhamService;

        public ReadingJsonData(RequestDelegate next,
                               core_w2.Areas.Users.Services.ISanPhamService sanPhamService)
        {
            _next = next;
            _sanPhamService = sanPhamService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // dùng _sanPhamService đọc dữ liệu JSON
            var allProducts = _sanPhamService.GetAll();

            // log thử
            Console.WriteLine($"[ReadingJsonData] Đọc {allProducts.Count()} sản phẩm");

            await _next(context);
        }
    }

}
