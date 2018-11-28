var saveFunds = function () {
    
    
    var serviceURL = '/Service/SaveFunds';

    var obj = {};
    obj.Method = $("#method").val();
    obj.AccountName = $("#Name").val();
    obj.Accountnumber = $("#accountnumber").val();
    obj.Cvv = $("#cvv").val();
    obj.Amount = $("#amount").val();
    obj.expiry = $("#expiry").val();
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
            alert("Funds added.");
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