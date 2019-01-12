﻿$(document).ready(function () {

    $("#divLoading").removeClass("show");

});
var showUpdate = function (obtpt) {
    var userid = $(obtpt).closest("tr").find(".uid").text();
    var txtdis = "txtdiscount" + "_" + userid;
    $("#" + txtdis).prop("disabled", false);
    var savebutton = "spsave" + "_" + userid;
    var editbutton = "spedit" + "_" + userid;
    $('#' + savebutton).css('display', '');
    $('#' + editbutton).css('display', 'none');

}
var showEdit = function (obtpt) {
    var userid = $(obtpt).closest("tr").find(".uid").text();
    var txtdis = "txtdiscount" + "_" + userid;

    var savebutton = "spsave" + "_" + userid;
    var editbutton = "spedit" + "_" + userid;
    $('#' + savebutton).css('display', 'none');
    $('#' + editbutton).css('display', '');
    var discount = $(obtpt).closest("tr").find(".clsdis").val();
    $("#" + txtdis).prop("disabled", true);
    updateDiscount(userid, discount);


}
var updateDiscount = function (userid, discount) {
    $("#divLoading").addClass("show");

    var serviceURL = '/Service/updateDiscount';
    var obj = {};
    obj.userid = userid;  // Finds the closest row <tr> 
    obj.discount = discount;// $(objectptr).closest("tr")   // Finds the closest row <tr> 
    //.find(".uid")     // Gets a descendent with class="nr"
    //.text();         // Retrieves the text within <td>
    // console.log(obj.userid);

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

        alert("Discount Updated.");
        location.reload();
    }

    function errorFunc(err) {
        $("#divLoading").removeClass("show");

        alert(err.responseText);
    }


}
var removeuser = function (objectptr) {
    $("#divLoading").addClass("show");

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
        $("#divLoading").removeClass("show");

        alert("User Removed");
        location.reload();
    }

    function errorFunc(err) {
        $("#divLoading").removeClass("show");

        alert(err.responseText);
    }


}
$("tbody > tr").first().addClass("hidden");