using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanMayTinh.Models.DB;

namespace BanMayTinh.Models
{
    public class TienDonHang
    {
        public GioHang gioHang { set; get; }
        public int tongTien { set; get; }
    }
}