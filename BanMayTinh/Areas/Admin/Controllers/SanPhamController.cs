using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanMayTinh.Models;
using BanMayTinh.Models.DB;
using System.IO;

namespace BanMayTinh.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/SanPham
        public ActionResult Index()
        {
            ProductsModel products = new ProductsModel();
            products.danhSach = db.SanPhams.ToList();
            ViewBag.products = products.danhSach;
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                return View(products);
            }
            return RedirectToAction("Login", "Admin");
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (sanPham == null)
                {
                    return HttpNotFound();
                }
                return View(sanPham);
            }
            return RedirectToAction("Login", "Admin");
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.Id_HangSanXuat = new SelectList(db.HangSanXuats, "Id", "TenHang");
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoaiSanPham");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
            "Id,TenSanPham,Id_HangSanXuat,Id_LoaiSanPham,ThuocTinh1," +
            "ThuocTinh2,ThuocTinh3,ThuocTinh4,ThuocTinh5,DonGia,SoLuong")] SanPham sanPham)
        {
            for (int i = 1; i <= 5; i++)
            {
                var fileIndex = "UL_anh" + i;
                HttpPostedFileBase UL_anh = Request.Files[fileIndex];

                if (UL_anh != null && UL_anh.ContentLength != 0)
                {
                    string fname = Guid.NewGuid() + UL_anh.FileName;
                    int id = sanPham.Id;
                    string ur = Path.Combine(Server.MapPath("~/Content/images/AnhSanPham/"), fname);
                    UL_anh.SaveAs(ur);

                    sanPham.AnhSanPham = fname;
                    AnhSanPham img = new AnhSanPham();
                    img.Id_SanPham = id;
                    img.UR_Anh = fname;
                    db.AnhSanPhams.Add(img);
                }
            }


            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_HangSanXuat = new SelectList(db.HangSanXuats, "Id", "TenHang", sanPham.Id_HangSanXuat);
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.Id_LoaiSanPham);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SanPham sanPham = db.SanPhams.Find(id);
                if (sanPham == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_HangSanXuat = new SelectList(db.HangSanXuats, "Id", "TenHang", sanPham.Id_HangSanXuat);
                ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.Id_LoaiSanPham);
                return View(sanPham);
            }
            return RedirectToAction("Login", "Admin");
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenSanPham,AnhSanPham,Id_HangSanXuat,Id_LoaiSanPham,ThuocTinh1,ThuocTinh2,ThuocTinh3,ThuocTinh4,ThuocTinh5,DonGia,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "SanPham", new { id = sanPham.Id });
            }
            ViewBag.Id_HangSanXuat = new SelectList(db.HangSanXuats, "Id", "TenHang", sanPham.Id_HangSanXuat);
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.Id_LoaiSanPham);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            //trường hợp không có chi tiết đơn hàng nào có sản phẩm này

            var donHangs = db.ChiTietDonDatHangs.Where(x => x.Id_SanPhamMua == id).ToList();
            if (donHangs.Count == 0)
            {
                // 1. xóa hết sản phẩm trong giỏ hàng đi nếu tồn tại
                var chiTietGH = db.ChiTietGioHangs.Where(x => x.Id_SanPham == id).ToList();

                if (chiTietGH.Count > 0)
                {
                    db.ChiTietGioHangs.RemoveRange(chiTietGH);
                }

                // tìm ảnh sản phẩm và xóa đi

                var anhSP = db.AnhSanPhams.Where(x => x.Id_SanPham == id).ToList();
                if (anhSP.Count > 0)
                {
                    db.AnhSanPhams.RemoveRange(anhSP);
                }

                // thực hiện xóa sản phẩm đi
                var sanPham = db.SanPhams.Where(x => x.Id == id).FirstOrDefault();
                db.SanPhams.Remove(sanPham);

                db.SaveChanges();
            }

            return RedirectToAction("Index", "SanPham");

            //trường hợp đã có chi tiết đơn hàng có sp này
            //không cho phép xóa




            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //SanPham sanPham = db.SanPhams.Find(id);
            //if (sanPham == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
