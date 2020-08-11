using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class DonHang
    {
        public long Id { get; set; }

        [DisplayName("Tên KH")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string KhachHang { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(200), Column(TypeName = "nvarchar(200)")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string DienThoai { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }
    }
}
