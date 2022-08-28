namespace BanMayTinh.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiSanPham")]
    public partial class LoaiSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string TenLoaiSanPham { get; set; }

        [StringLength(50)]
        public string ThuocTinh1 { get; set; }

        [StringLength(50)]
        public string ThuocTinh2 { get; set; }

        [StringLength(50)]
        public string ThuocTinh3 { get; set; }

        [StringLength(50)]
        public string ThuocTinh4 { get; set; }

        [StringLength(50)]
        public string ThuocTinh5 { get; set; }

        public int? Id_DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
