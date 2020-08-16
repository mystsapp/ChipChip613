using Data.Dtos;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ChipChip613.Models
{
    public class DonHangViewModel
    {
        public IPagedList<DonHangDto> DonHangDtos { get; set; }
        public IEnumerable<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DonHang DonHang { get; set; }
        public ChiTietDonHang ChiTietDonHang { get; set; }
        public string StrUrl { get; set; }
    }
}
