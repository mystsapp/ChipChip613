using Data.Dtos;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ChipChip613.Models
{
    public class NhapHangViewModel
    {
        public IPagedList<NhapHangDto> NhapHangs { get; set; }
        public IEnumerable<ListViewModel> TinhTrangs { get; set; }
        public NhapHang NhapHang { get; set; }
        public string StrUrl { get; set; }
        public decimal TongTien { get; set; }
    }
}
