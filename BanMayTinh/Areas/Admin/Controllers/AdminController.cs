using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanMayTinh.Models.DB;
using BanMayTinh.Models;

namespace BanMayTinh.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        DBContext dbContext = new DBContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen==1)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        [HttpPost]
        public ActionResult LogIn(TaiKhoan taiKhoan, String returnURL)
        {
            var result = dbContext.TaiKhoans.Where(a => a.UserName.Equals(taiKhoan.UserName) &&
                                                        a.Password.Equals(taiKhoan.Password) && 
                                                        a.Quyen == 1).FirstOrDefault();
            if (result != null)
            {
                Session["LogIn"] = result;
                if (String.IsNullOrEmpty(returnURL))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return Redirect(returnURL);
                }
            }
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult Login()
        {
            return View();
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
                tkMoi.Email = taiKhoan.Email;
                tkMoi.DiaChi = taiKhoan.DiaChi;

                dbContext.TaiKhoans.Add(tkMoi);

                GioHang gh = new GioHang();
                gh.UserNameKH = taiKhoan.UserName;
                dbContext.GioHangs.Add(gh);
                dbContext.SaveChanges();




                Session["logIn"] = tkMoi;
                return RedirectToAction("Index", "Admin");
            }
            else
            {

                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["LogIn"] = null;
            return RedirectToAction("Index");
        }
    }
}