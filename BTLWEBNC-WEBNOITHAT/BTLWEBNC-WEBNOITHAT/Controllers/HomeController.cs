using BTLWEBNC_WEBNOITHAT.Models;
using BTLWEBNC_WEBNOITHAT.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using System.Security.Claims;
using System.Linq;

namespace BTLWEBNC_WEBNOITHAT.Controllers
{
    public class HomeController : Controller
    {
        QlbanNoiThatContext db = new QlbanNoiThatContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index(int? page, string tensps)
        //{
        //    int pageSize = 8;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;
        //    var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
        //    PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);

        //    return View(lst);
        //}
       
        public IActionResult Index(int? page, string timKiem)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            if (userClaims != null)
            {
                // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                var idLogin = userClaims.FindFirst("IDUseName");
                var tenusername = userClaims.FindFirst("UseName");
                if (usernameClaim != null)
                {
                    string username1 = tenusername.Value;
                    string username = usernameClaim.Value;
                    TempData["Username"] = username1;
                    TempData["LoginData"] = username;
                    // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                }

                // You can also access your custom claim ("OtherProperties" in this case) in a similar manner.
            }

            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;


            if (timKiem == null)
            {

                var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
                PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);


                return View(lst);
            }
            else
            {
                var sanphamTimKiem = db.TDanhMucSps.AsNoTracking().Where(x => x.TenSp.Trim().ToUpper().Contains(timKiem.Trim().ToUpper()));
                PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(sanphamTimKiem, pageNumber, pageSize);
                return View(lst);
            }


        }
        public IActionResult SanPhamTheoLoai(string maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }

        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }

        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel { danhMucSp = sanPham, anhSps = anhSanPham };
            return View(homeProductDetailViewModel);
        }
        public IActionResult Gioithieu()
        {
            var userClaims = User.Identity as ClaimsIdentity;
            if (userClaims != null)
            {
                // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                var idLogin = userClaims.FindFirst("IDUseName");
                var tenusername = userClaims.FindFirst("UseName");
                if (usernameClaim != null)
                {
                    string username1 = tenusername.Value;
                    string username = usernameClaim.Value;
                    TempData["Username"] = username1;
                    TempData["LoginData"] = username;
                    // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                }

                // You can also access your custom claim ("OtherProperties" in this case) in a similar manner.
            }
            return View();
        }
        public IActionResult TatCaSanPham(int? page, string timKiem,decimal? to,decimal? from)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            if (userClaims != null)
            {
                // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                var idLogin = userClaims.FindFirst("IDUseName");
                var tenusername = userClaims.FindFirst("UseName");
                if (usernameClaim != null)
                {
                    string username1 = tenusername.Value;
                    string username = usernameClaim.Value;
                    TempData["Username"] = username1;
                    TempData["LoginData"] = username;
                    // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                }

                // You can also access your custom claim ("OtherProperties" in this case) in a similar manner.
            }

            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;


            if (timKiem == null)
            {

                var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
                PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
                return View(lst);
            }
            else
            {
                if(to != null && from != null)
                {
                    var sanphamTimKiem = db.TDanhMucSps.AsNoTracking().Where(x => x.TenSp.Trim().ToUpper().Contains(timKiem.Trim().ToUpper()) && x.GiaLonNhat >= to && x.GiaLonNhat<= from);
                    PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(sanphamTimKiem, pageNumber, pageSize);
                    return View(lst);
                }
                else
                {
                    var sanphamTimKiem = db.TDanhMucSps.AsNoTracking().Where(x => x.TenSp.Trim().ToUpper().Contains(timKiem.Trim().ToUpper()));
                    PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(sanphamTimKiem, pageNumber, pageSize);
                    return View(lst);
                }
                
            }
            //int pageSize = 8;
            //int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            //PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
            //return View(lst);
            //var lstAllSanPham = db.TDanhMucSps.ToList();
            //return View(lstAllSanPham);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddToCardDetail(String MaSp1, int SoLuong)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == MaSp1);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == MaSp1).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel { danhMucSp = sanPham, anhSps = anhSanPham };
            var userClaims = User.Identity as ClaimsIdentity;
            string maKH = "";
            if (userClaims.IsAuthenticated)
            {
                if (userClaims != null)
                {
                    // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                    var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                    var idLogin = userClaims.FindFirst("IDUseName");
                    var tenusername = userClaims.FindFirst("UseName");
                    if (usernameClaim != null)
                    {
                        string username1 = tenusername.Value;
                        string username = usernameClaim.Value;
                        maKH = idLogin.Value;
                        TempData["Username"] = username1;
                        TempData["LoginData"] = username;
                        // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                    }

                    // You can also access your custom claim ("OtherProperties" in this case) in a similar manner.
                    var gioHang = db.TblGioHangs.SingleOrDefault(x => x.IdkhachHang == maKH);
                    var item = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == MaSp1);


                    var chitietGioHang = db.TblChiTietGioHang1s.Where(x => x.IddonHang == gioHang.IdgioHang).ToList();

                    string maCanTim = chitietGioHang.SingleOrDefault(x => x.IdchiTietSanPham == MaSp1) + "";


                    if (maCanTim == "")
                    {
                        var addItem = new TblChiTietGioHang1();

                        var idChiTIetGIoHang = db.TblChiTietGioHang1s.Max(x => x.IdchiTietDonHang);
                        addItem.IdchiTietDonHang = idChiTIetGIoHang + 1;
                        addItem.IddonHang = gioHang.IdgioHang;
                        addItem.IdchiTietSanPham = MaSp1;
                        addItem.SoLuong = SoLuong;
                        addItem.ThanhTien = (int)item.GiaLonNhat;
                        addItem.TenSp = item.TenSp;
                        addItem.Check1 = true;
                        addItem.HinhAnh = item.AnhDaiDien;
                        db.TblChiTietGioHang1s.Add(addItem);
                        db.SaveChanges();
                        return View("ProductDetail", homeProductDetailViewModel);

                    }
                    else
                    {
                        var themsoluong = chitietGioHang.SingleOrDefault(x => x.IdchiTietSanPham == MaSp1);
                        themsoluong.SoLuong += SoLuong;
                        db.TblChiTietGioHang1s.Update(themsoluong);
                        db.SaveChanges();
                        return View("ProductDetail", homeProductDetailViewModel);
                    }

                }
                return View("ProductDetail", homeProductDetailViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }


        }

        public IActionResult AddToCard(String id)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            string maKH = "";
            if (userClaims.IsAuthenticated)
            {
                if (userClaims != null)
                {
                    // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                    var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                    var idLogin = userClaims.FindFirst("IDUseName");
                    var tenusername = userClaims.FindFirst("UseName");
                    if (usernameClaim != null)
                    {
                        string username1 = tenusername.Value;
                        string username = usernameClaim.Value;
                        maKH = idLogin.Value;
                        TempData["Username"] = username1;
                        TempData["LoginData"] = username;
                        // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                    }

                    // You can also access your custom claim ("OtherProperties" in this case) in a similar manner.
                    var gioHang = db.TblGioHangs.SingleOrDefault(x => x.IdkhachHang == maKH);
                    var item = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == id);
                    var chitietsanpham = db.TChiTietSanPhams.FirstOrDefault(x => x.MaSp == item.MaSp);

                    var chitietGioHang = db.TblChiTietGioHang1s.Where(x => x.IddonHang == gioHang.IdgioHang).ToList();

                    string maCanTim = chitietGioHang.SingleOrDefault(x => x.IdchiTietSanPham == chitietsanpham.MaChiTietSp) + "";


                    if (maCanTim == "")
                    {
                        var addItem = new TblChiTietGioHang1();

                        var idChiTIetGIoHang = db.TblChiTietGioHang1s.Max(x => x.IdchiTietDonHang);
                        addItem.IdchiTietDonHang = idChiTIetGIoHang + 1;
                        addItem.IddonHang = gioHang.IdgioHang;
                        addItem.IdchiTietSanPham = chitietsanpham.MaChiTietSp;
                        addItem.SoLuong = 1;
                        addItem.ThanhTien = (int)item.GiaLonNhat;
                        addItem.TenSp = item.TenSp;
                        addItem.Check1 = true;
                        addItem.HinhAnh = item.AnhDaiDien;
                        db.TblChiTietGioHang1s.Add(addItem);
                        db.SaveChanges();
                        TempData["success"] = "Sản phẩm đã được thêm vào giỏ hàng!";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        var themsoluong = chitietGioHang.SingleOrDefault(x => x.IdchiTietSanPham == chitietsanpham.MaChiTietSp);
                        themsoluong.SoLuong += 1;
                        db.TblChiTietGioHang1s.Update(themsoluong);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
        }

        public IActionResult AddYeuThich(String id)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            string maKH = "";
            if (userClaims.IsAuthenticated)
            {
                if (userClaims != null)
                {
                    // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                    var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                    var idLogin = userClaims.FindFirst("IDUseName");
                    var tenusername = userClaims.FindFirst("UseName");
                    if (usernameClaim != null)
                    {
                        string username1 = tenusername.Value;
                        string username = usernameClaim.Value;
                        maKH = idLogin.Value;
                        TempData["Username"] = username1;
                        TempData["LoginData"] = username;
                        // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                    }

                    // You can also access your custom claim ("OtherProperties" in this case) in a similar manner.
                    var yeuThich = db.TblYeuThiches.SingleOrDefault(x => x.IdkhachHang == maKH);
                    var item = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == id);
                    var chitietsanpham = db.TChiTietSanPhams.FirstOrDefault(x => x.MaSp == item.MaSp);

                    var chitietYeuThich = db.TblCtyeuThiches.Where(x => x.IdyeuThich == yeuThich.IdyeuThich).ToList();

                    string maCanTim1 = chitietYeuThich.SingleOrDefault(x => x.IdchiTietSanPham == chitietsanpham.MaChiTietSp) + "";


                    if (maCanTim1 == "")
                    {
                        var addItem = new TblCtyeuThich();

                        var idChiTietYeuThich = db.TblCtyeuThiches.Max(x => x.IdctyeuThich);
                        addItem.IdctyeuThich = idChiTietYeuThich + 1;
                        addItem.IdyeuThich = yeuThich.IdyeuThich;
                        addItem.IdchiTietSanPham = chitietsanpham.MaChiTietSp;
                        addItem.DonGiaBan = (int)item.GiaLonNhat;
                        addItem.TenSp = item.TenSp;
                        addItem.HinhAnh = item.AnhDaiDien;
                        db.TblCtyeuThiches.Add(addItem);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}