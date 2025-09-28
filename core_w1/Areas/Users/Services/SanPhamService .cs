using core_w2.Areas.Users.Models;
using System.Text.Json;
namespace core_w2.Areas.Users.Services
{
    public class SanPhamService : ISanPhamService
    {
        private List<SanPham> _dsSanPham;
        public SanPhamService()
        {
            _dsSanPham = ReadingJsonData();
        }
        public List<SanPham> GetAll() => _dsSanPham;

        //Huy code
        public void UpdateList(List<SanPham> list)
        {
            _dsSanPham.Clear();
            _dsSanPham.AddRange(list);
        }
        //Huy end
        //tan code
        private List<SanPham> ReadingJsonData()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "db.json");

            if (!File.Exists(filePath))
            {
                return new List<SanPham>();
            }

            var jsonData = File.ReadAllText(filePath);

            var jsonObj = JsonSerializer.Deserialize<JsonElement>(jsonData);

            var products = jsonObj.GetProperty("products").Deserialize<List<SanPham>>();

            return products ?? new List<SanPham>();
        }
        public SanPham GetById(int id)
        {
            return _dsSanPham.FirstOrDefault(sp => sp.MaSP == id);
        }
        //tan end
    }
}
