var dtble;
$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url":"/Admin/Product/GetData"
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "description"},
            { "data": "price" },
            { "data": "category.name" },
            {
                "data": "image",
                "render": function (data, type, row) {
                    return `<img src="/Images/Products/${data}" alt="datat" style="width:50px;height:50px;" />`;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div>
                            <a href="/Admin/Product/Edit/${data}" class="btn btn-success">Edit</a>
                            <a onClick=DeleteItem("/Admin/Product/Delete/${data}") class="btn btn-danger">Delete</a>
                            </div>
                            `
                    
                }

                }

            

        ]
    });
}

function DeleteItem(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,                
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dtble.ajax.reload();                        
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })            
        }
    })
}


