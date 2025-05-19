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
        url: '/Category/Edit?id=' + id,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.success == false) {
                alert(res.message)
            }
            else {
                $(".txtName").val(res.data.data.categoryName);
                $(".txtCode").val(res.data.data.categoryCode);
                $(".hdnId").val(res.data.data.categoryId);

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
    var hiddenId = $(".hdnId").val();

    var createcategorydto = {
        CategoryName: $(".txtName").val(),
        CategoryCode: $(".txtCode").val()
    };

    // Add ID only if it's an edit operation
    if (hiddenId && hiddenId != "0") {
        createcategorydto.HiddenId = parseInt(hiddenId);
    }

    // Log what we're sending
    console.log("Sending data:", createcategorydto);

    $.ajax({
        method: 'POST',
        url: '/Category/Save',
        contentType: 'application/json',
        data: JSON.stringify(createcategorydto),
        success: function (response) {
            console.log("Response:", response);
            if (response.success) {
                alert(response.message);
                loadTable();
                clearForm();
            } else {
                alert("Error: " + response.message);
            }
        },
        error: function (xhr) {
            console.error("Error details:", xhr);
            alert("Error: " + (xhr.responseJSON?.message || "Failed to save category"));
        }
    });
});