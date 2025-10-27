using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Api.Models.ThongKeModle
{
    public class ThongKe
    {
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
        [Display(Name = "Tổng doanh thu (VNĐ)")]
        [DataType(DataType.Currency)]
        public decimal TongDoanhThu { get; set; }

        [Display(Name = "Tổng số sản phẩm")]
        [Range(0, int.MaxValue)]
        public int TongSanPham { get; set; }

        [Display(Name = "Tổng danh mục")]
        [Range(0, int.MaxValue)]
        public int TongDanhMuc { get; set; }

        [Display(Name = "Tổng đơn hàng")]
        [Range(0, int.MaxValue)]
        public int TongDonHang { get; set; }



        // Các nhóm thống kê chi tiết

        [Display(Name = "Thống kê theo danh mục")]
        public List<ThongKeTheoDanhMuc> ThongKeDanhMuc { get; set; }

        [Display(Name = "Doanh thu theo tháng")]
        public List<DoanhThuTheoThang> DoanhThuTheoThang { get; set; }

        [Display(Name = "Sản phẩm bán chạy")]
        public List<SanPhamBanChay> SanPhamBanChay { get; set; }

        public ThongKe()
        {
            ThongKeDanhMuc = new List<ThongKeTheoDanhMuc>();
            DoanhThuTheoThang = new List<DoanhThuTheoThang>();
            SanPhamBanChay = new List<SanPhamBanChay>();
        }
    }
}
