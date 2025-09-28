using core_w2.Areas.Users.Models;

namespace core_w2.Areas.Users.Services
{
    public interface ISanPhamService
    {
        List<SanPham> GetAll();

        //Huy code
        void UpdateList(List<SanPham> list);
        //Huy end

        // Tan code
        SanPham GetById(int maSP); 
        // Tan end
    }
}
