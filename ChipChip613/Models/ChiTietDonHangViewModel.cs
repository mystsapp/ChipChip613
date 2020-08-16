using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChipChip613.Models
{
    public class ChiTietDonHangViewModel
    {
        public ChiTietDonHang ChiTietDonHang { get; set; }
        public IEnumerable<SPDaChon> SPDaChons { get; set; }
        public IEnumerable<SanPhamViewModel> SanPhams { get; set; }
        public DonHang DonHang { get; set; }
        public SPDaChon SPDaChon { get; set; }
        public string StrUrl { get; set; }
    }
}
