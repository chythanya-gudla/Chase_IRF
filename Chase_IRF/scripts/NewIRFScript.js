
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function itemownername(val) {
    //if ('<%=Session["ItemOwnerEmailID"]%>' != null) {
    //    '<%=Session["ItemOwnerEmailID"]%>' = null;
    //}
    var url = location.protocol + '//' + location.host + '/NewIRF.aspx?value=' + val;
    window.location.href = url;
    return false;
}

function CheckItemExists(val) {
    var ItemType = $('#head_SelectItem').val();
    if (ItemType == "replaceditem") {
        var url = location.protocol + '//' + location.host + '/NewIRF.aspx?ItemValue=' + val + '&ItemType=' + ItemType;
        window.location.href = url;
        return false;
    }
}

function CopyItemExists() {
    var ItemNumber = document.getElementById("head_ItemNumber").value.toString();
    var ItemType = "copieditem";
        var url = location.protocol + '//' + location.host + '/NewIRF.aspx?ItemValue=' + ItemNumber + '&ItemType=' + ItemType;
        window.location.href = url;
        return false;

}

function ReplaceItemExists() {
    var ItemNumber = document.getElementById("head_ItemNumber").value.toString();
    var ItemType = "replaceditem";
    var url = location.protocol + '//' + location.host + '/NewIRF.aspx?ItemValue=' + ItemNumber + '&ItemType=' + ItemType;
    window.location.href = url;
    return false;

}

function cancelNewIRF() {
    var r = confirm("Are you sure you want to cancel the form?");
    if (r == true)
    {
        var url = location.protocol + '//' + location.host + '/Home.aspx';
        window.location.href = url;
    }
    return false;

}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

