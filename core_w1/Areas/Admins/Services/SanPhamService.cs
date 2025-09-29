using System.Text.Json;
using core_w2.Areas.Users.Models;
using Microsoft.AspNetCore.Hosting;

namespace core_w2.Areas.Admins.Services
{
    public class SanPhamService : ISanPhamService
    {
        private readonly string _path;

        private class DbModel
        {
            public List<SanPham> Products { get; set; } = new();
            public int Total { get; set; }
        }

        public SanPhamService(IWebHostEnvironment env)
        {
            // db.json nằm cùng cấp Areas
            _path = Path.Combine(env.ContentRootPath, "db.json");
        }

        public IEnumerable<SanPham> GetAll()
        {
            var db = ReadData();
            return db.Products;
        }

        private DbModel ReadData()
        {
            if (!File.Exists(_path))
            {
                var emptyDb = new DbModel();
                SaveData(emptyDb);
                return emptyDb;
            }
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<DbModel>(json) ?? new DbModel();
        }

        private void SaveData(DbModel db)
        {
            var json = JsonSerializer.Serialize(db, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        public void LuuSanPham(SanPham sp)
        {
            var db = ReadData();

            sp.MaSP = db.Products.Any() ? db.Products.Max(p => p.MaSP) + 1 : 1;

            db.Products.Add(sp);
            db.Total = db.Products.Count;

            SaveData(db);
        }
    }
}
