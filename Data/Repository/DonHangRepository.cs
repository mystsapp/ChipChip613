using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IDonHangRepository : IRepository<DonHang>
    {

    }
    public class DonHangRepository : Repository<DonHang>, IDonHangRepository
    {
        public DonHangRepository(ChiChip613DbContext context) : base(context)
        {
        }
    }
}
