using core_website.Areas.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace core_website
{
    public class ApplicationDbContext: DbContext
    {
        //Huy - 23/10/25: khai báo DbSet cho bảng SanPham
        public DbSet<SanPham> SanPham { get; set; }
        //Huy - end
        //Tin -24/10 : khai báo DbSet cho bảng DanhMuc
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<PhanLoai> PhanLoai { get; set; }
        //Tin - end
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected ApplicationDbContext()
        {
        }


        //Huy - 23/10/25: Entity config cho bảng SanPham
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.ToTable("DanhMuc");
                entity.HasKey(e => e.MaDM);
                entity.Property(e => e.TenDM)
                .IsRequired()
                .HasMaxLength(50);
            });
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
        }
        //Huy - end
    }
}
