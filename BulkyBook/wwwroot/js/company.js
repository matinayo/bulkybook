var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "12%" },
            { "data": "streetAddress", "width": "12%" },
            { "data": "city", "width": "12%" },
            { "data": "state", "width": "12%" },
            { "data": "postalCode", "width": "12%" },
            { "data": "phoneNumber", "width": "12%" },
            { "data": "isAuthorizedCompany", "width": "12%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Admin/Company/Upsert/${data}" class="btn btn-success text-white" style="cursor: pointer">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a onClick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor: pointer">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>`;
                }, "width": "40%"
            }
        ]

    });
}

function Delete(url) {
    // display sweet alert
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        // make ajax call to api delete
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}