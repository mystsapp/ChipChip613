using Data.Dtos;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ChipChip613.Models
{
    public class ChiPhiViewModel
    {
        public IPagedList<ChiPhiDto> ChiPhiDtos { get; set; }
        public IEnumerable<ListViewModel> ListNhapHang { get; set; }
        public IEnumerable<NhapHang> NhapHangs { get; set; }
        public ChiPhi ChiPhi { get; set; }
        public string StrUrl { get; set; }
        public decimal SoLuongCu { get; set; }
        public decimal SoLuong2Cu { get; set; }
    }
}
