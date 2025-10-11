using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Api.Models
{
  public class DanhMuc
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaDM { get; set; }

    [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
    [StringLength(50, ErrorMessage = "Tên danh mục không được vượt quá 50 ký tự.")]
    public string TenDM { get; set; }
  }
}
