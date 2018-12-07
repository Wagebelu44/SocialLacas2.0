var callPayPal = function () {
    var paypalurl = '/Service/PayPal';
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
var callWebMoney = function () {
    var paypalurl = '/Service/WebMoney';
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
            addfuncds();
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
    obj.Method = $("#method").val();
    obj.AccountName = $("#Name").val();
    obj.Accountnumber = $("#accountnumber").val();
    obj.Cvv = $("#cvv").val();
    obj.Amount = $("#amount").val();
    obj.expiry = $("#expiry").val();
    // obj.userId = $("#hdnUserId").val();

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        if (data[0] == "1") {
            alert("Funds added.");
            location.reload(true);
        }
        else {
            alert("Something went wrong!")
        }
    }

    function errorFunc(err) {
        alert(err.responseText);
    }
}
var saveFunds = function () {
    var paymentMethod = $("order_type").val();
    if (paymentMethod == "PayPal") {
        callPayPal();
    }
    else if (paymentMethod == "BTH") {
        callBTH();
    }
    else if (paymentMethod == "PerfectMoney") {
        callPerfectMoney();
    }
    else if (paymentMethod == "WebMoney") {
        callWebMoney();
    }
    return false;
   

}

$("#order_type").change(function () {
    if ($('#order_type').val() == 46) {
        $("#email-grp").show();
    }
    else {
        $("#email-grp").hide();
    }
});

