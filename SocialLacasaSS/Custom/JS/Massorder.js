var objMassOrder = [];
var CalculateTotalCharge = function () {
    for (var i = 0; i < objMassOrder.length; i++) {
        var charge = objMassOrder[i].quantity;

    }
}
var getObjectOrder = function () {
    var massOrders = $("#links").val();
    var massorderArray = massOrders.split("\n");
    $.each(massorderArray, function (index, item) {
        var order = item.split('|');
        var orderArray = {};
        orderArray.ServiceId = order[0];
        orderArray.Link = order[1];
        orderArray.Quantity = order[2];
        orderArray.Charge = 0;
        objMassOrder.push(orderArray);
    });
}
var SaveMassOrder = function () {
    getObjectOrder();
   
  //  var totalCharge = CalculateTotalCharge();
    //return false;
    //var IsFundsExist = GetAccountFunds(totalCharge);
    //if (IsFundsExist == "1") {
        var serviceURL = '/Service/SaveMassOrder';

        var obj = {};
        obj.MassOrder = objMassOrder;
       
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
    //}
    //else {
    //    alert("Insuficient Funds!")
    //}

}