using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanMayTinh.Models.DB;
using BanMayTinh.Models;

namespace BanMayTinh.Controllers
{
    public class HomeController : Controller
    {
        DBContext dbContext = new DBContext();

        public ActionResult Index()
        {
            ProductsModel products = new ProductsModel();
            products.danhSach = dbContext.SanPhams.OrderByDescending(u => u.Id).Take(12).ToList();
            return View(products);
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(TaiKhoan taiKhoan, String returnURL)
        {
            var result = dbContext.TaiKhoans.Where(a => a.UserName.Equals(taiKhoan.UserName) &&
                                                        a.Password.Equals(taiKhoan.Password)).FirstOrDefault();
            if (result != null)
            {
                Session["LogIn"] = result;
                if (String.IsNullOrEmpty(returnURL))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnURL);
                }
            }
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["LogIn"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TaiKhoan taiKhoan)
        {
            var result = dbContext.TaiKhoans.Where(a => a.UserName.Equals(taiKhoan.UserName) &&
                                                        a.Password.Equals(taiKhoan.Password)).FirstOrDefault();
            if (result == null && ModelState.IsValid)
            {
                TaiKhoan tkMoi = new TaiKhoan();
                tkMoi.UserName = taiKhoan.UserName;
                tkMoi.Password = taiKhoan.Password;
                tkMoi.TenKhachHang = taiKhoan.TenKhachHang;
                tkMoi.Email = taiKhoan.Email;
                tkMoi.DiaChi = taiKhoan.DiaChi;
                tkMoi.Quyen = 0;

                dbContext.TaiKhoans.Add(tkMoi);

                GioHang gh = new GioHang();
                gh.UserNameKH = taiKhoan.UserName;
                dbContext.GioHangs.Add(gh);
                dbContext.SaveChanges();


                Session["logIn"] = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View();
            }
        }


        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Single()
        {
            return View();
        }

        public ActionResult Laptop77()
        {
            return View();
        }
    }
}