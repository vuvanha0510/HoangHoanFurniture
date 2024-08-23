using System;
using System.Collections.Generic;

namespace BTLWEBNC_WEBNOITHAT.Models;

public partial class TblChiTietGioHang1
{
    public int IdchiTietDonHang { get; set; }

    public int? IddonHang { get; set; }

    public string? IdchiTietSanPham { get; set; }

    public int? SoLuong { get; set; }

    public int? ThanhTien { get; set; }

    public double? TongTien => SoLuong * ThanhTien;

    public string? TenSp { get; set; }

    public bool? Check1 { get; set; }

    public string? HinhAnh { get; set; }

    public virtual TChiTietSanPham? IdchiTietSanPhamNavigation { get; set; }

    public virtual TblGioHang? IddonHangNavigation { get; set; }
}
