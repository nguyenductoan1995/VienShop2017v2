using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VienShops.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace VienShops.Controllers
{
    public class AdminController : Controller
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();
        
        public ActionResult Welcome()
        {
            return View();
        }

        // GET: Admin
        public ActionResult AdminHome(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(Db.SANPHAMs.ToList().OrderBy(n=>n.TENSP).ToPagedList(pageNumber,pageSize));
        }
        // Thêm mới sản phẩm
        [HttpGet]
        public ActionResult AddProductNew()
        {
            // Đưa vào dropdown

            ViewBag.MaLoaiSP = new SelectList(Db.LOAISANPHAMs.ToList(),"MaLoaiSP","TenLoai");
            return View();
        }
        // Up load file ảnh
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProductNew(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
           
            ViewBag.MaLoaiSP = new SelectList(Db.LOAISANPHAMs.ToList(), "MaLoaiSP", "TenLoai");
            if(fileUpload == null)
            {
                ViewBag.Notification = "Mời bạn chọn hình ảnh";
                return View();
            }
            // Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                // Lưu tên file 
                var fileName = Path.GetFileName(fileUpload.FileName);
                // Lưu đường dẫn file
                var path = Path.Combine(Server.MapPath("~/images/products/large"), fileName);
                // Kiểm tra hình ảnh đã tồn tại
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Notification = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sanpham.URL = fileUpload.FileName;
                Db.SANPHAMs.InsertOnSubmit(sanpham);
                Db.SubmitChanges();
            }
            return View();
        }
        // ------------------------------------------------------------------
        [HttpGet]
        public ActionResult EditProduct(int MaSP)
        {
            SANPHAM sanpham = Db.SANPHAMs.SingleOrDefault(n => n.MASP == MaSP);
           // LOAISANPHAM loaisanpham = Db.LOAISANPHAMs.SingleOrDefault(n => n.MALOAISP == sanpham.MALOAISP);
            if(sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoaiSP = new SelectList(Db.LOAISANPHAMs.ToList(), "MaLoaiSP", "TenLoai",sanpham.MALOAISP);

           // ViewBag.TenLoai = loaisanpham.TENLOAI.ToString();
            return View(sanpham);
        }

        [HttpPost]
         [ValidateInput(false)]
        public ActionResult EditProduct(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
            SANPHAM sanpham1 = Db.SANPHAMs.SingleOrDefault(n => n.MASP == sanpham.MASP); 

            if(fileUpload == null)
            {
                sanpham1.TENSP = sanpham.TENSP;
                sanpham1.MALOAISP = sanpham.MALOAISP;
                sanpham1.GIA = sanpham.GIA;
                sanpham1.NGAYCAPNHAT = sanpham.NGAYCAPNHAT;
                sanpham1.CHATLIEU = sanpham.CHATLIEU;
                sanpham1.MOTA = sanpham.MOTA;
            }
            else
            {
                sanpham1.TENSP = sanpham.TENSP;
                sanpham1.MALOAISP = sanpham.MALOAISP;
                sanpham1.GIA = sanpham.GIA;
                sanpham1.NGAYCAPNHAT = sanpham.NGAYCAPNHAT;
                sanpham1.CHATLIEU = sanpham.CHATLIEU;
                sanpham1.MOTA = sanpham.MOTA;
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/images/products/large"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Notification = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sanpham1.URL = fileUpload.FileName;
            }
          

            ViewBag.MaLoaiSP = new SelectList(Db.LOAISANPHAMs.ToList(), "MaLoaiSP", "TenLoai",sanpham.MALOAISP);
            Db.SubmitChanges();
            return RedirectToAction("AdminHome","Admin");
        }
        // Xóa sản phẩm
        public ActionResult DeleteProduct(int MaSP)
        {
            SANPHAM sanpham = Db.SANPHAMs.SingleOrDefault(n => n.MASP == MaSP);
            // LOAISANPHAM loaisanpham = Db.LOAISANPHAMs.SingleOrDefault(n => n.MALOAISP == sanpham.MALOAISP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        // Xác nhận xóa
        [HttpPost,ActionName("DeleteProduct")]
        public ActionResult ConfirmDelete(int MaSP)
        {
            SANPHAM sanpham = Db.SANPHAMs.SingleOrDefault(n => n.MASP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Db.SANPHAMs.DeleteOnSubmit(sanpham);
            Db.SubmitChanges();
            return RedirectToAction("AdminHome","Admin");
        }
        // Quản lý đơn hàng
        public ActionResult OrderManagement()
        {
            //var entryPoint = (from ep in Db.AspNetUsers
            //                  join e in Db.DONHANGs on ep.Id equals e.Id
            //                  join t in Db.CHITIETDONHANGs on e.MADH equals t.MADH
            //                  join h in Db.SANPHAMs on t.MASP equals h.MASP
            //                  select new
            //                  {
            //                      Email = ep.Email,
            //                      TinhTrang = e.TINHTRANG,
            //                      SoLuong = t.SOLUONG,
            //                      TenSanPham = h.TENSP
            //                  }).ToList();
            //var entryPoint = from p in Db.DONHANGs
            //                         from od in Db.CHITIETDONHANGs
            //                         from o in Db.SANPHAMs
            //                         from n in Db.AspNetUsers
            //                         where p.MADH == od.MADH && od.MASP == o.MASP && p.Id == n.Id
            //                         select new 
            //                         {
            //                             TINHTRANG = p.TINHTRANG,
            //                             TENSPg = o.TENSP,
            //                             SOLUONGg = od.SOLUONG
            //                         };
            return View(Db.DONHANGs.ToList().OrderBy(n=>n.MADH));
        }
    }
}