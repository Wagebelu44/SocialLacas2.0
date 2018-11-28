var submitTicket = function () {
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
            alert("New ticket saved.");
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