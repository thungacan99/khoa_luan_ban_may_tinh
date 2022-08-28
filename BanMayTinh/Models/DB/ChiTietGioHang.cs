namespace BanMayTinh.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGioHang")]
    public partial class ChiTietGioHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_GioHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_SanPham { get; set; }

        public int SoLuong { get; set; }

        public double? DonGia { get; set; }

        public virtual GioHang GioHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
