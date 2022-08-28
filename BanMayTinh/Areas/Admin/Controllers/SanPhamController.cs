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
            return View(products);
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
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
            return View(sanPham);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = 
            "Id,TenSanPham,AnhSanPham,Id_HangSanXuat,Id_LoaiSanPham,ThuocTinh1," +
            "ThuocTinh2,ThuocTinh3,ThuocTinh4,ThuocTinh5,DonGia")] SanPham sanPham, HttpPostedFileBase UL_anh)
        {
            string fname = UL_anh.FileName;
            int id = sanPham.Id;
            string ur = Path.Combine(Server.MapPath("~/Content/images/AnhSanPham/"), fname);
            UL_anh.SaveAs(ur);

            SanPham sp = db.SanPhams.FirstOrDefault(x => x.Id == id);
            sp.AnhSanPham = fname;

            AnhSanPham img = new AnhSanPham();
            img.Id_SanPham = id;
            img.UR_Anh = fname;
            db.AnhSanPhams.Add(img);

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

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenSanPham,AnhSanPham,Id_HangSanXuat,Id_LoaiSanPham,ThuocTinh1,ThuocTinh2,ThuocTinh3,ThuocTinh4,ThuocTinh5,DonGia")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_HangSanXuat = new SelectList(db.HangSanXuats, "Id", "TenHang", sanPham.Id_HangSanXuat);
            ViewBag.Id_LoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.Id_LoaiSanPham);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
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
            return View(sanPham);
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
