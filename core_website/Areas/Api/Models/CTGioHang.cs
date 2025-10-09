using System.ComponentModel.DataAnnotations.Schema;
using core_website.Areas.Api.Models;
//Tin cập nhật Model  ct giỏ hàng
namespace core_website.Models
{
    public class CTGioHang
    {
        public int MaGH { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGiaKhuyenMai { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ThanhTien { get; set; }
        public GioHang GioHang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
