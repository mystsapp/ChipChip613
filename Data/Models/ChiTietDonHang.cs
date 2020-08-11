using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class ChiTietDonHang
    {
        public long Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Tên được để trống.")]
        public string TenSanPham { get; set; }

        [DisplayName("Đơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        [DisplayName("Đơn hàng")]
        public long DonHangId { get; set; }

        [ForeignKey("DonHangId")]
        public virtual DonHang DonHang { get; set; }
    }
}
