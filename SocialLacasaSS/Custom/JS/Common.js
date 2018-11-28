$(document).ready(function () {
    var selector = '.nav li';

    $(selector).on('click', function () {
        $(selector).removeClass('active');
        $(this).addClass('active');
        
    });
    var isAdmin = localStorage["isAdmin"];
   
    if (isAdmin == "1") {
        $('.clsUser').hide();
        $('.clsadmin').show();
    }
    else {
        $('.clsUser').show();
        $('.clsadmin').hide();
    }
})

var LogOut = function () {
   

  
        
        $.ajax({
            type: "POST",
            url: "/Service/logOut",
          
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                localStorage["isAdmin"]=null;
                    window.location.href = "/Visitor/SignIn";

                
            },
            error: function (err) { }
        });

    
}

