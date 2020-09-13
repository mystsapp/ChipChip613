using System;
using System.ComponentModel;

namespace Data.Dtos
{
    public class DonHangDto
    {
        [DisplayName("ID")]
        public long Id { get; set; }

        [DisplayName("Tên KH")]
        public string KhachHang { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }

        [DisplayName("SL Tổng")]
        public decimal SoLuong { get; set; }
        
        [DisplayName("SL Xúc xích")]
        public decimal SoLuongXX { get; set; }
        
        [DisplayName("SL truyền thống")]
        public decimal SoLuongTT { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }
        public decimal TongSL { get; set; }
        public decimal TongSLTT { get; set; }
        public decimal TongSLXX { get; set; }
        public decimal TongTien { get; set; }
    }
}