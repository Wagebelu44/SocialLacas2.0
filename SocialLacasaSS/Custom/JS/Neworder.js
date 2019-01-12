﻿function checkvalidity() {
    var valid = true;
    if ($("#field-orderform-fields-link").val() == "") {
        $(".topalert").removeClass("hidden");
        $(".topalert").text("Please enter correct link");
        valid = false;
    }
    else if ($("#field-orderform-fields-quantity").val() == "") {
        $(".topalert").removeClass("hidden");
        $(".topalert").text("Please enter quantity");
        valid = false;
    }
    //else if ($("#field-orderform-fields-quantity").val() != "" && parseInt($("#field-orderform-fields-quantity").val()) < 1000) {
    //    //  var quantity = $("#field-orderform-fields-quantity").val();
    //    //   if (parseInt(quantity) < 1000) {
    //    $(".alert").removeClass("hidden");
    //    $(".alert").text("Please enter min 1000 in quantity.");
    //    valid = false;
    //    //   }

    //}
    else if ($("#charge").val() != "") {
        var charges = $("#charge").val();
        var isAdmin = $("#hdnIsAdmin").val();
        if (isAdmin != "1") {
            if (parseFloat(charges) > parseFloat($(".badge").html())) {
                //  $(".topalert").removeClass("hidden");
                // $(".alert").text("Insuficient Funds.");
                alert("Insufficient funds");
                valid = false;
            }
        }
    }
    return valid;

}


$(document).ready(function () {
    $(".nav").removeClass("active");
    $(".neworderuser").addClass('active');
    $("#divLoading").removeClass("show");
    var isAdmin = $("#hdnIsAdmin").val();




});


var callapi = function () {
    var obj = {};
    obj.serviceid = $("#ddlServices").val();
    obj.quantity = $("#field-orderform-fields-quantity").val();
    obj.link = $("#field-orderform-fields-link").val();
    $("#divLoading").addClass("show");

    $.ajax({
        type: "POST",
        url: "/Service/PlaceOrder_Api",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#divLoading").removeClass("show");

            if (data != null) {
                if (data == "0") {
                    alert("Error placing the order!");
                    return false;
                }
                else {
                    AddOrder(data);
                    // alert("Order Placed");
                }

            }
        },
        error: function (err) {
            $("#divLoading").removeClass("show");
        }
    });

}
var AddOrder = function (id) {

    var serviceURL = '/Service/SaveNewOrder';

    var obj = {};
    obj.category = $("#CatagoryName").val();
    obj.service = $("#ddlServices").val();
    obj.link = $("#field-orderform-fields-link").val();
    obj.quantity = $("#field-orderform-fields-quantity").val();
    obj.charge = $("#charge").val();
    obj.orderid = id;
    // obj.userId = $("#hdnUserId").val();
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
            alert("Order Placed");
            $(".badge").html(data[1]);
            window.location = window.location;

        }
        else {
            $("#divLoading").removeClass("show");

            alert("Something went wrong!")
        }
    }

    function errorFunc(err) {
        $("#divLoading").removeClass("show");
        alert("Check Order Quantity");
        // alert(err.responseText);
    }
}
var SaveNewOrder = function () {
    var valid = checkvalidity();
    if (valid == true) {
        $(".alert").addClass("hidden");
        callapi();

    }
    else {
        return false;
    }

}
var rate = 100;
$(document).ready(function () {
    // var category = $("#CatagoryName").val();
    var isAdmin = $("#hdnIsAdmin").val();
    //if (isAdmin == "1") {
    //    checkBalance();
    //}

    BindServices();
})
var BindServices = function () {
    var serviceURL = '/Service/BindServices';
    var categoryid = $("#CatagoryName").val();// $(id).val();

    var obj = {};
    obj.category = categoryid;
    $.ajax({
        type: "POST",
        url: serviceURL,
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json",
        success: function (res) {
            $("#ddlServices").empty();
            $.each(res, function (data, value) {
                //  quantity = parseInt($("#field-orderform-fields-quantity").val());

                // rate = value.Rate;
                // $("#rate").val(rate);



                $("#ddlServices").append($("<option></option>").val(value.SWserviceId).html(value.ServiceTypeRate).attr("rate", value.Rate).attr("Desc", value.Description));
                rate = $("#ddlServices option:selected").attr("rate");
                desc = $("#ddlServices option:selected").attr("Desc");
                $("#rate").val(rate);
                $("#dvDescription").html(desc);
                //$("#dvDescription").html(value.Description);

            })
        }

    });
}
$("#ddlServices").on('change', function () {
    rate = $("#ddlServices option:selected").attr("rate");
    desc = $("#ddlServices option:selected").attr("Desc");
    $("#rate").val(rate);
    $("#dvDescription").text(desc);

});
$("#field-orderform-fields-quantity").focusout(function () {
    var isAdmin = $("#hdnIsAdmin").val();

    if ($("#field-orderform-fields-quantity").val() != "") {
        var qu = $("#field-orderform-fields-quantity").val();
        var quantity = parseInt(qu);
        var charge = quantity * (rate / 1000);
        var discount = $("#hdnDiscount").val();
        if (isAdmin != "1") {
            if (discount != "0.00") {
                var discountcharge = charge * (discount / 100);
                charge = charge - discountcharge;
                $(".disalert").removeClass("hidden");
                $(".disalert").text("Discount: " + discount + "%");
            }
        }


        var ch = charge.toFixed(3).toString();
        $("#charge").val(ch);
    }


});

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