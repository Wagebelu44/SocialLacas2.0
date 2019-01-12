﻿$(document).ready(function () {
    $(".accountuser").addClass('active');
    $("#divLoading").removeClass("show");

    //var isAdmin =$("#hdnIsAdmin").val();
    //if (isAdmin == "1") {
    //    $('.clsUser').hide();
    //    $('.clsadmin').show(); }
    //else {
    //    $('.clsUser').show();
    //    $('.clsadmin').hide(); }

});
function validate() {
    var isAdmin = $("#hdnIsAdmin").val();
    var valid = true;
    if (($("#username").val() == "") && (isAdmin != "1")) {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter username");
        valid = false;
    }
    else if ($("#current").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter current password");
        valid = false;
    }
    else if ($("#new").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter new password");
        valid = false;
    }
    else if ($("#confirm").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please confirm password");
        valid = false;
    }
    else if ($("#new").val() != $("#confirm").val()) {
        $(".alert").removeClass("hidden");
        $(".alert").text("Password dont match!");
        valid = false;
    }

    return valid;
}

changePassword = function () {
    var valid = validate();
    if (valid == true) {
        $(".alert").addClass("hidden");
        $("#divLoading").addClass("show");

        var serviceURL = '/Service/changePassword';
        var obj = {};
        var isAdmin = $("#hdnIsAdmin").val();
        if (isAdmin == "1") {
            obj.username = "Admin";
        }
        else {
            obj.username = $("#username").val();
        }
        obj.oldpassword = $("#current").val();
        obj.newpassword = $("#new").val();
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

            if ((data == "0") || (data == "-1")) {
                alert("Password changed successfully");
                window.location.href("/visitor/signin");
            }
            else {
                alert("Invalid username or password")
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