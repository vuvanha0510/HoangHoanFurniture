using System;
using System.Collections.Generic;

namespace BTLWEBNC_WEBNOITHAT.Models;

public partial class TblGioHang
{
    public int IdgioHang { get; set; }

    public string? IdkhachHang { get; set; }

    public string? TenGioHang { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? ModyfiAt { get; set; }

    public virtual TKhachHang? IdkhachHangNavigation { get; set; }

    public virtual ICollection<TblChiTietGioHang1> TblChiTietGioHang1s { get; set; } = new List<TblChiTietGioHang1>();
}
