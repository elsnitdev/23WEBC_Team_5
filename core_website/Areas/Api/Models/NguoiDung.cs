namespace core_website.Areas.Api.Models
{
  public class NguoiDung
  {
    public int MaND { get; set; }
    public string? TenND { get; set; }
    public string? MatKhau { get; set; }
    public string? VaiTro { get; set; } // 'admin' or 'user'
    public bool TrangThai { get; set; }
  }
  public class NguoiDungLoginRequest
  {
    public string? TenND { get; set; }
    public string? MatKhau { get; set; }
  }
  // Bỏ MatKhau, TrangThai 
  // NguoiDungResponse dùng để chứa thông tin người dùng (phục vụ cho các tác vụ cần đọc thông tin NguoiDung trừ những thông tin nhạy cảm)
  public class NguoiDungInfo
  {
    public int MaND { get; set; }
    public string? TenND { get; set; }
    public string? VaiTro { get; set; } // 'admin' or 'user'
  }
  public class NguoiDungResponse
  {
    public bool Success { get; set; }
    public string? Message { get; set; }
    public NguoiDungInfo User { get; set; }
  }
}
