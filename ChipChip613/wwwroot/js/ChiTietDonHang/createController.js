function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var createController = {
    init: function () {
        createController.sanPhamChangeFunction();
        createController.registerEven();
    },
    registerEven: function () {

        // format .numbers
        $('input.numbers').keyup(function (event) {

            // Chỉ cho nhập số
            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return addCommas(value);
            });
        });

        $('#txtSoLuong').off('blur').on('blur', function () {
            soLuong = $('#txtSoLuong').val();
            //donGia = $('#txtDonGia').val(); // ko nhan 10,000 sau khi format
            sanPham = $('#ddlSanPham').val();
            if (sanPham === 'Truyền thống') {
                var thanhTien = numeral(soLuong * 10000).format('0,0');
                $('#txtThanhTien').val(thanhTien);
            }
            else if (sanPham === 'Xúc xích') {
                var thanhTien1 = numeral(soLuong * 15000).format('0,0');
                $('#txtThanhTien').val(thanhTien1);
            }

        });
        
        $('#ddlSanPham').off('change').on('change', function () {
            
            createController.sanPhamChangeFunction();
        });

    },
    sanPhamChangeFunction: function () {
        sanPham = $('#ddlSanPham').val();
        if (sanPham === 'Truyền thống') {
            $('#txtDonGia').val(numeral(10000).format('0,0'));
            var thanhTien = numeral($('#txtSoLuong').val() * 10000).format('0,0');
            $('#txtThanhTien').val(thanhTien);
        }
        else if (sanPham === 'Xúc xích') {
            $('#txtDonGia').val(numeral(15000).format('0,0'))
            var thanhTien1 = numeral($('#txtSoLuong').val() * 15000).format('0,0');
            $('#txtThanhTien').val(thanhTien1);
        }
    }
    //blurFunction: function () {
    //    var donGia = $('.txtDonGia').val();
    //    var soLuong = $('.txtSoLuong').val();
    //    var chiPhiKhac = $('.txtChiPhiKhac').val();
    //    $.ajax({
    //        url: '/NhapHangs/GetThanhTien',
    //        type: 'GET',
    //        data: {
    //            donGia: donGia,
    //            soLuong: soLuong,
    //            chiPhiKhac: chiPhiKhac,
    //        },
    //        dataType: 'json',
    //        success: function (response) {
    //            //var data = JSON.parse(response.data);
    //            //console.log(data);
    //            console.log(response.thanhTien);
    //            var thanhTien = numeral(response.thanhTien).format('0,0');
    //            $('.txtThanhTien').val(thanhTien);
    //        }
    //    });
        
    //}
};
createController.init();