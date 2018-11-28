
$(document).ready(function () {
    $('.clsUser').show();
    $('.clsadmin').hide();
})


var callapi = function () {
    var obj = {};
    obj.serviceid = $("#ddlServices").val();
    obj.quantity = $("#field-orderform-fields-quantity").val();
    obj.link = $("#field-orderform-fields-link").val();
    $.ajax({
        type: "POST",
        url: "/Service/PlaceOrder_Api",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                alert("Order Placed");

            }
        },
        error: function (err) { }
    });

}

var SaveNewOrder = function () {

    var serviceURL = '/Service/SaveNewOrder';

    var obj = {};
    obj.category = $("#CatagoryName").val();
    obj.service = $("#ddlServices").val();
    obj.link = $("#field-orderform-fields-link").val();
    obj.quantity = $("#field-orderform-fields-quantity").val();
    obj.charge = $("#charge").val();
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
            alert("New order saved.");
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
var rate = 100;
$(document).ready(function () {
    // var category = $("#CatagoryName").val();

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

                rate = value.Rate;



                $("#ddlServices").append($("<option></option>").val(value.SWserviceId).html(value.ServiceType));
                $("#dvDescription").html(value.Description);

            })
        }

    });
}
$("#field-orderform-fields-quantity").focusout(function () {
    if ($("#field-orderform-fields-quantity").val() != "") {
        var qu = $("#field-orderform-fields-quantity").val();
        var quantity = parseInt(qu);
        var charge = quantity * (rate / 1000);
        var ch = charge.toFixed(3).toString();
        $("#charge").val(ch);
    }


});