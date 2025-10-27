using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Api.Models.ThongKeModle
{
    // Nhóm: Thống kê theo danh mục

    public class ThongKeTheoDanhMuc
    {
        [Required]
        [Display(Name = "Tên danh mục")]
        public string TenDanhMuc { get; set; }

        [Display(Name = "Số lượng mặt hàng")]
        [Range(0, int.MaxValue)]
        public int SoLuongMatHang { get; set; }
    }
}
