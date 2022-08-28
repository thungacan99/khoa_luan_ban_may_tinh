using BanMayTinh.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using BanMayTinh.Models.DB;
using System.Web;
using System;

namespace BanMayTinh.Controllers
{
    public class DonHangController : Controller
    {
        DBContext dbContext = new DBContext();
        // GET: DonHangUser
        public ActionResult DSDonHang(int trangthai = 0)
        {
            ViewBag.TongTien = 0;

            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null)
            {
                DSDonDatHang donDatHang = new DSDonDatHang();
                donDatHang.ListDonDatHang = dbContext.DonDatHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).ToList();
                //GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault();

                if (trangthai != 0)
                {
                    donDatHang.ListDonDatHang = donDatHang.ListDonDatHang.Where(x => x.TrangThai == trangthai).ToList();
                }
                return View(donDatHang);
            }
            return RedirectToAction("LogIn", "Home");
        }
    }
}