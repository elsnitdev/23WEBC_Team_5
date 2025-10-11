using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Api.Models
{
    public class DanhMuc
    {
        [Key]
        public int MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        public string TenDanhMuc { get; set; }

        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        //sp co 1 / N danh muc 
        public ICollection<SanPham>? SanPhams { get; set; }
    }
}
