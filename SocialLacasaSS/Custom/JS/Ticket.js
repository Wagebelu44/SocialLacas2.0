$(document).ready(function () {
    $(".nav").removeClass("active");
    $(".tickets").addClass('active');



});
var validity = function () {
    var valid = true;
    if ($("#subject").val() == "") {
        valid = false;
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter subject");
    }
    else if ($("#message").val() == "") {
        valid = false;
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter the issue");
    }
    return valid;
}
var submitTicket = function () {
    var valid = validity();
    if (valid == true) {
        $(".alert").addClass("hidden");

        var serviceURL = '/Service/SubmitTicket';

        var obj = {};


        // obj.userId = $("#hdnUserId").val();
        obj.subject = $("#subject").val();
        obj.ticketmessage = $("#message").val();

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
                window.location = window.location;
            }
            else {
                alert("Something went wrong!")
            }
        }

        function errorFunc(err) {
            alert(err.responseText);
        }
    }
    else {
        return false;
    }


}