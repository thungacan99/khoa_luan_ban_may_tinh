using BanMayTinh.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace BanMayTinh.Controllers
{
    public class ProductController : Controller
    {
        DBContext context = new DBContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="danhmucId"></param>
        /// <param name="loaisanphamId"></param>
        /// <param name="numberProperty"></param>
        /// <param name="valueProperty"></param>
        /// <param name="page"></param>
        /// <param name="typeSort"></param>
        /// <returns></returns>
        public ActionResult List(int danhmucId, int? loaisanphamId, int? numberProperty, string valueProperty, int? page, string typeSort)
        {
            ViewBag.CurrentSort = typeSort;
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var model = context.SanPhams.Where(x => x.TenSanPham != null && x.LoaiSanPham.Id_DanhMuc == danhmucId);
            if (loaisanphamId.HasValue)
            {
                model = context.SanPhams.Where(x => x.TenSanPham != null && x.LoaiSanPham.Id == loaisanphamId);
            }

            if (numberProperty.HasValue)
            {
                switch (numberProperty)
                {
                    case 1:
                        model.Where(x => x.ThuocTinh1 == valueProperty);
                        break;
                    case 2:
                        model.Where(x => x.ThuocTinh2 == valueProperty);
                        break;
                    case 3:
                        model.Where(x => x.ThuocTinh3 == valueProperty);
                        break;
                    case 4:
                        model.Where(x => x.ThuocTinh4 == valueProperty);
                        break;
                    case 5:
                        model.Where(x => x.ThuocTinh5 == valueProperty);
                        break;
                }
            }

            switch (typeSort)
            {
                case "asc": //thấp đến cao
                    model = model.OrderBy(s => s.DonGia);
                    break;
                case "desc": //cao đến thấp
                    model = model.OrderByDescending(s => s.DonGia);
                    break;
                default: // Không sắp xếp 
                    typeSort = "noSort";
                    model = model.OrderByDescending(s => s.Id);
                    break;
            }
            IPagedList<SanPham> sp = null;
            //return View("Shop", model);
            sp = model.ToPagedList(pageIndex, pageSize);

            var ListLoaiSanPham = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == danhmucId);
            var ListLoaiSanPhamMayTinh = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == 1);
            var ListLoaiSanPhamLinhKien = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == 2);
            var ListLoaiSanPhamManHinh = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == 3);

            string TenDanhMuc = "";
            if (danhmucId == 1)
            {
                TenDanhMuc = "DANH MỤC MÁY TÍNH";
            }
            else if (danhmucId == 2)
            {
                TenDanhMuc = "DANH MỤC LINH KIỆN";
            }
            else if (danhmucId == 3)
            {
                TenDanhMuc = "LOẠI MÀN HÌNH";
            }

            ViewBag.ListLoaiSanPham = ListLoaiSanPham;
            ViewBag.ListLoaiSanPhamMayTinh = ListLoaiSanPhamMayTinh;
            ViewBag.ListLoaiSanPhamLinhKien = ListLoaiSanPhamLinhKien;
            ViewBag.ListLoaiSanPhamManHinh = ListLoaiSanPhamManHinh;
            ViewBag.CurrentSort = typeSort;
            ViewBag.CurrentDanhMuc = danhmucId;
            ViewBag.CurrentLoaiSanPham = loaisanphamId;
            ViewBag.TenDanhMuc = TenDanhMuc;
            return View(sp);
        }

        public ActionResult Shop(int? page, string typeSort)
        {

            ViewBag.CurrentSort = typeSort;
            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var model = context.SanPhams.Where(x => x.TenSanPham != null);
            switch (typeSort)
            {
                case "asc": //thấp đến cao
                    model = model.OrderBy(s => s.DonGia);
                    break;
                case "desc": //cao đến thấp
                    model = model.OrderByDescending(s => s.DonGia);
                    break;
                default: // Không sắp xếp 
                    typeSort = "noSort";
                    model = model.OrderByDescending(s => s.Id);
                    break;
            }
            IPagedList<SanPham> sp = null;
            //return View("Shop", model);
            sp = model.ToPagedList(pageIndex, pageSize);
            ViewBag.CurrentSort = typeSort;
            return View(sp);
        }

        /// <summary>
        /// Chi tiết sản phẩm
        /// </summary>
        /// <param name="id">Mã sản phẩm</param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            var ListLoaiSanPhamMayTinh = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == 1);
            var ListLoaiSanPhamLinhKien = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == 2);
            var ListLoaiSanPhamManHinh = context.LoaiSanPhams.Where(x => x.TenLoaiSanPham != null && x.Id_DanhMuc == 3);
            ViewBag.ListLoaiSanPhamMayTinh = ListLoaiSanPhamMayTinh;
            ViewBag.ListLoaiSanPhamLinhKien = ListLoaiSanPhamLinhKien;
            ViewBag.ListLoaiSanPhamManHinh = ListLoaiSanPhamManHinh;

            var sanpham = context.SanPhams.Find(id);
            var listanh = context.AnhSanPhams.Where(x => x.Id == id).ToList();
            ViewBag.ListAnh = listanh;
            return View("Detail", sanpham);
        }
    }
}