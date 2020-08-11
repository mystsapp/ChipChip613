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
        public IPagedList<NhapHang> NhapHangs { get; set; }
        public NhapHang NhapHang { get; set; }
        public string StrUrl { get; set; }
    }
}
