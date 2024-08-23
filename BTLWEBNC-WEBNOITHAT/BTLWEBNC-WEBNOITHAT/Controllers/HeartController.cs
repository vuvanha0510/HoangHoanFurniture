using BTLWEBNC_WEBNOITHAT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTLWEBNC_WEBNOITHAT.Controllers
{
    public class HeartController : Controller
    {
        QlbanNoiThatContext db = new QlbanNoiThatContext();
        private readonly QlbanNoiThatContext _context;
        private readonly ILogger<HeartController> _logger;
        public HeartController(ILogger<HeartController> logger)
        {
            _logger = logger;
        }

        public static List<TblCtyeuThich> chitietyeuthich = new List<TblCtyeuThich>();

        public IActionResult YeuThich()
        {
            var userClaims = User.Identity as ClaimsIdentity;
            int idLogin = 0;
            if (userClaims.IsAuthenticated)
            {
                // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                var idClaim = userClaims.FindFirst("idYeuThich");
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

                var sanPham = db.TblCtyeuThiches.Where(x => x.IdyeuThich == idLogin).ToList();
                string maSP;

                for (int i = 0; i < sanPham.Count(); i++)
                {
                    maSP = db.TChiTietSanPhams.SingleOrDefault(x => x.MaChiTietSp == sanPham[i].IdchiTietSanPham).MaSp;


                    if (sanPham[i].TenSp == null)
                    {
                        sanPham[i].TenSp = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).TenSp;

                    }
                    if (sanPham[i].HinhAnh == null)
                    {
                        sanPham[i].HinhAnh = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).AnhDaiDien;

                    }
                    if (sanPham[i].DonGiaBan == null)
                    {
                        sanPham[i].DonGiaBan = (int)db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSP).GiaLonNhat;

                    }
                    db.SaveChanges();
                }
                return View(sanPham);
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var yeuThich = db.TblCtyeuThiches.SingleOrDefault(x => x.IdctyeuThich == id);
            if (yeuThich != null)
            {
                db.TblCtyeuThiches.Remove(yeuThich);
                db.SaveChangesAsync();


            }
            return RedirectToAction("YeuThich");
        }
    }
}
