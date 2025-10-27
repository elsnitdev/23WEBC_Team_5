using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Api.Models.ThongKeModle
{
    public class DoanhThuTheoThang
    {
        [Display(Name = "Tháng")]
        [Range(1, 12, ErrorMessage = "Tháng phải nằm trong khoảng 1-12")]
        public int Thang { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
        [Display(Name = "Tổng doanh thu (VNĐ)")]
        [DataType(DataType.Currency)]
        public decimal TongDoanhThu { get; set; }
    }
}
