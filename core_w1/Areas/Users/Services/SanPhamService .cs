using core_w2.Areas.Users.Models;

using System.Text.Json;

namespace core_w2.Areas.Users.Services
{
    public class SanPhamService : ISanPhamService
    {

        private List<SanPham> _dsSanPham;
        public SanPhamService(IWebHostEnvironment env)
        {
            var jsonPath = Path.Combine(env.ContentRootPath, "db.json");

            var json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var wrapper = JsonSerializer.Deserialize<SanPhamWrapper>(json, options);
            _dsSanPham = wrapper?.Products ?? new List<SanPham>();
        }
        public SanPhamService(List<SanPham> dsSanPham)
        {
            _dsSanPham = dsSanPham;
        }
        public List<SanPham> GetAll() => _dsSanPham;

        //Huy code
        public void UpdateList(List<SanPham> list)
        {
            _dsSanPham.Clear();
            _dsSanPham.AddRange(list);
        }
        //Huy end

    }
}
