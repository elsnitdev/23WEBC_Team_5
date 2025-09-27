using core_w2.Areas.Users.Models;
using System.Text.Json;
namespace core_w2.Areas.Users.Services
{
    public class SanPhamService : ISanPhamService
    {
        private readonly List<SanPham> _dsSanPham;
        public SanPhamService()
        {
            // Giả lập dữ liệu ban đầu
            _dsSanPham = new List<SanPham>
            {
                new SanPham { MaSP = 1, TenSP = "Laptop", DonGia = 15000000, DonGiaKhuyenMai = 14000000, HinhAnh="laptop.jpg", MoTa="Laptop gaming", LoaiSP="Điện tử" },
                new SanPham { MaSP = 2, TenSP = "Điện thoại", DonGia = 8000000, DonGiaKhuyenMai = 7500000, HinhAnh="phone.jpg", MoTa="Smartphone mới", LoaiSP="Điện tử" }
            };
        }
        public List<SanPham> GetAll() => _dsSanPham;
    }
}
