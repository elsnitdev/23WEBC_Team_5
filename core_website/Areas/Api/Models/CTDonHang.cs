using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace core_website.Areas.Admins.Models
{
    [Table("CTDonHang")]
    public class CTDonHang
    {
        [Required]
        public int MaDH { get; set; }   

        [Required]
        public int MaSP { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGiaKhuyenMai { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ThanhTien { get; set; }
    }
}
