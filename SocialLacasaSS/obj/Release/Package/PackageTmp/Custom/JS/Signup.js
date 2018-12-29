$(document).ready(function () {
    $(".vsignup").addClass('active');
    $("#divLoading").removeClass("show");




});

function checkavailability() {
    var obj = {};
    obj.username = $("#txtusername").val();
    obj.email = $("#txtemail").val();
    var serviceURL = '/Service/checkusername';
    $("#divLoading").addClass("show");

    var aval = "0";
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


         aval = data; 
    }

    function errorFunc(err) {
        $("#divLoading").removeClass("show");

         aval = "0";
    }
    return aval;

}


function checkvalidity() {
    var valid = true;
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

  
    
    if (($("#txtusername").val() == "")) {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter username");
        valid = false;
    }
    else if ($("#txtpassword").val() == "") {
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter password");
        valid = false;
    }
    else if ($("#txtemail").val() == "") {
        valid = false;
        $(".alert").removeClass("hidden");
        $(".alert").text("Please enter email");
    }


        else if (!expr.test($("#txtemail").val())) {
            valid = false;
            $(".alert").removeClass("hidden");
            $(".alert").text("Please enter valid email");
        }
    
    else if ($("#txtconfirm").val() != ($("#txtpassword").val()))
    {
        valid = false;
        $(".alert").removeClass("hidden");
        $(".alert").text("Passwords do not match");
    }

    return valid;
}
var SaveUser = function () {
    var valid=checkvalidity();
    if (valid == true) {
        $(".alert").addClass("hidden");
        var serviceURL = '/Service/SaveUser';

        var obj = {};
        var isChecked = $('#chkCaptcha').is(":checked");
        obj.userName = $("#txtusername").val();
        obj.password = $("#txtpassword").val();
        obj.email = $("#txtemail").val();
       // aval = checkavailability();

        if (isChecked == true) {
            //if (aval == "0") {
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
                    if (data != "0") {
                        alert("Successfully signed up");
                        localStorage["isAdmin"] = "0";
                        window.location.href = "/User/NewOrder";
                    }
                    else {
                        $(".alert").removeClass("hidden");
                        $(".alert").text("User already exits!");
                       // alert("User already exist");
                    }
                }

                function errorFunc(err) {
                    alert(err.responseText);
                }
            //}
            //else {
            //    $(".alert").removeClass("hidden");
            //    $(".alert").text("User already exits!");
            //}
        }
        else {
            alert("Please accept the terms and conditions!")
        }
    }
    else {
        return false;
    }
    

}