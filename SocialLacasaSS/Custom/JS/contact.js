﻿$(document).ready(function () {
    $(".nav").removeClass("active");
    $(".contactus").addClass('active');
    $(".vcontact").addClass('active');


    $("#divLoading").removeClass("show");

    //var isAdmin = $("#hdnIsAdmin").val();
    //if (isAdmin == "1") {
    //    checkBalance();
    //}


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

function checkvalidity() {
    var valid = false;
    if (($("#txtname").val() != "") && ($("#txtemail").val() != "") && ($("#txtmsg").val() != "")) {
        valid = true;
    }
    else {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter the required fields");

    }
    return valid;

}


var sendmail = function () {
    var valid = checkvalidity();
    if (valid == true) {
        $(".alert").addClass("hidden");
        var serviceURL = '/Service/SendMail';
        $("#divLoading").addClass("show");
        var obj = {};
        obj.name = $("#txtname").val();
        obj.UserEmail = $("#txtemail").val();
        obj.message = $("#txtmsg").val();

        $.ajax({
            type: "POST",
            url: serviceURL,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
        });

        function successFunc(data, status) {
            $("#divLoading").removeClass("show");

            if (data != "") {

                if (data == "1") {
                    alert("mail sent");
                    window.location = window.location;
                }
                else {
                    alert("Something went wrong");
                    window.location = window.location;

                }

            }
            else {
                $("#divLoading").removeClass("show");
                alert("Something went wrong");
                window.location = window.location;

            }


        }

    }
}