using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Api.Models.ThongKeModle
{
    public class SanPhamBanChay
    {
        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }

        [Display(Name = "Số lượng bán")]
        [Range(0, int.MaxValue)]
        public int SoLuongBan { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
        [Display(Name = "Tổng tiền (VNĐ)")]
        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; }
    }
}
