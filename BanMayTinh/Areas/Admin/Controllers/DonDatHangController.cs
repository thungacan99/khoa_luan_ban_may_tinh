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
    public class DonDatHangController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/DonDatHang
        public ActionResult Index()
        {
            var list = db.DonDatHangs.ToList();
            return View("Index", list);
        }

        // GET: Admin/DonDatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }

            var listHang = new List<SanPham>();
            foreach (var ddh in donDatHang.ChiTietDonDatHangs)
            {
                listHang.Add(ddh.SanPham);
            }

            ViewBag.ListSanPham = listHang;
            Console.WriteLine("List San pham = ", listHang);
            Console.WriteLine("ViewBag.ListSanPham = ", ViewBag.ListSanPham);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DonDatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenDonDatHang,ThuocTinh1,ThuocTinh2,ThuocTinh3,ThuocTinh4,ThuocTinh5,Id_DanhMuc")] DonDatHang loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.DonDatHangs.Add(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSanPham);
        }

        // GET: Admin/DonDatHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang loaiSanPham = db.DonDatHangs.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/DonDatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenDonDatHang,ThuocTinh1,ThuocTinh2,ThuocTinh3,ThuocTinh4,ThuocTinh5,Id_DanhMuc")] DonDatHang loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);
        }

        // GET: Admin/DonDatHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang loaiSanPham = db.DonDatHangs.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/DonDatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonDatHang loaiSanPham = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(loaiSanPham);
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
