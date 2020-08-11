using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IChiTietDonHangRepository chiTietDonHangRepository { get; }
        IDonHangRepository donHangRepository { get; }
        INhapHangRepository nhapHangRepository { get; }
        IChiPhiRepository chiPhiRepository { get; }
        Task<int> Complete();
    }
    public class UnitOfwork : IUnitOfWork
    {
        private readonly ChiChip613DbContext _context;

        public UnitOfwork(ChiChip613DbContext context)
        {
            _context = context;

            chiTietDonHangRepository = new ChiTietDonHangRepository(_context);
            donHangRepository = new DonHangRepository(_context);
            nhapHangRepository = new NhapHangRepository(_context);
            chiPhiRepository = new ChiPhiRepository(_context);

        }

        public IChiTietDonHangRepository chiTietDonHangRepository { get; }

        public IDonHangRepository donHangRepository { get; }

        public INhapHangRepository nhapHangRepository { get; }

        public IChiPhiRepository chiPhiRepository { get; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
            
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.Collect();
        }
    }
}
