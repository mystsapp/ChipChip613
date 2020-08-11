function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var createController = {
    init: function () {
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

        //$('input.numbers').val(function (index, value) {
        //    return addCommas(value);
        //});
        $('.txtDonGia').off('blur').on('blur', function () {
            createController.blurFunction();
        });
        
        $('.txtSoLuong').off('blur').on('blur', function () {
            createController.blurFunction();
        });
        
        $('.txtChiPhiKhac').off('blur').on('blur', function () {
            createController.blurFunction();
        });

    },
    blurFunction: function () {
        var donGia = $('.txtDonGia').val();
        var soLuong = $('.txtSoLuong').val();
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
createController.init();