using System;
using System.Collections.Generic;

namespace BTLWEBNC_WEBNOITHAT.Models;

public partial class TblCtyeuThich
{
    public int IdctyeuThich { get; set; }

    public int? IdyeuThich { get; set; }

    public string? IdchiTietSanPham { get; set; }

    public int? DonGiaBan { get; set; }

    public string? TenSp { get; set; }

    public string? HinhAnh { get; set; }

    public virtual TChiTietSanPham? IdchiTietSanPhamNavigation { get; set; }

    public virtual TblYeuThich? IdyeuThichNavigation { get; set; }
}
