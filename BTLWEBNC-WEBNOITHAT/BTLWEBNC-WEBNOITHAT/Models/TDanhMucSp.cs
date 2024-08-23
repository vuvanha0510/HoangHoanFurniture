using System;
using System.Collections.Generic;

namespace BTLWEBNC_WEBNOITHAT.Models;

public partial class TDanhMucSp
{
    public string MaSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public double? ThoiGianBaoHanh { get; set; }

    public string? GioiThieuSp { get; set; }

    public string? MaLoai { get; set; }

    public string? AnhDaiDien { get; set; }

    public decimal? GiaNhoNhat { get; set; }

    public int? GiaLonNhat { get; set; }

    public virtual TLoaiSp? MaLoaiNavigation { get; set; }

    public virtual ICollection<TAnhSp> TAnhSps { get; set; } = new List<TAnhSp>();

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}
