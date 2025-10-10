using System.ComponentModel.DataAnnotations;
namespace core_website.Models.CustomAttributes
{
    public class MatKhauManh : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password)) 
                return new ValidationResult("Mật khẩu là bắt buộc!");

            if (password.Length < 6 || password.Length > 50)
                return new ValidationResult("Mật khẩu tối thiểu 6 kí tự và tối đa 50 kí tự!");

            if (!password.Any(char.IsUpper) 
                || !password.Any(char.IsLower) 
                || !password.Any(char.IsDigit)
                || !password.Any((ch => !char.IsLetterOrDigit(ch))))
                return new ValidationResult("Mật khẩu phải bao gồm chữ hoa, chữ thường, số và kí tự đặc biệt!");
            return ValidationResult.Success;
        }
    }
}
