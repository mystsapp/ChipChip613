using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ISPDaChonRepository : IRepository<SPDaChon>
    {

    }
    public class SPDaChonRepository : Repository<SPDaChon>, ISPDaChonRepository
    {
        public SPDaChonRepository(ChiChip613DbContext context) : base(context)
        {
        }
    }
}
