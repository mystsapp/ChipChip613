using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SPDaChon
    {
        public int Id { get; set; }

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
    }
}