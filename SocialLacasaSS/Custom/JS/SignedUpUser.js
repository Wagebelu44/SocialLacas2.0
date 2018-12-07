var removeuser = function (objectptr) {
    var serviceURL = '/Service/removeUser';
    var obj = {};
    obj.userid = $(objectptr).closest("tr")   // Finds the closest row <tr> 
        .find(".uid")     // Gets a descendent with class="nr"
        .text();         // Retrieves the text within <td>
    console.log(obj.userid);

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

            alert("User Removed");
            window.location = '/';
        }

        function errorFunc(err) {
            alert(err.responseText);
        }
  

}