var LoginUser = function () {
    alert(1);
    var serviceURL = '/Service/CheckUser';

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
            if (data[0] != "") {
                
                if (data[1] == "0") {
                  
                    window.location.href = "/User/NewOrder";
                }
                else {
                    
                    window.location.href = "/Admin/PlaceOrder";
                }
            }
            else {
                alert("Invalid UserName or Password.")
            }
           
        }

        function errorFunc(err) {
            alert(err.responseText);
        }
    

}