
function checkvalidity() {
    var valid = true;
  
    
    if (($("#txtusername").val() == "")) {
        alert("Please enter username.");
        valid = false;
    }
    else if ($("#txtpassword").val() == "") {
        valid = false;
        alert("Please enter password.");
    }
    else if ($("#txtemail").val() == "") {
        valid = false;
        alert("Please enter email.");
    }
    else if ($("#txtemail").val() != "") {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

        if (!expr.test($("#txtemail").val())) {
            valid = false;
            alert("Please enter valid email.");
        }
    }
    else if ($.trim($("#txtconfirm").val()) == $.trim($("#txtconfirm").val())) {
        valid = false;
        alert("Password does not match.");
    }

    return valid;
}
var SaveUser = function () {
    var valid=checkvalidity();
    if (valid == true) {
        var serviceURL = '/Service/SaveUser';

        var obj = {};
        var isChecked = $('#chkCaptcha').is(":checked");
        obj.userName = $("#txtusername").val();
        obj.password = $("#txtpassword").val();
        obj.email = $("#txtemail").val();
        if (isChecked == true) {
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

                alert("User Saved.");
                window.location = '/';
            }

            function errorFunc(err) {
                alert(err.responseText);
            }
        }
        else {
            alert("Please accept the terms and conditions!")
        }
    }
    else {
        return false;
    }
    

}