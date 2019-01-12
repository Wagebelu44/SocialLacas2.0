﻿$(document).ready(function () {
    $("#divLoading").removeClass("show");

    $(".vsignin").addClass('active');



});


var valid = false;
function checkvalidity() {
    if (($("#txtusername").val() != "") && ($("#txtpassword").val() != "")) {
        valid = true;
    }
    else {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter username and password");

    }


}
var LoginUser = function () {
    //  localStorage["isAdmin"] = $("#hdnIsAdmin").val();

    checkvalidity();
    if (valid == true) {
        $(".alert").addClass("hidden");
        var serviceURL = '/Service/CheckUser';
        $("#divLoading").addClass("show")
        var obj = {};
        obj.userName = $("#txtusername").val();
        obj.password = $("#txtpassword").val();

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
            $("#divLoading").removeClass("show")

            if (data[0] != "") {

                if (data[1] == "0") {
                    localStorage["isAdmin"] = "0";
                    window.location.href = "/User/NewOrder";
                }
                else {
                    localStorage["isAdmin"] = "1";
                    window.location.href = "/Admin/PlaceOrder";
                }
            }
            else {
                $(".alert").removeClass("hidden");
                $(".alert").text("Invalid username and password");
            }

        }
        function errorFunc(err) {
            $("#divLoading").removeClass("show");

            alert(err.responseText);


        }

    }
    else {
        return false;
    }



}