changePassword = function () {
    var serviceURL = '/Service/changePassword';
    var obj = {};
    obj.username = $("#username").val();
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
        if (data == "0") {
            alert("Password changed successfully");
            window.location.href("/visitor/signin");
        }
        else {
            alert("Invalid username or password")
        }
    }

    function errorFunc(err) {
        alert(err.responseText);
    }

}