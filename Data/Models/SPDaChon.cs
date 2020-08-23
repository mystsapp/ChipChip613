using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SPDaChon
    {
        public int Id { get; set; }

        [DisplayName("Tên SP")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string TenSanPham { get; set; }

        [DisplayName("Đơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        public long DonHangId { get; set; }
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
    }
}