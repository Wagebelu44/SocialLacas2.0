$(document).ready(function () {

    $(".nav").removeClass("active");
    $(".addbalance").addClass('active');
    $("#divLoading").removeClass("show");

   

});


var saveFunds = function () {
    if ($("#field-paypal_email").val() == "") {
        $(".valalert").removeClass("hidden");
        $(".valalert").text("Please provide paypal email");
        return false;
    }
    else if ($("#amount").val() == "") {
        $(".valalert").removeClass("hidden");
        $(".valalert").text("Please enter amount");
        return false;
    }
    else {
        obj = {};
        obj.cost = $("#amount").val();
        var paypalurl = '/Service/BalancePayPal';
        $("#divLoading").addClass("show");

        $.ajax({
            type: "POST",
            url: paypalurl,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                // addfuncds();
                var url = data;
                $("#divLoading").removeClass("show");

                window.location.href = url;

            },
            error: function (err) {
                $("#divLoading").removeClass("show");

                alert("Error in processing payment.")
            }
        });
    }
}

var checkBalance = function () {
    $("#divLoading").addClass("show");
    $.ajax({
        type: "POST",
        url: "/Service/APIShowBalance",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null && data.indexOf(',') != -1) {
                $("#divLoading").removeClass("show");
                var array = data.split(",");
                var arrstatus = array[0].split(":");
                 var status = arrstatus[1].substr(1, arrstatus[1].length - 1)
                status = status.replace(/"/g, "");
               // alert(status);
               var  numstatus = parseFloat(status);
                var inusd = (numstatus / 68.0).toFixed(2);
                $(".valalert").removeClass("hidden");
                $(".valalert").text("Your current balance is : " + inusd);
            }
            else {
                $("#divLoading").removeClass("show");
                alert(data);
            }

        },
        error: function (err) { }
    });

   
}