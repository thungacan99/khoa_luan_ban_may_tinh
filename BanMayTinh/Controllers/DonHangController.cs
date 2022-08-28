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

            DSDonDatHang donDatHang = new DSDonDatHang();


            var formName = "Tất cả đơn hàng";

            if (trangthai == 1)
            {
                formName = "Đơn hàng chờ xác nhận";
            }
            else if (trangthai == 2)
            {
                formName = "Đơn hàng đang giao";
            }
            else if (trangthai == 3)
            {
                formName = "Đơn hàng đã giao";
            }
            else if (trangthai == 4)
            {
                formName = "Đơn hàng giao thành công";
            }
            else if (trangthai == 5)
            {
                formName = "Đơn hàng giao thất bại";
            }
            else if (trangthai == 6)
            {
                formName = "Đơn hàng hủy";
            }
            donDatHang.FormName = formName;

            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null)
            {
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

        /// <summary>
        /// chuyển đến màn hình chi tiết đơn hàng
        /// </summary>
        /// <param name="idDonHang"></param>
        /// <returns></returns>
        public ActionResult CTDonHang(int idDonHang)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null)
            {
                DonDatHang ctDonDatHang = dbContext.DonDatHangs.Where(x => x.Id == idDonHang).FirstOrDefault();

                return View(ctDonDatHang);
            }
            return RedirectToAction("LogIn", "Home");
        }
    }
}