using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanMayTinh.Models;
using BanMayTinh.Models.DB;

namespace BanMayTinh.Controllers
{
    public class SearchController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult SearchProduct(String name)
        {
            ProductsModel rs = new ProductsModel();
            rs.danhSach = db.SanPhams.Where(u => u.TenSanPham.Contains(name)).ToList();
            return View(rs);
        }
    }
}