using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ChiChip613DbContext : DbContext
    {
        public ChiChip613DbContext(DbContextOptions<ChiChip613DbContext> options) : base(options)
        {

        }

        public DbSet<NhapHang> NhapHangs { get; set; }
        public DbSet<ChiPhi> ChiPhis { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<SPDaChon> SPDaChons { get; set; }

    }
}
