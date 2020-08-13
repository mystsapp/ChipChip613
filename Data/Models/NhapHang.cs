using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class NhapHang
    {
        public long Id { get; set; }

        [DisplayName("Tên hàng")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Tên được để trống.")]
        public string TenHang { get; set; }

        [DisplayName("Nơi nhập")]
        [MaxLength(200), Column(TypeName = "nvarchar(200)")]
        public string NoiNhap { get; set; }

        [DisplayName("Điện thoại")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string DTNoiNhap { get; set; }

        [DisplayName("Đơn vị")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string DVT { get; set; }

        [DisplayName("Đơn vị 2")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string DVT2 { get; set; }

        [DisplayName("Dơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Số lượng")]
        public decimal SoLuong { get; set; }

        [DisplayName("Số lượng 2")]
        public decimal SoLuong2 { get; set; }

        public DateTime NgayTao { get; set; }

        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        [DisplayName("Ngày nhập")]
        public DateTime NgayNhap { get; set; }

        [DisplayName("Chi phí khác")]
        public decimal? ChiPhiKhac { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        [DisplayName("Thành tiền Lưu")]
        public decimal ThanhTienLuu { get; set; }

        [DisplayName("Số lượng lưu")]
        public decimal SoLuongLuu { get; set; }

        [DisplayName("Đơn vị lưu")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string DVTLuu { get; set; }

        [DisplayName("Số lượng lưu")]
        public decimal SoLuong2Luu { get; set; }

        [DisplayName("Đơn vị lưu")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string DVT2Luu { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string GhiChu { get; set; }
    }
}