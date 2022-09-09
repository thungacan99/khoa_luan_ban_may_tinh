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
    public class CartController : Controller
    {
        DBContext dbContext = new DBContext();

        public ActionResult GioHang()
        {
            ViewBag.TongTien = 0;

            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            if (dangnhap != null)
            {
                GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault();
                //GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault();

                return View(gioHang);
            }

            return RedirectToAction("LogIn", "Home");

        }

        public ActionResult ThemSanPham(int MaSP)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];

            if (dangnhap != null)
            {
                var ctGioHang = (from n in dbContext.GioHangs
                                 join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
                                 where n.UserNameKH.Equals(dangnhap.UserName) && m.Id_SanPham == MaSP
                                 select m).FirstOrDefault();

                //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault());
                //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault());

                //SanPham sp = (dbContext.SanPhams.Where(y => y.Id == MaSP).FirstOrDefault());

                //ChiTietGioHang chiTietGH = new ChiTietGioHang();
                //chiTietGH.Id_GioHang = gh.Id_GioHang;
                //chiTietGH.Id_SanPham = sp.Id;
                //chiTietGH.DonGia = sp.DonGia;
                //chiTietGH.SoLuong = 1;

                //dbContext.ChiTietGioHangs.Add(chiTietGH);
                //dbContext.SaveChanges();



                if (ctGioHang == null)
                {
                    GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault());
                    //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault());

                    SanPham sp = (dbContext.SanPhams.Where(y => y.Id == MaSP).FirstOrDefault());

                    ChiTietGioHang chiTietGH = new ChiTietGioHang();
                    chiTietGH.Id_GioHang = gh.Id_GioHang;
                    chiTietGH.Id_SanPham = sp.Id;
                    chiTietGH.DonGia = sp.DonGia;
                    chiTietGH.SoLuong = 1;

                    dbContext.ChiTietGioHangs.Add(chiTietGH);

                    dbContext.SaveChanges();
                }
                else
                {
                    var ctGioHang2 = (from n in dbContext.GioHangs
                                      join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
                                      where n.UserNameKH.Equals(dangnhap.UserName) && m.Id_SanPham == MaSP
                                      select m).FirstOrDefault();


                    int sl = (int)ctGioHang2.SoLuong + 1;
                    cnsl(MaSP, sl);
                }
                return RedirectToAction("GioHang", "Cart");
            }

            return RedirectToAction("LogIn", "Home");
        }

        [HttpPost]
        public void cnsl(int masp, int sl)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];

            GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault());
            //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault());

            ChiTietGioHang ctGioHang = (from n in dbContext.GioHangs
                                        join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
                                        where n.UserNameKH.Equals(dangnhap.UserName) && m.Id_SanPham == masp
                                        select m).FirstOrDefault();

            //ChiTietGioHang ctGioHang = (from n in dbContext.GioHangs
            //                            join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
            //                            where n.UserNameKH.Equals("0394362405") && m.Id_SanPham == maSP
            //                            select m).FirstOrDefault();

            ctGioHang.SoLuong = sl;
            dbContext.Entry(ctGioHang).State = EntityState.Modified;
            dbContext.SaveChanges();

            //return RedirectToAction("GioHang");
        }
        [HttpPost]
        public ActionResult CapNhapSoLuong(int maSP, int soLuong)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            SanPham sp = dbContext.SanPhams.Where(x => x.Id == maSP).FirstOrDefault();
            if (soLuong > sp.SoLuong)
            {
                soLuong = (int)sp.SoLuong;
            }
            GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault());
            //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault());

            ChiTietGioHang ctGioHang = (from n in dbContext.GioHangs
                                        join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
                                        where n.UserNameKH.Equals(dangnhap.UserName) && m.Id_SanPham == maSP
                                        select m).FirstOrDefault();

            //ChiTietGioHang ctGioHang = (from n in dbContext.GioHangs
            //                            join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
            //                            where n.UserNameKH.Equals("0394362405") && m.Id_SanPham == maSP
            //                            select m).FirstOrDefault();

            ctGioHang.SoLuong = soLuong;
            dbContext.Entry(ctGioHang).State = EntityState.Modified;
            dbContext.SaveChanges();

            return RedirectToAction("GioHang");
        }

        public ActionResult XoaSP(long maSP)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];

            GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault());

            //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault());

            ChiTietGioHang ctGioHang = (from n in dbContext.GioHangs
                                        join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
                                        where n.UserNameKH.Equals(dangnhap.UserName) && m.Id_SanPham == maSP
                                        select m).FirstOrDefault();

            //ChiTietGioHang ctGioHang = (from n in dbContext.GioHangs
            //                            join m in dbContext.ChiTietGioHangs on n.Id_GioHang equals m.Id_GioHang
            //                            where n.UserNameKH.Equals("0394362405") && m.Id_SanPham == maSP
            //                            select m).FirstOrDefault();

            dbContext.ChiTietGioHangs.Remove(ctGioHang);
            dbContext.SaveChanges();

            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang()
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];

            GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault());
            //GioHang gh = (dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault());

            string sql = "select *from ChiTietGioHang where Id_GioHang =" + gh.Id_GioHang.ToString();
            var listChiTietGioHang = dbContext.ChiTietGioHangs.SqlQuery(sql).ToList();

            if (listChiTietGioHang.Count > 0)
            {
                foreach (ChiTietGioHang ct in listChiTietGioHang)
                {
                    dbContext.ChiTietGioHangs.Remove(ct);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult DatHang()
        {
            return View();
        }

        [HttpPost]
        public ActionResult XacNhanDatHang([Bind(Include = "Id, UserNameKH, " +
            "SoDienThoaiNguoiNhan, NgayDat, NgayGiao, TenNguoiNhan, DiaChi, YeuCau")] DonDatHang donDH)
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault();
            //GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault();

            donDH.TrangThai = 1;
            dbContext.DonDatHangs.Add(donDH);
            dbContext.SaveChanges();

            string maDonHang = "select top 1 * from DonDatHang where UserNameKH ='" + dangnhap.UserName.ToString() + "'order by Id desc";
            //string maDonHang = "select top 1 * from DonDatHang where UserNameKH ='"+"0394362405" + "'order by Id desc";

            DonDatHang donTemp = dbContext.DonDatHangs.SqlQuery(maDonHang).FirstOrDefault();
            ThemChiTietDat_CSDL(gioHang, donTemp.Id);

            var ctDonDatHang = donDH.ChiTietDonDatHangs;
            var listIdSpMua = ctDonDatHang.Select(x => x.Id_SanPhamMua);
            var spDatHang = dbContext.SanPhams.Where(x => listIdSpMua.Contains(x.Id));
            foreach (SanPham spDat in spDatHang)
            {
                var spDonHang = ctDonDatHang.Where(x => x.Id_SanPhamMua == spDat.Id).FirstOrDefault();
                spDat.SoLuong = spDat.SoLuong - spDonHang.SoLuong;
                dbContext.Entry(spDat).State = EntityState.Modified;
            }
            dbContext.SaveChanges();

            return RedirectToAction("CheckOut", "Home");
            //return RedirectToAction("ChiTietDatHang");
        }

        public void ThemChiTietDat_CSDL(GioHang gh, int maDonHang)
        {
            string sql = "select *from ChiTietGioHang where Id_GioHang =" + gh.Id_GioHang.ToString();
            var listChiTietGioHang = dbContext.ChiTietGioHangs.SqlQuery(sql).ToList();

            foreach (ChiTietGioHang ct in listChiTietGioHang)
            {
                ChiTietDonDatHang ctDonHang = new ChiTietDonDatHang();
                ctDonHang.Id_DonDathang = maDonHang;
                ctDonHang.Id_SanPhamMua = ct.Id_SanPham;
                ctDonHang.SoLuong = ct.SoLuong;
                ctDonHang.DonGia = ct.DonGia;

                dbContext.ChiTietDonDatHangs.Add(ctDonHang);
                dbContext.SaveChanges();

                dbContext.ChiTietGioHangs.Remove(ct);
                dbContext.SaveChanges();
            }
        }

        public ActionResult ChiTietDatHang()
        {
            TaiKhoan dangnhap = (TaiKhoan)Session["LogIn"];
            TienDonHang tienDonHang = new TienDonHang();

            GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals(dangnhap.UserName)).FirstOrDefault();
            //GioHang gioHang = dbContext.GioHangs.Where(x => x.UserNameKH.Equals("0394362405")).FirstOrDefault();

            string sql = "select *from ChiTietGioHang where Id_GioHang =" + gioHang.Id_GioHang.ToString();
            var listChiTietGioHang = dbContext.ChiTietGioHangs.SqlQuery(sql).ToList();

            tienDonHang.gioHang = gioHang;
            tienDonHang.tongTien = 0;

            foreach (ChiTietGioHang ct in listChiTietGioHang)
            {
                tienDonHang.tongTien += (int)ct.SoLuong * (int)ct.DonGia;
            }

            //ViewBag.TongTien = tienDonHang.tongTien;
            ViewData["TongTien"] = tienDonHang.tongTien;

            return View(tienDonHang);
        }


        [HttpPost]
        public ActionResult ChinhSuaTTDonHang([Bind(Include = "Id, UserNameKH, " +
            "SoDienThoaiNguoiNhan, NgayDat, NgayGiao, TenNguoiNhan, DiaChi, YeuCau, TrangThai")] DonDatHang donDH)
        {
            var donhang = dbContext.DonDatHangs.Where(a => a.Id == donDH.Id).FirstOrDefault();
            donhang.SoDienThoaiNguoiNhan = donDH.SoDienThoaiNguoiNhan;
            donhang.TenNguoiNhan = donDH.TenNguoiNhan;
            donhang.DiaChi = donDH.DiaChi;
            donhang.NgayGiao = donDH.NgayGiao;
            donhang.YeuCau = donDH.YeuCau;
            dbContext.Entry(donhang).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("CTDonHang", "DonHang", new { @idDonHang = donDH.Id });
        }

        /// <summary>
        /// Hàm này để cập nhật trạng thái của đơn hàng
        /// </summary>
        /// <param name="donDH"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CapNhatTTDonHang(int idDonHang, int trangThai = -1)
        {
            var donhang = dbContext.DonDatHangs.Where(a => a.Id == idDonHang).FirstOrDefault();
            if (trangThai != -1)
            {
                donhang.TrangThai = (int)trangThai;
            }
            else
            {
                donhang.TrangThai = donhang.TrangThai + 1;
            }
            dbContext.Entry(donhang).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("CTDonHang", "DonHang", new { @idDonHang = idDonHang });
        }
    }
}