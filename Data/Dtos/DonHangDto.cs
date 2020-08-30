using System;
using System.ComponentModel;

namespace Data.Dtos
{
    public class DonHangDto
    {
        public long Id { get; set; }

        [DisplayName("Tên KH")]
        public string KhachHang { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }

        [DisplayName("SL Tổng")]
        public decimal SoLuong { get; set; }
        
        [DisplayName("SL Xúc xích")]
        public decimal SoLuongXX { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }
    }
}