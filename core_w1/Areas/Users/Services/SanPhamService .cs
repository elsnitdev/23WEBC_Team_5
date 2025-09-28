using core_w2.Areas.Users.Models;
namespace core_w2.Areas.Users.Services
{
    public class SanPhamService : ISanPhamService
    {
        private List<SanPham> _dsSanPham;
        public SanPhamService()
        {
            _dsSanPham = new List<SanPham>();
        }
        public SanPhamService(List<SanPham> dsSanPham)
        {
            _dsSanPham = dsSanPham;
        }
        public List<SanPham> GetAll() => _dsSanPham;

        //Huy code
        public void UpdateList(List<SanPham> list)
        {
            _dsSanPham.Clear();
            _dsSanPham.AddRange(list);
        }
        //Huy end
    }
}
