﻿$(document).ready(function () {
    $(".nav").removeClass("active");
    $(".massorder").addClass('active');

    $("#divLoading").removeClass("show");

    var isAdmin = $("#hdnIsAdmin").val();
    if (isAdmin == "1") {
        checkBalance();
    }


});

function checkvalidity() {
    var valid = true;
    if ($("#links").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter correct links");
        valid = false;
    }

    return valid;

}

var objMassOrder = [];
var CalculateTotalCharge = function () {
    for (var i = 0; i < objMassOrder.length; i++) {
        var charge = objMassOrder[i].quantity;

    }
}
var getObjectOrder = function () {
    //get object
    var massOrders = $("#links").val();
    var massorderArray = massOrders.split("\n");
    $.each(massorderArray, function (index, item) {
        var order = item.split('|');
        var orderArray = {};
        orderArray.ServiceId = order[0];
        orderArray.Link = order[1];
        orderArray.Quantity = order[2];
        orderArray.Charge = 0;
        objMassOrder.push(orderArray);
    });
}
var SaveMassOrder = function () {
    var valid = checkvalidity();
    if (valid == true) {
        $(".alert").addClass("hidden");

        getObjectOrder();

        //  var totalCharge = CalculateTotalCharge();
        //return false;
        //var IsFundsExist = GetAccountFunds(totalCharge);
        //if (IsFundsExist == "1") {
        var serviceURL = '/Service/SaveMassOrder';

        var obj = {};
        obj.MassOrder = objMassOrder;
        obj.funds = $(".badge").html();
        $("#divLoading").addClass("show");

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
            $("#divLoading").removeClass("show");

            if (data[0] == "1") {
                alert("New order saved.");
                location.reload(true);
            }
            else {
                $("#divLoading").removeClass("show");

                alert("Something went wrong!")
            }
        }

        function errorFunc(err) {
            alert(err.responseText);
        }
        //}
        //else {
        //    alert("Insuficient Funds!")
        //}
    }
    else {
        return false;
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
                var numstatus = parseFloat(status);
                var inusd = (numstatus / 68.0).toFixed(2);
                $(".mybal").text(inusd);
            }
            else {
                $("#divLoading").removeClass("show");
                alert(data);
            }

        },
        error: function (err) {
            $("#divLoading").removeClass("show");
        }
    });


}