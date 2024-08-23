using Microsoft.AspNetCore.Mvc;
using BTLWEBNC_WEBNOITHAT.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BTLWEBNC_WEBNOITHAT.Controllers
{
    public class AccessController : Controller
    {
        QlbanNoiThatContext db = new QlbanNoiThatContext();
        private readonly QlbanNoiThatContext _context;
        private readonly ILogger<CartController> _logger;
        public IActionResult Login()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(TUser userData)
        {
            if (userData.Username == null || userData.Password == null)
            {
                return View(userData);
            }
            else
            {
                var checkUsername = db.TUsers.SingleOrDefault(x => x.Username == userData.Username && x.Password == userData.Password);

                if (!PasswordMeetsRequirements(userData.Password))
                {
                    ModelState.AddModelError("Password", "Mật khẩu cần chứa ít nhất một số và một chữ ");
                    return View(userData);
                }
                else if (userData.Password.Length < 7)
                {
                    ModelState.AddModelError("Password", "Mật khẩu phải lớn hơn 6 ký tự ");
                    return View(userData);
                }
                else if (userData.Username.Length < 4)
                {
                    ModelState.AddModelError("Username", "Tài khoản phải lớn hơn 4 ký tự ");
                    return View(userData);
                }

                else if (checkUsername == null)
                {
                    ModelState.AddModelError("Username", "Tài khoản không tồn tại hoặc sai mật khẩu ");
                    return View(userData);
                }
                else
                {

                    if (userData.Username == "admin")
                    {
                        return RedirectToAction("danhmucsanpham", "Admin");
                    }
                    else
                    {
                        var getKhachHang1 = db.TKhachHangs.SingleOrDefault(x => x.Username == checkUsername.Username);
                        if (getKhachHang1 == null)
                        {
                            DateTime ngayBatDau = new DateTime(2022, 1, 1);
                            DateTime ngayHienTai = DateTime.Now;

                            TimeSpan thoiGian = ngayHienTai - ngayBatDau;

                            double tongGiay = thoiGian.TotalSeconds;
                            var khachHang = new TKhachHang();
                            khachHang.MaKhanhHang = tongGiay.ToString();
                            khachHang.Username = checkUsername.Username;
                            db.TKhachHangs.Add(khachHang);
                            db.SaveChanges();
                        }
                        var getKhachHang = db.TKhachHangs.SingleOrDefault(x => x.Username == checkUsername.Username);
                        var checkDonHang = db.TblGioHangs.Any(x => x.IdkhachHang == getKhachHang.MaKhanhHang);
                        if (!checkDonHang)
                        {
                            var donHang = new TblGioHang();
                            var idMaxDonHang = db.TblGioHangs.Max(x => x.IdgioHang);
                            donHang.IdgioHang = idMaxDonHang + 1;
                            donHang.IdkhachHang = getKhachHang.MaKhanhHang;
                            donHang.TenGioHang = "Gio hang cua" + getKhachHang.Username;
                            donHang.CreateAt = DateTime.Now;
                            db.TblGioHangs.Add(donHang);

                            db.SaveChanges();
                        }
                        var checkYeuThich = db.TblYeuThiches.Any(x => x.IdkhachHang == getKhachHang.MaKhanhHang);
                        if (!checkYeuThich)
                        {
                            var yeuThich = new TblYeuThich();
                            var idMaxYeuThich = db.TblYeuThiches.Max(x => x.IdyeuThich);
                            yeuThich.IdyeuThich = idMaxYeuThich + 1;
                            yeuThich.IdkhachHang = getKhachHang.MaKhanhHang;
                            db.TblYeuThiches.Add(yeuThich);

                            db.SaveChanges();
                        }
                        var getDonHang = db.TblGioHangs.SingleOrDefault(x => x.IdkhachHang == getKhachHang.MaKhanhHang);
                        var getYeuThich = db.TblYeuThiches.SingleOrDefault(x => x.IdkhachHang == getKhachHang.MaKhanhHang);

                        List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, checkUsername.Username),
                          new Claim("IDUseName",getKhachHang.MaKhanhHang),
                          new Claim("UseName",getKhachHang.Username),
                           new Claim("idGioHang",getDonHang.IdgioHang.ToString()),
                           new Claim("idYeuThich",getYeuThich.IdyeuThich.ToString())

                    };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            AllowRefresh = true,
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                        return RedirectToAction("Index", "Home");

                    }

                }


            }

        }

       

        public async Task<IActionResult> Register()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(TUser userData, string nlmatkhau)
        {
            if (userData.Username == null || userData.Password == null)
            {
                return View(userData);
            }
            else
            {
                var checkUsername = db.TUsers.SingleOrDefault(x => x.Username == userData.Username);

                if (!PasswordMeetsRequirements(userData.Password))
                {
                    ModelState.AddModelError("Password", "Mật khẩu cần chứa ít nhất một số và một chữ ");
                    return View(userData);
                }
                else if (userData.Password.Length < 7)
                {
                    ModelState.AddModelError("Password", "Mật khẩu phải dài hơn 6 ký tự ");
                    return View(userData);
                }
                else if (userData.Username.Length < 5)
                {
                    ModelState.AddModelError("Username", "Tài khoản phải lớn hơn 4 ký tự ");
                    return View(userData);
                }

                else if (checkUsername != null)
                {
                    ModelState.AddModelError("Username", "Tài khoản đã tồn tại ");
                    return View(userData);
                }
                else if(userData.Password != nlmatkhau)
                {
                    TempData["Thông báo"] = "Mật khẩu không khớp!";
                    return View(userData);
                }
                else
                {
                    db.TUsers.Add(userData);

                    DateTime ngayBatDau = new DateTime(2022, 1, 1);
                    DateTime ngayHienTai = DateTime.Now;

                    TimeSpan thoiGian = ngayHienTai - ngayBatDau;

                    double tongGiay = thoiGian.TotalSeconds;

                    var khachHang = new TKhachHang();
                    khachHang.MaKhanhHang = tongGiay.ToString();
                    khachHang.Username = userData.Username;
                    var donHang = new TblGioHang();
                    var idMaxDonHang = db.TblGioHangs.Max(x => x.IdgioHang);
                    donHang.IdgioHang = idMaxDonHang + 1;
                    donHang.IdkhachHang = khachHang.MaKhanhHang;
                    donHang.TenGioHang = "Gio hang cua" + khachHang.Username;
                    donHang.CreateAt = DateTime.Now;
                    db.TblGioHangs.Add(donHang);
                    db.TKhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Login");



                }
            }

        }
        // GET: DangKy/Details/5
        private bool PasswordMeetsRequirements(string password)
        {
            // Use a regular expression to check if the password contains at least one letter and one number
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$");
        }

        public async Task<IActionResult> NhapThongTin()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NhapThongTin(TKhachHang khachHang)
        {
            return RedirectToAction("Index", "Home");
        }

        //QlbanNoiThatContext db = new QlbanNoiThatContext();
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    if (HttpContext.Session.GetString("UserName") == null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        //[HttpPost]
        //public IActionResult Login(TUser user)
        //{
        //    if (HttpContext.Session.GetString("UserName") == null)
        //    {
        //        var u = db.TUsers.Where(x => x.Username.Equals(user.Username) &&
        //        x.Password.Equals(user.Password)).FirstOrDefault();
        //        if (u != null)
        //        {
        //            HttpContext.Session.SetString("UserName", u.Username.ToString());
        //            if (user.Username == "a")
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                return RedirectToAction("danhmucsanpham", "admin");
        //            }
        //        }
        //    }
        //    return View();
        //}
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    HttpContext.Session.Remove("UserName");
        //    return RedirectToAction("Login", "Access");
        //}
    }
}
