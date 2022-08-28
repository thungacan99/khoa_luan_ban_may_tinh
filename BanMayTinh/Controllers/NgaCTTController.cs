using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanMayTinh.Models.DB;

namespace BanMayTinh.Controllers
{
    public class NgaCTTController : Controller
    {
        DBContext dBContext = new DBContext();
        // GET: NgaCTT
        public ActionResult Index()
        {
            GioHang gioHang = dBContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault();

            Session["GioHang"] = (GioHang)gioHang;

            if(Session["GioHang"] == null)
            {
                gioHang = new GioHang();
                gioHang.Id_GioHang = 1000;
                dBContext.GioHangs.Add((GioHang)Session["GioHang"]);
            }
            else
            {

                SanPham sp = dBContext.SanPhams.Where(x => x.Id == 1).FirstOrDefault();

                ChiTietGioHang ct = new ChiTietGioHang();
                ct.Id_GioHang = gioHang.Id_GioHang;
                ct.Id_SanPham = sp.Id;
                ct.SoLuong = 1;
                ct.DonGia = sp.DonGia;

                dBContext.ChiTietGioHangs.Add(ct);
                dBContext.SaveChanges();

            }

            return View(gioHang);
        }


    }
}