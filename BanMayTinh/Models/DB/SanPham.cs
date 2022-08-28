namespace BanMayTinh.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            AnhSanPhams = new HashSet<AnhSanPham>();
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string TenSanPham { get; set; }

        [StringLength(500)]
        public string AnhSanPham { get; set; }

        public int? Id_HangSanXuat { get; set; }

        public int? Id_LoaiSanPham { get; set; }

        [StringLength(250)]
        public string ThuocTinh1 { get; set; }

        [StringLength(250)]
        public string ThuocTinh2 { get; set; }

        [StringLength(250)]
        public string ThuocTinh3 { get; set; }

        [StringLength(250)]
        public string ThuocTinh4 { get; set; }

        [StringLength(250)]
        public string ThuocTinh5 { get; set; }

        public double? DonGia { get; set; }

        public int SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public virtual HangSanXuat HangSanXuat { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
