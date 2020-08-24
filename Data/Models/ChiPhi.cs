using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class ChiPhi
    {
        public long Id { get; set; }

        [DisplayName("Phí xuất")]
        public long NhapHangId { get; set; }

        [DisplayName("Chi phí khác")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string ChiPhiKhac { get; set; }

        [DisplayName("DVT")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string DVT { get; set; }

        [DisplayName("DVT 2")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string DVT2 { get; set; }

        [DisplayName("Số lượng")]
        public decimal SoLuong { get; set; }
        
        [DisplayName("Số lượng 2")]
        public decimal SoLuong2 { get; set; }

        [DisplayName("Dơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string GhiChu { get; set; }
    }
}