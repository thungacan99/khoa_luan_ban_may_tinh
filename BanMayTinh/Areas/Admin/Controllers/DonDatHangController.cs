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
            var list = db.DonDatHangs.OrderByDescending(x => x.Id).ToList();
            ViewBag.list = list;
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                return View("Index", ViewBag.list);
            }
            return RedirectToAction("Login", "Admin");
        }


        public ActionResult TimKiemDonHang(String name)
        {
            //DonDatHang ddh = new DonDatHang();
            //ddh = db.DonDatHangs.Where(x => x.Id == id).FirstOrDefault();
            var list = db.DonDatHangs.Where(x => x.UserNameKH.Contains(name) || 
                                                 x.SoDienThoaiNguoiNhan.Contains(name) ||
                                                 x.Id.ToString().Contains(name)).ToList();
            ViewBag.list = list;
            return View("Index", ViewBag.list);
            //ProductsModel rs = new ProductsModel();
            //rs.danhSach = db.SanPhams.Where(u => u.TenSanPham.Contains(name)).ToList();
            //return View(rs);
        }

        public ActionResult DSTrangThai(int? trangthai)
        {
            var list = db.DonDatHangs.Where(x => x.TrangThai == trangthai).ToList();
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                return View(list);
            }
            return RedirectToAction("Login", "Admin");
        }

        // GET: Admin/DonDatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);

            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            var listHang = donDatHang.ChiTietDonDatHangs;
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                if (donDatHang == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ListSanPham = listHang;
                return View(donDatHang);
            }
            return RedirectToAction("Login", "Admin");
        }

        // GET: Admin/DonDatHang/Create
        public ActionResult Create()
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                return View();
            }
            return RedirectToAction("Login", "Admin");
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
            DonDatHang loaiSanPham = db.DonDatHangs.Find(id);
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (loaiSanPham == null)
                {
                    return HttpNotFound();
                }
                return View(loaiSanPham);
            }
            return RedirectToAction("Login", "Admin");
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

        /// <summary>
        /// Hàm này để cập nhật trạng thái của đơn hàng
        /// </summary>
        /// <param name="donDH"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CapNhatTTDonHang(int idDonHang, int trangThai = -1)
        {
            var donhang = db.DonDatHangs.Where(a => a.Id == idDonHang).FirstOrDefault();
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null && dangnhap.Quyen == 1)
            {
                if (trangThai != -1)
                {
                    donhang.TrangThai = (int)trangThai;
                }
                else
                {
                    donhang.TrangThai = donhang.TrangThai + 1;
                }
                db.Entry(donhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "DonDatHang", new { @id = idDonHang });
            }
            return RedirectToAction("Login", "Admin");
        }

        /// <summary>
        /// Thực hiện chỉnh sửa thông tin đơn hàng
        /// </summary>
        /// <param name="donDH"></param>
        /// <returns></returns>
        public ActionResult ChinhSuaTTDonHang([Bind(Include = "Id, UserNameKH, " +
            "SoDienThoaiNguoiNhan, NgayDat, NgayGiao, TenNguoiNhan, DiaChi, YeuCau, TrangThai")] DonDatHang donDH)
        {
            var donhang = db.DonDatHangs.Where(a => a.Id == donDH.Id).FirstOrDefault();
            donhang.SoDienThoaiNguoiNhan = donDH.SoDienThoaiNguoiNhan;
            donhang.TenNguoiNhan = donDH.TenNguoiNhan;
            donhang.DiaChi = donDH.DiaChi;
            donhang.NgayGiao = donDH.NgayGiao;
            donhang.YeuCau = donDH.YeuCau;
            db.Entry(donhang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "DonDatHang", new { @id = donDH.Id });
        }

    }
}
