$(document).ready(function () {
    $(".nav").removeClass("active");
    $(".tickets").addClass('active');

    $("#divLoading").removeClass("show");


});
var checkvalidity = function () {
    var valid = true;
    if ($("#message").val() == "") {
        valid = false;
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter message");
    }
    return valid;

}

var saveTicketMessage = function () {
    var valid = checkvalidity();
    if (valid == true) {
        $("#divLoading").addClass("show");

        var serviceURL = '/Service/saveTicketMessage';

        var obj = {};
        obj.message = $("#message").val();
        obj.ticketid = $(".clsticketid").val();
        obj.isAdmin = $("#hdnIsAdmin").val();

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
                //alert("New message saved.");
                location.reload(true);
            }
            else {
                alert("Something went wrong!")
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