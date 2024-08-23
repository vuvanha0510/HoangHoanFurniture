using BTLWEBNC_WEBNOITHAT.Models;
namespace BTLWEBNC_WEBNOITHAT.Respository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp Add(TLoaiSp loaiSP);

        TLoaiSp Update(TLoaiSp loaiSP);

        TLoaiSp Delete(String maloaiSp);

        TLoaiSp GetLoaiSp(String maloaiSp);

        IEnumerable<TLoaiSp> GetAllLoaiSp();
    }
}
