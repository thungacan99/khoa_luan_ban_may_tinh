namespace BanMayTinh.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public virtual DbSet<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDonDatHangs)
                .WithRequired(e => e.DonDatHang)
                .HasForeignKey(e => e.Id_DonDathang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GioHang>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.GioHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.HangSanXuat)
                .HasForeignKey(e => e.Id_HangSanXuat);

            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.LoaiSanPham)
                .HasForeignKey(e => e.Id_LoaiSanPham);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.AnhSanPhams)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.Id_SanPham);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonDatHangs)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.Id_SanPhamMua)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.Id_SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.GioHangs)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.UserNameKH);
        }
    }
}
