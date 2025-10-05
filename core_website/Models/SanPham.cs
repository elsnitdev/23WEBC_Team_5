using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_website.Models
{
  public class SanPham
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaSP { get; set; }

    [StringLength(100)]
    public string TenSP { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DonGia { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal KhuyenMai { get; set; }

    [Column(TypeName = "text")]
    public string MoTa { get; set; }

    [Column(TypeName = "text")]
    public string ThongSo { get; set; }

    [StringLength(100)]
    public string Tag { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "text")]
    public string HinhAnh { get; set; }

    public DateTime ThoiGianTao { get; set; }

    public DateTime ThoiGianCapNhat { get; set; }

    public bool TrangThai { get; set; }
  }
}
