var saveTicketMessage = function () {

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
        if (data[0] == "1") {
            alert("New message saved.");
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