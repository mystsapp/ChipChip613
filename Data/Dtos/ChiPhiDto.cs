using System;
using System.ComponentModel;

namespace Data.Dtos
{
    public class ChiPhiDto
    {
        public long Id { get; set; }

        [DisplayName("Hàng nhập")]
        public string TenHangNhap { get; set; }

        [DisplayName("Chi phí khác")]
        public string ChiPhiKhac { get; set; }

        [DisplayName("DVT")]
        public string DVT { get; set; }

        [DisplayName("DVT 2")]
        public string DVT2 { get; set; }

        [DisplayName("Số lượng")]
        public decimal SoLuong { get; set; }

        [DisplayName("Số lượng 2")]
        public decimal SoLuong2 { get; set; }

        [DisplayName("Dơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        [DisplayName("Ngày nhập")]
        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
    }
}