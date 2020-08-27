using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Data.Repository
{
    public interface IChiTietDonHangRepository : IRepository<ChiTietDonHang>
    {
        void CreateRange(List<ChiTietDonHang> chiTietDonHangs);
    }
    public class ChiTietDonHangRepository : Repository<ChiTietDonHang>, IChiTietDonHangRepository
    {
        public ChiTietDonHangRepository(ChiChip613DbContext context) : base(context)
        {
        }

        public void CreateRange(List<ChiTietDonHang> chiTietDonHangs)
        {
            _context.ChiTietDonHangs.AddRange(chiTietDonHangs);
            _context.SaveChanges();
        }
    }
}
