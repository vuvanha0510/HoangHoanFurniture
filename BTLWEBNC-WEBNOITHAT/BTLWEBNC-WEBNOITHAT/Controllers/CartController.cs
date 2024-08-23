using BTLWEBNC_WEBNOITHAT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTLWEBNC_WEBNOITHAT.Controllers
{
    public class CartController : Controller
    {
        QlbanNoiThatContext db = new QlbanNoiThatContext();
        private readonly QlbanNoiThatContext _context;
        private readonly ILogger<CartController> _logger;
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        public static List<TblChiTietGioHang1> chitietgiohang = new List<TblChiTietGioHang1>();

        public IActionResult GioHang()
        {
            var userClaims = User.Identity as ClaimsIdentity;
            int idLogin = 0;
            if (userClaims.IsAuthenticated)
            {
                // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                var idClaim = userClaims.FindFirst("idGioHang");
                var tenusername = userClaims.FindFirst("UseName");
                if (idClaim != null)
                {
                    idLogin = int.Parse(idClaim.Value);
                    // Now you have the integer value in idLogin
                }
                if (usernameClaim != null)
                {
                    string username1 = tenusername.Value;
                    string username = usernameClaim.Value;
                    TempData["Username"] = username1;
                    TempData["LoginData"] = username;
                    // Now, 'username' contains the value of 
                    // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                }

                var sanPham = db.TblChiTietGioHang1s.Where(x => x.IddonHang == idLogin).ToList();
                string maSP;
                double tongSoTien = 0;
                int sogiohang = 0;

                for (int i = 0; i < sanPham.Count(); i++)
                {
                    maSP = db.TChiTietSanPhams.SingleOrDefault(x => x.MaChiTietSp == sanPham[i].IdchiTietSanPham).MaSp;


                    if (sanPham[i].TenSp == null)
                    {
                        sanPham[i].TenSp = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).TenSp;

                    }
                    if (sanPham[i].SoLuong == null)
                    {
                        sanPham[i].SoLuong = 1;

                    }
                    if (sanPham[i].HinhAnh == null)
                    {
                        sanPham[i].HinhAnh = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).AnhDaiDien;

                    }
                    if (sanPham[i].ThanhTien == null)
                    {
                        sanPham[i].ThanhTien = (int)db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).GiaLonNhat;

                    }
                    if (sanPham[i].Check1 == true)
                    {
                        tongSoTien += sanPham[i].TongTien.GetValueOrDefault();
                        sogiohang += sanPham[i].SoLuong.GetValueOrDefault();
                    }
                    db.SaveChanges();
                }
                TempData["TongSotien"] = tongSoTien;
                TempData["SoGioHang"] = sogiohang;
                return View(sanPham);
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = db.TblChiTietGioHang1s.SingleOrDefault(x => x.IdchiTietDonHang == id);
            if (sanPham != null)
            {
                db.TblChiTietGioHang1s.Remove(sanPham);
                db.SaveChangesAsync();


            }
            return RedirectToAction("GioHang");
        }

        public async Task<IActionResult> Thaydoi(int id, string data)
        {
            var sanPham = db.TblChiTietGioHang1s.SingleOrDefault(x => x.IdchiTietDonHang == id);
            if (data == "+")
            {
                sanPham.SoLuong += 1;
            }
            else if (data == "-")
            {
                sanPham.SoLuong -= 1;
            }
            db.TblChiTietGioHang1s.Update(sanPham);
            db.SaveChangesAsync();
            return RedirectToAction("GioHang");
        }
        public async Task<IActionResult> checkThayDoi(int id, Boolean isCheked)
        {
            var sanPham1 = db.TblChiTietGioHang1s.SingleOrDefault(x => x.IdchiTietDonHang == id);
            sanPham1.Check1 = isCheked;
            db.SaveChanges();
            var sanPham = db.TblChiTietGioHang1s.Where(x => x.IddonHang == 1).ToList();
            string maSP;
            double tongSoTien = 0;
            int sogiohang = 0;

            for (int i = 0; i < sanPham.Count(); i++)
            {
                maSP = db.TChiTietSanPhams.SingleOrDefault(x => x.MaChiTietSp == sanPham[i].IdchiTietSanPham).MaSp;
                sanPham[i].TenSp = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).TenSp;


                if (sanPham[i].Check1 == true)
                {
                    tongSoTien += sanPham[i].TongTien.GetValueOrDefault();
                    sogiohang += sanPham[i].SoLuong.GetValueOrDefault();
                }
                TempData["TongSotien"] = tongSoTien;
                TempData["SoGioHang"] = sogiohang;

            }

            return PartialView("giohangPartialView");
        }
    }
}
