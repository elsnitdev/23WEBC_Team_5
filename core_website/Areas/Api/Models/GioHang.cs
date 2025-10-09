using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Tin cập nhật Model  giỏ hàng
namespace core_website.Models
{
    public class GioHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaGH {  get; set; }
        public int MaND { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; }
        public ICollection<CTGioHang> ChiTietGioHangs { get; set; }
        //ICollection là một navigation property biểu diễn mối quan hệ một-nhiều giữa GioHang và CTGioHang.
    }
}
