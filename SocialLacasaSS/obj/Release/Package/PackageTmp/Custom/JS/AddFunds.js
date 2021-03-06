﻿$(document).ready(function () {

    $(".nav").removeClass("active");
    $(".addfunds").addClass('active');
    $("#divLoading").removeClass("show");

    if ($("#hdnAmount").val() != null) {
        addfuncds();
    }

});

var checkvalidity = function () {
    var valid = true;
    if ($("#amount").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter amount");
        valid = false;
    }
    return valid;
}


var callPayPal = function () {
    if ($("#field-paypal_email").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please provide paypal email");
        return false;
    }
    else {
        obj = {};
        obj.cost = $("#amount").val();
        var paypalurl = '/Service/PayPal';
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
var callWebMoney = function () {
    var obj = {};
    obj.LMI_PAYMENT_AMOUNT = "1.0";
    obj.LMI_PAYMENT_DESC = "test payment";
    obj.LMI_PAYMENT_NO = "1";
    obj.LMI_PAYEE_PURSE = "Z145179295679";
    obj.LMI_SIM_MODE = "0";
    var paypalurl = 'https://merchant.wmtransfer.com/lmi/payment.asp';
    $.ajax({
        type: "POST",
        url: paypalurl,
         data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data);
           // addfuncds();
           // var url = data;
          //  window.location.href = url;
        },
        error: function (err) {
            alert("Error in processing payment.")
        }
    });
}
var callPerfectMoney = function () {
    var paypalurl = '/Service/PerfectMoney';
    $.ajax({
        type: "POST",
        url: paypalurl,
        //   data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            addfuncds();
            var url = data;
            window.location.href = url;
        },
        error: function (err) {
            alert("Error in processing payment.")
        }
    });
}
var callBTH = function () {
    var paypalurl = '/Service/BTH';
    $.ajax({
        type: "POST",
        url: paypalurl,
        //   data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
           // addfuncds();
            var url = data;
            window.location.href = url;
        },
        error: function (err) {
            alert("Error in processing payment.")
        }
    });
}
var addfuncds = function () {
    var serviceURL = '/Service/SaveFunds';

    var obj = {};
//    obj.Amount = $("#amount").val();
    $("#divLoading").addClass("show");

    $.ajax({
        type: "POST",
        url: serviceURL,
    //    data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        $("#divLoading").removeClass("show");

        if (data[0] == "1") {
            $(".badge").html(data[1]);
        }
        else {
            $("#divLoading").removeClass("show");

            alert("Something went wrong!")
        }
    }

    function errorFunc(err) {
        alert(err.responseText);
    }
}
var saveFunds = function () {
    var valid = checkvalidity();
    if (valid == true)
    {
        $(".alert").addClass("hidden");

        var paymentMethod = $("#order_type").val();
        if (paymentMethod.toLowerCase() == "paypal") {
            callPayPal();
        }
        else if (paymentMethod.toLowerCase() == "btc") {
            callBTH();
        }
        else if (paymentMethod.toLowerCase() == "perfectmoney") {
            callPerfectMoney();
        }
        else if (paymentMethod.toLowerCase() == "webmoney") {
            callWebMoney();
        }
        return false;

    }

    else {
        return false;
    }
}

$("#order_type").change(function () {
    if ($('#order_type').val() == "Paypal") {
        $("#email-grp").show();
    }
    else {
        $("#email-grp").hide();
    }
});

