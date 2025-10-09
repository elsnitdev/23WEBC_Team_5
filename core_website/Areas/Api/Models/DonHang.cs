using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace core_website.Models
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // IDENTITY(1,1)
        public int MaDH { get; set; }

        [Required]
        public int MaND { get; set; }  

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; }

        [Required]
        public DateTime ThoiGianTao { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("pending|resolved|rejected", ErrorMessage = "Trạng thái phải là pending, resolved hoặc rejected")]
        public string TrangThai { get; set; }


    }
}
