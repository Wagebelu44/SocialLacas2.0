var valid = false;
function checkvalidity() {
    if (($("#txtusername").val() != "") && ($("#txtpassword").val() != "")) {
        valid = true;
    }

}
var LoginUser = function () {
  //  localStorage["isAdmin"] = $("#hdnIsAdmin").val();

    checkvalidity();
    if (valid == true) {
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
                    localStorage["isAdmin"] = "0";
                    window.location.href = "/User/NewOrder";
                }
                else {
                    localStorage["isAdmin"] = "1";
                    window.location.href = "/Admin/PlaceOrder";
                }
            }
            else {
                alert("Invalid UserName or Password.")
            }

        }
    }
    else {
        alert("please enter username and password/")
    }

        function errorFunc(err) {
            alert(err.responseText);
        }
    

}