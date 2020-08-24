function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var editController = {
    init: function () {
        editController.registerEven();
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

        //$('input.numbers').val(function (index, value) {
        //    return addCommas(value);
        //});
        $('.txtDonGia').off('blur').on('blur', function () {
            editController.blurFunction();
        });
        
        $('.txtSoLuong').off('blur').on('blur', function () {
            editController.blurFunction();
        });
        
        $('.txtChiPhiKhac').off('blur').on('blur', function () {
            editController.blurFunction();
        });

    },
    blurFunction: function () {
        var donGia = $('.txtDonGia').val();
        var soLuong = $('.txtSoLuong').val();
        soLuong = soLuong.replace(',', '');
        var chiPhiKhac = $('.txtChiPhiKhac').val();
        $.ajax({
            url: '/NhapHangs/GetThanhTien',
            type: 'GET',
            data: {
                donGia: donGia,
                soLuong: soLuong,
                chiPhiKhac: chiPhiKhac,
            },
            dataType: 'json',
            success: function (response) {
                //var data = JSON.parse(response.data);
                //console.log(data);
                console.log(response.thanhTien);
                var thanhTien = numeral(response.thanhTien).format('0,0');
                $('.txtThanhTien').val(thanhTien);
            }
        });
        
    }
};
editController.init();