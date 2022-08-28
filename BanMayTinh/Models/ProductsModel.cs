using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanMayTinh.Models.DB;

namespace BanMayTinh.Models
{
    public class ProductsModel
    {
        public List<SanPham> danhSach { get; set; }

        public ProductsModel()
        {
            danhSach = new List<SanPham>();
        }

    }
}