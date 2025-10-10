using core_website.Models.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_website.Models
{
    public class NguoiDung
    {
        /*
          [MaND] integer PRIMARY KEY IDENTITY(1, 1),
          [TenND] nvarchar(50),
          [MatKhau] nvarchar(50),
          [VaiTro] nvarchar(20),
          [TrangThai] bit,
          CONSTRAINT CHK_VaiTro CHECK (VaiTro IN ('admin', 'user'))
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaND { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} tối thiểu 3 kí tự, tối đa 50 kí tự!")]
        [Display(Name = "Tên người dùng", Prompt = "Nhập tên người dùng")]
        public string TenND { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc!")]
        [MatKhauManh]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu", Prompt = "Nhập mật khẩu")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc!")]
        [StringLength(20, ErrorMessage = "{0} không quá 20 kí tự!")]
        [RegularExpression("^(admin|user)$", ErrorMessage = "{0} phải là admin hoặc user!")]
        [Display(Name = "Vai trò", Prompt = "Chọn vai trò")]
        public string VaiTro { get; set; }


        [Required(ErrorMessage = "{0} là bắt buộc!")]
        [Display(Name = "Trại thái hoạt động")]
        public bool TrangThai { get; set; }
    }
}
