$(document).ready(function () {
    loadTable(); // Fetch data when the page loads
});
function clearForm() {
    $(".txtName").val("");
    $(".txtCode").val("");
    $(".hdnId").val("");
}


function loadTable() {
    $.ajax({
        method: 'GET',
        url: '/Category/GetCategories', // Fetch data from controller
        success: function (res) {
            var tableBody = $("table tbody");
            tableBody.empty(); // Clear existing rows

            $.each(res.data, function (index, category) {
                var row = `<tr>
                              <td>${index + 1}</td>
                                    <td>${category.categoryName}</td>
                                    <td>${category.categoryCode}</td>
                                    <td>${category.createdAt}</td>
                                    <td>
                                        <button class="btn btn-warning btnEdit" data-key="${category.categoryId}">Edit</button>
                                        <button class="btn btn-danger btnDlt" data-key="${category.categoryId}">Delete</button>
                                    </td>
                            </tr>`;

                tableBody.append(row);
            });
        },
        error: function () {
            alert("Failed to load data!");
        }
    });

}


/*Edit button */
$(document).on("click", ".btnEdit", function () {

    var id = $(this).data('key');

    $.ajax({
        method: 'get',
        contentType: "application/json; charset=utf-8",
        url: '/Category/Edit?id=' + id,
        success: function (res) {
            if (res.success == false) {
                alert(res.message)
            }
            else {
                $(".txtName").val(res.data.categoryName);
                $(".txtCode").val(res.data.categoryCode);
                $(".hdnId").val(res.data.categoryId);

            }
        },
        error: function (res) {
            alert(res.message);
        }
    });

});

//delete ajax
$(document).on("click", ".btnDlt", function () {
    var id = $(this).data('key');

    $.ajax({
        method: 'get',
        contentType: "application/json; charset=utf8",
        url: "/Category/Delete?id=" + id,
        success: function (res) {
            loadTable();
            alert(res.message);
        },
        error: function (res) {
            alert(res.message);
        }
    })
})

$(document).on("click", ".btnCancel", function () {
    var ok = confirm("Are you sure you want to cancel");
    if (ok) {
        $(".txtName").val("");
        $(".txtCode").val("");
        alert("Cancelled");

    }
    else {
        alert("Not Cancelled");
    }
})

$(document).on("click", ".btnSave", function () {
    var name = encodeURIComponent($(".txtName").val()); // Encode special characters
    var code = encodeURIComponent($(".txtCode").val());
    var hiddenId = $(".hdnId").val();

    var createcategorydto = {
        hiddenId : hiddenId,
        CategoryName : name,
        CategoryCode : code
    }

    if (name == "" || code == "") {
        alert("Please fill all the fields");
    }
    else {
        $.ajax({
            method: 'POST',
            url: '/Category/Save',
            data: JSON.stringify(createcategorydto),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                success = response.success;
                if (success == true) {
                    loadTable();
                    clearForm();
                }
                alert(response.message);
            },
            error: function (response) {
                alert(response.message);
            }
        });
    }






})