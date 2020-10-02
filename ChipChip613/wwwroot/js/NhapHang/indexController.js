function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var indexController = {
    init: function () {
        indexController.registerEven();
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

        $('.cursor-pointer').click(function () {
            if ($(this).hasClass("bg-warning"))
                $(this).removeClass("bg-warning");
            else {
                $('tr.bg-warning').removeClass("bg-warning");
                $(this).addClass("bg-warning");
            }

        });



    }
};
indexController.init();