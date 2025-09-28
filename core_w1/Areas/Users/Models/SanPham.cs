using System.ComponentModel.DataAnnotations;

namespace core_w2.Areas.Users.Models
{
    public class SanPham
    {
        public int MaSP { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "Đơn giá sản phẩm là bắt buộc")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá sản phẩm phải lớn hơn 0")]
        public decimal DonGia { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá khuyến mãi phải lớn hơn 0")]
        public decimal DonGiaKhuyenMai { get; set; }
        [Url(ErrorMessage = "Hình ảnh phải là một URL dẫn tới hình ảnh hợp lệ")]
        public string? HinhAnh { get; set; }
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? MoTa { get; set; }
        [Required(ErrorMessage = "Loại sản phẩm là bắt buộc")]
        public string LoaiSP { get; set; }
    }
}
