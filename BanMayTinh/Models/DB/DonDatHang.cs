namespace BanMayTinh.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string UserNameKH { get; set; }

        public string SoDienThoaiNguoiNhan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayDat { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayGiao { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNguoiNhan { get; set; }

        [Required]
        public string DiaChi { get; set; }

        public string YeuCau { get; set; }

        public int TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
    }
}
