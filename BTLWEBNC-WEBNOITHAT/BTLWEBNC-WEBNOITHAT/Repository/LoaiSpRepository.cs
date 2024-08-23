using BTLWEBNC_WEBNOITHAT.Models;
using BTLWEBNC_WEBNOITHAT.Respository;

namespace BTLWEBNC_WEBNOITHAT.Repository
{
    public class LoaiSpRepository:ILoaiSpRepository
    {
        private readonly QlbanNoiThatContext _context;

        public LoaiSpRepository(QlbanNoiThatContext context)
        {
            _context = context;
        }

        public TLoaiSp Add(TLoaiSp loaiSP)
        {
            _context.TLoaiSps.Add(loaiSP);
            _context.SaveChanges();
            return loaiSP;
        }

        public TLoaiSp Delete(string maloaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp GetLoaiSp(string maloaiSp)
        {
            return _context.TLoaiSps.Find(maloaiSp);
        }

        public TLoaiSp Update(TLoaiSp loaiSP)
        {
            _context.Update(loaiSP);
            _context.SaveChanges();
            return loaiSP;
        }
    }
}
