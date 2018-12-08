
$(document).ready(function () {

//    var selector = '.nav li';
//    var tab = localStorage["tab"];
//    $("[tab*='" + tab + "']").addClass('active');
//    //if (localStorage["tab"] != null) {
//      //  $(selector).removeClass('active');
//        $(this).addClass('active');
//    //}
//    $(selector).on('click', function () {
//        localStorage["tab"] = $(this).attr("tab");
        
//        $(selector).removeClass('active');
//        $(this).addClass('active');
        
//    });

   
        var isAdmin = localStorage["isAdmin"];

        if (isAdmin == "1") {
            $('.clsUser').hide();
            $('.clsadmin').show();
        }
        else {
            $('.clsUser').show();
            $('.clsadmin').hide();
        }

   
    });

   
    var LogOut = function () {




        $.ajax({
            type: "POST",
            url: "/Service/logOut",

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                localStorage["isAdmin"] = null;
                window.location.href = "/Visitor/SignIn";


            },
            error: function (err) { }
        });


    }



