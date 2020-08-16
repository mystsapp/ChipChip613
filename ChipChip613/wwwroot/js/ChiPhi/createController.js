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

        $('.txtSoLuong').off('blur').on('blur', function () {
            $('#btnSubmit').prop('disabled', false);
            createController.txtSoLuongBlurFunction();
        });

        $('.txtSoLuong2').off('blur').on('blur', function () {
            $('#btnSubmit').prop('disabled', false);
            createController.txtSoLuong2BlurFunction();
        });
        
        $('.txtThanhTien').off('blur').on('blur', function () {
            $('#btnSubmit').prop('disabled', false);
        });

    },
    txtSoLuong2BlurFunction: function () { // kt so luong hang va sl2 neu co
        var soLuong2 = $('.txtSoLuong2').val();
        hangNhapId = $('.ddlHangNhap').val();

        $.ajax({
            url: '/ChiPhis/KiemTraSL2',
            type: 'GET',
            data: {
                soLuong2: soLuong2,
                hangNhapId: hangNhapId
            },
            dataType: 'json',
            success: function (response) {
                if (!response.status) {
                    bootbox.alert("Số lượng không đủ!");
                    return;
                }
                else {
                    if (response.sLuong !== 0) {
                        $('.txtSoLuong').val(response.sLuong);
                        var thanhTien = numeral(response.thanhTien).format('0,0');
                        $('.txtThanhTien').val(thanhTien);
                    }
                }
                //console.log(response.thanhTien);
                //var thanhTien = numeral(response.thanhTien).format('0,0');
                //$('.txtThanhTien').val(thanhTien);
            }
        });

    },
    txtSoLuongBlurFunction: function () { // kt so luong hang va sl2 neu co
        var soLuong = $('.txtSoLuong').val();
        hangNhapId = $('.ddlHangNhap').val();

        $.ajax({
            url: '/ChiPhis/KiemTraSL',
            type: 'GET',
            data: {
                soLuong: soLuong,
                hangNhapId: hangNhapId
            },
            dataType: 'json',
            success: function (response) {
                if (!response.status) {
                    bootbox.alert("Số lượng không đủ!");
                    return;
                }
                else {
                    var thanhTien = numeral(response.thanhTien).format('0,0');
                    $('.txtThanhTien').val(thanhTien);
                    if (response.sLuong2 !== 0) {
                        $('.txtSoLuong2').val(response.sLuong2);
                        
                    }
                }
                //console.log(response.thanhTien);
                //var thanhTien = numeral(response.thanhTien).format('0,0');
                //$('.txtThanhTien').val(thanhTien);
            }
        });

    }

};
createController.init();