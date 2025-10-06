using Microsoft.AspNetCore.Http;// Tin: gọi để sử dụng IFormFile
using System.Threading.Tasks;
namespace core_website.Services
{
    //Tin 6/10
    public interface IUpLoadIMG
    {
        bool ValidateImg(IFormFile file);
        Task<string> SaveImageAsync(IFormFile file, string path);// : Task để sử dụng await , gía trị trả về là Task<type>
        // IFormFile file: file được upload lên từ form
        // string path: đường dẫn thư mục để lưu file
        // trả về chuỗi string là đường dẫn của file đc lưu

        void DeleteImage(string filePath);
    }
    //Tin END
}
