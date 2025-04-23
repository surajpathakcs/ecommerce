$(document).ready(function () {
    loadItems();
});

function loadTable() {
    $.ajax({
        method: 'GET',
        url: '/ProductItem/GetProductItems',
        success: function (res) {
            debugger
            var tableBody = $("table tbody");
            tableBody.empty();
            $.each(res.data, function (index, product) {
                var row = `<tr>
                                 <td>${index + 1}</td>
                                 <td>${product.productItemName}</td>
                                 <td>${product.productItemCode}</td>
                                 <td>${product.categoryId}</td>
                                 <td>${product.description}</td>
                                 <td>${product.unitPrice}</td>
                                 <td><img src="${product.thumbnail}" alt="Thumbnail" width="50"></td>
                                 <td>
                                     <button class="btn btn-warning btnEdit" data-key="${product.productItemId}">Edit</button>
                                     <button class="btn btn-danger btnDlt" data-key="${product.productItemId}">Delete</button>
                                 </td>
                             </tr>`;

                tableBody.append(row);
            });

            //$("table tbody").html(row)
        },
        error: function () {
            alert("Failed to load data!");
        }
    });
}

function clearForm() {
    $(".txtName").val("");
    $(".txtCode").val("");
    $(".txtCategory").val("");
    $(".txtDescription").val("");
    $(".txtPrice").val("");
    $(".txtThumbnail").val("");
    $(".hdnId").val("0");
};



$(document).on("click", ".btnEdit", function () { ///////////////////////        EDIT
    var id = $(this).data('key');

    $.ajax({
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        url: '@Url.Action("Edit", "ProductItem")?id=' + id,
        success: function (res) {
            if (res.success == false) {
                alert(res.message);
            } else {
                $(".txtName").val(res.data.productItemName);
                $(".txtCode").val(res.data.productItemCode);
                $(".txtCategory").val(res.data.categoryId);
                $(".txtDescription").val(res.data.description);
                $(".txtPrice").val(res.data.unitPrice);
                $(".txtThumbnail").val(res.data.thumbnail);
                $(".hdnId").val(res.data.productItemId);
            }
        },
        error: function (res) {
            alert(res.message);
        }
    });
});


$(document).on("click", ".btnDlt", function () { ///////////////////////        DELETE
    var id = $(this).data('key');

    $.ajax({
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        url: '@Url.Action("Delete", "ProductItem")?id=' + id,
        success: function (res) {
            loadTable();
            alert(res.message);
        },
        error: function (res) {
            alert(res.message);
        }
    });
});



$(document).on("click", ".btnCancel", function () {///////////////////////        CANCEL
    var ok = confirm("Are you sure you want to cancel?");
    if (ok) {
        clearForm();
        alert("Cancelled");
    } else {
        alert("Not Cancelled");
    }
});



$(document).on("click", ".btnSave", function () {///////////////////////        SAVE

    alert("Helloo");
    var createproductdto = {
        ProductItemName: $(".txtName").val(),
        ProductItemCode: $(".txtCode").val(),
        CategoryId: $(".txtCategory").val(),
        Description: $(".txtDescription").val(),
        UnitPrice: $(".txtPrice").val(),
        Thumbnail: $(".txtThumbnail").val()
    }
    $.ajax({
        method: 'POST',
        url: '@Url.Action("Create", "ProductItem")',
        contentType: 'application/json',
        data: JSON.stringify(createproductdto),
        success: function (response) {
            if (response.success) {
                alert('Product created successfully');
            } else {
                alert('Error: ' + response.message);
            }
        },
        error: function(xhr, status, error) {
                alert('An error occurred: ' + error);
            }
        });

});

