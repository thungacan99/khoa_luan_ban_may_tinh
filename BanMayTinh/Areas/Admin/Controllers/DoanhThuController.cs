using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanMayTinh.Models.DB;

namespace BanMayTinh.Areas.Admin.Controllers
{
    public class DoanhThuController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Admin/DoanhThu
        public ActionResult DoanhThu()
        {
            DateTime tuNgay = DateTime.Now; DateTime denNgay = DateTime.Now;
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                ViewBag.tuNgay = tuNgay.ToString("yyyy-MM-dd");
                ViewBag.denNgay = denNgay.ToString("yyyy-MM-dd");

                // gán giá trị mặc định
                ViewBag.slDonHang = 0;
                ViewBag.slDonHuy = 0;
                ViewBag.slDonKhongHuy = 0;

                ViewBag.doanhThuKhongHuy = 0;
                ViewBag.doanhThuHuy = 0;
                ViewBag.doanhThuChoXacNhan = 0;
                ViewBag.doanhThuDangGiaoHang = 0;
                ViewBag.doanhThuDaGiaoHang = 0;
                ViewBag.doanhThuGiaoThanhCong = 0;
                ViewBag.doanhThuGiaoThatBai = 0;
                ViewBag.doanhThuDonHuy = 0;


                var listDonHang = db.DonDatHangs.Where(x => x.NgayDat >= tuNgay && x.NgayDat <= denNgay).ToList();
                if (listDonHang != null && listDonHang.Count > 0)
                {
                    var slDonHang = listDonHang.Count;
                    var slDonHuy = listDonHang.Where(x => x.TrangThai == 5 || x.TrangThai == 6).ToList().Count;
                    var slDonKhongHuy = slDonHang - slDonHuy;

                    var doanhThuKhongHuy = listDonHang.Where(x => x.TrangThai != 5 && x.TrangThai != 6).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuHuy = listDonHang.Where(x => x.TrangThai == 5 || x.TrangThai == 6).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuChoXacNhan = listDonHang.Where(x => x.TrangThai == 1).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuDangGiaoHang = listDonHang.Where(x => x.TrangThai == 2).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuDaGiaoHang = listDonHang.Where(x => x.TrangThai == 3).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuGiaoThanhCong = listDonHang.Where(x => x.TrangThai == 4).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuGiaoThatBai = listDonHang.Where(x => x.TrangThai == 5).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuDonHuy = listDonHang.Where(x => x.TrangThai == 6).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));

                    ViewBag.slDonHang = slDonHang;
                    ViewBag.slDonHuy = slDonHuy;
                    ViewBag.slDonKhongHuy = slDonKhongHuy;

                    ViewBag.doanhThuKhongHuy = doanhThuKhongHuy?.ToString("N0");
                    ViewBag.doanhThuHuy = doanhThuHuy?.ToString("N0");
                    ViewBag.doanhThuChoXacNhan = doanhThuChoXacNhan?.ToString("N0");
                    ViewBag.doanhThuDangGiaoHang = doanhThuDangGiaoHang?.ToString("N0");
                    ViewBag.doanhThuDaGiaoHang = doanhThuDaGiaoHang?.ToString("N0");
                    ViewBag.doanhThuGiaoThanhCong = doanhThuGiaoThanhCong?.ToString("N0");
                    ViewBag.doanhThuGiaoThatBai = doanhThuGiaoThatBai?.ToString("N0");
                    ViewBag.doanhThuDonHuy = doanhThuDonHuy?.ToString("N0");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        public ActionResult DoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {

                if (tuNgay == null)
                {
                    tuNgay = DateTime.Now;
                }
                if (denNgay == null)
                {
                    denNgay = DateTime.Now;
                }
                ViewBag.tuNgay = tuNgay.ToString("yyyy-MM-dd");
                ViewBag.denNgay = denNgay.ToString("yyyy-MM-dd");

                // gán giá trị mặc định
                ViewBag.slDonHang = 0;
                ViewBag.slDonHuy = 0;
                ViewBag.slDonKhongHuy = 0;

                ViewBag.doanhThuKhongHuy = 0;
                ViewBag.doanhThuHuy = 0;
                ViewBag.doanhThuChoXacNhan = 0;
                ViewBag.doanhThuDangGiaoHang = 0;
                ViewBag.doanhThuDaGiaoHang = 0;
                ViewBag.doanhThuGiaoThanhCong = 0;
                ViewBag.doanhThuGiaoThatBai = 0;
                ViewBag.doanhThuDonHuy = 0;


                var listDonHang = db.DonDatHangs.Where(x => x.NgayDat >= tuNgay && x.NgayDat <= denNgay).ToList();
                if (listDonHang != null && listDonHang.Count > 0)
                {
                    var slDonHang = listDonHang.Count;
                    var slDonHuy = listDonHang.Where(x => x.TrangThai == 5 || x.TrangThai == 6).ToList().Count;
                    var slDonKhongHuy = slDonHang - slDonHuy;

                    var doanhThuKhongHuy = listDonHang.Where(x => x.TrangThai != 5 && x.TrangThai != 6).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuHuy = listDonHang.Where(x => x.TrangThai == 5 || x.TrangThai == 6).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuChoXacNhan = listDonHang.Where(x => x.TrangThai == 1).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuDangGiaoHang = listDonHang.Where(x => x.TrangThai == 2).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuDaGiaoHang = listDonHang.Where(x => x.TrangThai == 3).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuGiaoThanhCong = listDonHang.Where(x => x.TrangThai == 4).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuGiaoThatBai = listDonHang.Where(x => x.TrangThai == 5).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));
                    var doanhThuDonHuy = listDonHang.Where(x => x.TrangThai == 6).ToList().Sum(x => x.ChiTietDonDatHangs.Sum(y => y.SoLuong * y.DonGia));

                    ViewBag.slDonHang = slDonHang;
                    ViewBag.slDonHuy = slDonHuy;
                    ViewBag.slDonKhongHuy = slDonKhongHuy;

                    ViewBag.doanhThuKhongHuy = doanhThuKhongHuy?.ToString("N0");
                    ViewBag.doanhThuHuy = doanhThuHuy?.ToString("N0");
                    ViewBag.doanhThuChoXacNhan = doanhThuChoXacNhan?.ToString("N0");
                    ViewBag.doanhThuDangGiaoHang = doanhThuDangGiaoHang?.ToString("N0");
                    ViewBag.doanhThuDaGiaoHang = doanhThuDaGiaoHang?.ToString("N0");
                    ViewBag.doanhThuGiaoThanhCong = doanhThuGiaoThanhCong?.ToString("N0");
                    ViewBag.doanhThuGiaoThatBai = doanhThuGiaoThatBai?.ToString("N0");
                    ViewBag.doanhThuDonHuy = doanhThuDonHuy?.ToString("N0");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
    }
}