using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VienShops.Models;
namespace VienShops.Controllers
{
    public class CartController : Controller
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();
        #region Cart
        // GET Cart & Check Cart
        public List<Cart> GetCart()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                // if cart null -> create session cart
                listCart = new List<Cart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }

        // Add cart
        public ActionResult AddCart(int sMaSP, string sUrl)
        {
            SANPHAM sanpham = Db.SANPHAMs.SingleOrDefault(n => n.MASP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy ra sesion giỏ hàng
            List<Cart> listCart = GetCart();
            // Tìm xem sản phẩm có trong giỏ hàng hay chưa - session
            Cart c = listCart.Find(n => n.sMaSP == sMaSP);
            if (c == null)
            {
                c = new Cart(sMaSP);
                // Add sản phẩm mới thêm vào list
                listCart.Add(c);
                return Redirect(sUrl);
            }
            else
            {
                c.iSoLuong++;
                return Redirect(sUrl);
            }
        }
        // Update cart
        public ActionResult UpdateCart(int sMaSP, FormCollection f)
        {
            // Kiểm tra Mã SP
            SANPHAM sanpham = Db.SANPHAMs.SingleOrDefault(n => n.MASP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = GetCart();
            Cart c = listCart.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (c != null)
            {
                c.iSoLuong = int.Parse(f["txtQuantity"].ToString());

            }
            return RedirectToAction("Cart");
        }
        // Delete Cart
        public ActionResult DeleteCart(int sMaSP)
        {
            // Kiểm tra Mã SP
            SANPHAM sanpham = Db.SANPHAMs.SingleOrDefault(n => n.MASP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = GetCart();
            Cart c = listCart.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (c != null)
            {
                listCart.RemoveAll(n => n.sMaSP == sMaSP);

            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        // Xây dựng trang giỏ hàng
        public ActionResult Cart()
        {
            // If gio hàng null trả về trang chủ
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Total = Total();
            List<Cart> listCart = GetCart();
            return View(listCart);
        }
        // Xây dựng tính tổng số lượng
        private int AllQuantity()
        {
            int iAllQuantity = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                iAllQuantity = listCart.Sum(n => n.iSoLuong);
            }
            return iAllQuantity;
        }
        // Tính tổng tiền
        private double Total()
        {
            double dTotal = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                dTotal = listCart.Sum(n => n.dThanhTien);
            }
            return dTotal;
        }
        // Đếm số lượng trong giỏ hàng hiển thị ở Header, icon cart
        public ActionResult CartLayoutHeader()
        {
            if (Total() == 0)
            {
                return PartialView();
            }
            ViewBag.AllQuantity = AllQuantity();
            ViewBag.Total = Total();
            return PartialView();
        }
    }
    #endregion
    #region Order

    #endregion
}