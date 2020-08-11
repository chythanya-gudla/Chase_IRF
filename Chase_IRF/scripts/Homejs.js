//Apply colors based on status

$(document).ready(function () {
    var otable = $('#ordersTable').DataTable({
        "createdRow": function (row, data, dataIndex) {
            if (data[4] == 1) {
                $(row).addClass('grey');

            }


        }
    });
});