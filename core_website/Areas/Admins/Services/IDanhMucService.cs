using System.Collections.Generic;
using core_website.Areas.Admins.Models;

namespace core_website.Areas.Admins.Services
{
    public interface IDanhMucService
    {
        List<DanhMuc> GetAll();
        DanhMuc? GetById(int id);
        void Add(DanhMuc danhMuc);
        void Update(DanhMuc danhMuc);
        void Delete(int id);
    }
}
