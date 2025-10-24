using core_website.Areas.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace core_website
{
    public class ApplicationDbContext: DbContext
    {
        //Huy - 23/10/25: khai báo DbSet cho bảng SanPham
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
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
                //Cấu hình tên bảng
                entity.ToTable("SanPham");

                //Cấu hình các thuộc tính
                entity.HasKey(e => e.MaSP);
                entity.Property(e => e.TenSP)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.DonGia)
                    .IsRequired()
                    .HasColumnType("Decimal(18,2)");
                entity.Property(e => e.KhuyenMai)
                    .IsRequired()
                    .HasColumnType("Decimal(18,2)");
                entity.Property(e => e.MoTa)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.ThongSo)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.SoLuong)
                    .HasDefaultValue(0)
                    .IsRequired();
                entity.Property(e => e.HinhAnh)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.ThoiGianTao)
                    .HasColumnType("datetime")
                    .IsRequired(false); 
                entity.Property(e => e.ThoiGianCapNhat)
                    .HasColumnType("datetime")
                    .IsRequired(false); 
                entity.Property(e => e.TrangThai)
                    .HasColumnType("bit")
                    .IsRequired();

                //cấu hình khoá ngoại
                entity
                    .HasMany(sp => sp.DanhMuc)
                    .WithMany(dm => dm.SanPham)
                    .UsingEntity(j => j.ToTable("PhanLoai"));
            });

            // KhoaTr - 24/10/2025: Entity config cho bảng NguoiDung
            modelBuilder.Entity<NguoiDung>(entity =>
            {
              //Cấu hình tên bảng
              entity.ToTable("NguoiDung");

              //Cấu hình các thuộc tính
              entity.HasKey(e => e.MaND);
              entity.Property(e => e.TenND)
                  .IsRequired()
                  .HasMaxLength(50);
              entity.Property(e => e.MatKhau)
                  .IsRequired();
              entity.Property(e => e.VaiTro)
                  .IsRequired()
                  .HasMaxLength(20);
              entity.Property(e => e.TrangThai)
                  .IsRequired();
            });
        }
        //Huy - end
    }
}
