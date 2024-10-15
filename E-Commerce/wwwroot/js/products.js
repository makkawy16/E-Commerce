var dtble;
$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetData",
            "dataSrc": "data"  // Ensure this is correct, or leave out if default
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" },
            {


                "data": "id",
                "render": function (data) {
                    return `
            <a href="/Admin/Product/Edit/${data}" class="btn btn-success">Edit</a>
            <a href="/Admin/Product/Delete/${data}" class="btn btn-danger">Delete</a>
        `;


                }
            }


        ]

    });
}
