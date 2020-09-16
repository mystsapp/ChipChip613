using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Dtos
{
    public class NhapHangDto
    {
        public long Id { get; set; }

        [DisplayName("Tên hàng")]
        [Required(ErrorMessage = "Tên được để trống.")]
        public string TenHang { get; set; }

        [DisplayName("Nơi nhập")]
        public string NoiNhap { get; set; }

        [DisplayName("Điện thoại")]
        public string DTNoiNhap { get; set; }

        [DisplayName("Đơn vị")]
        public string DVT { get; set; }

        [DisplayName("Đơn vị 2")]
        public string DVT2 { get; set; }

        [DisplayName("Dơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Số lượng")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal SoLuong { get; set; }

        [DisplayName("Số lượng 2")]
        public decimal SoLuong2 { get; set; }

        public DateTime NgayTao { get; set; }

        public string NguoiTao { get; set; }

        [DisplayName("Ngày nhập")]
        public DateTime NgayNhap { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        [DisplayName("Thành tiền Lưu")]
        public decimal ThanhTienLuu { get; set; }

        [DisplayName("Số lượng lưu")]
        public decimal SoLuongLuu { get; set; }

        [DisplayName("Đơn vị lưu")]
        public string DVTLuu { get; set; }

        [DisplayName("Số lượng lưu")]
        public decimal SoLuong2Luu { get; set; }

        [DisplayName("Đơn vị lưu")]
        public string DVT2Luu { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        public string LogFile { get; set; }

        public decimal TongTien { get; set; }
        public decimal TongTienTheoNgay { get; set; }
    }
}
