using core_website.Areas.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace core_website
{
    public class ApplicationDbContext: DbContext
    {
        //Huy - 23/10/25: khai báo DbSet cho bảng SanPham
        DbSet<SanPham> SanPham { get; set; }
        //Huy - end
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected ApplicationDbContext()
        {
        }


        //Huy - 23/10/25: Entity config cho bảng SanPham
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.ToTable("SanPham");
                entity.HasKey("MaSP");
                entity.Property("TenSP")
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property("DonGia")
                    .IsRequired()
                    .HasColumnType("Decimal(18,2)");
                entity.Property("KhuyenMai")
                    .IsRequired()
                    .HasColumnType("Decimal(18,2)");
                entity.Property("MoTa")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property("ThongSo")
                    .IsRequired()
                    .HasMaxLength(255);
                //entity.Property("Tag");
                entity.Property("SoLuong")
                    .HasDefaultValue(0);
                entity.Property("HinhAnh")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property("ThoiGianTao");
                entity.Property("ThoiGianCapNhat");
                entity.Property("TrangThai");
            });
        }
        //Huy - end
    }
}
