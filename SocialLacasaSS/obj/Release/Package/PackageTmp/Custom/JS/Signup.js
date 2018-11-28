var SaveUser = function () {
    var serviceURL = '/Service/SaveUser';
   
    var obj = {};
    var isChecked = $('#chkCaptcha').is(":checked");
    obj.userName = $("#txtusername").val();
    obj.password = $("#txtpassword").val(); 
    obj.email = $("#txtemail").val(); 
    if (isChecked==true) {
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