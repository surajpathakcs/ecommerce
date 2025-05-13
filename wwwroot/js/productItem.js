$(document).ready(function () {
    loadTable();
});
let categoriesLoaded = false;

/*$(document).ready(function () {
    $("#CategoryInput").on("focus", function () {
        if (!categoriesLoaded) {
            $.ajax({
                method: 'GET',
                url: '/Category/GetCategories',
                success: function (res) {
                    if (res.data && res.data.length > 0) {
                        const datalist = $("#categoryList");
                        datalist.empty(); // clear existing options
                        $.each(res.data, function (index, category) {
                            datalist.append(`<option value="${category.categoryName}" data-id="${category.categoryId}"></option>`);
                        });
                        categoriesLoaded = true; // load only once
                    }
                },
                error: function () {
                    alert("Failed to fetch categories.");
                }
            });
        }
    });
});*/
$(document).ready(function () {
    let categoriesLoaded = false;
    $("#CategorySelect").on("focus", function () {
        if (!categoriesLoaded) {
            $.ajax({
                method: 'GET',
                url: '/Category/GetCategories',
                success: function (res) {
                    if (res.data && res.data.length > 0) {
                        const select = $("#CategorySelect");
                        select.empty(); // clear existing options
                        select.append('<option value="" disabled selected>Select Category</option>'); // default option
                        $.each(res.data, function (index, category) {
                            select.append(`<option value="${category.categoryId}">${category.categoryName}</option>`);
                        });
                        categoriesLoaded = true; // load only once
                    }
                },
                error: function () {
                    alert("Failed to fetch categories.");
                }
            });
        }
    });

    // Listen for change event to capture the selected category's ID
    $('#CategorySelect').on('change', function () {
        const selectedCategoryId = $(this).val();
        $("#CategoryId").val(selectedCategoryId); // Store the categoryId in the hidden input
    });
});

function loadTable() {
    $.ajax({
        method: 'GET',
        url: '/ProductItem/GetProductItems',
        success: function (res) {
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



$(document).on("click", ".btnEdit", function () {
    var id = $(this).data("key");

    $.ajax({
        method: 'GET',
        url: '/ProductItem/GetById?id=' + id,
        success: function (res) {
            if (res.success) {
                var product = res.data;
                $(".txtName").val(product.productItemName);
                $(".txtCode").val(product.productItemCode);
                $(".txtDescription").val(product.description);
                $(".txtPrice").val(product.unitPrice);
                $(".txtThumbnail").val(product.thumbnail);
                $(".hdnId").val(product.productItemId);
                $("#CategoryId").val(product.categoryId);

                $.ajax({
                    method: 'GET',
                    url: '/Category/GetCategories',
                    success: function (resCat) {
                        if (resCat.data && resCat.data.length > 0) {
                            const select = $("#CategorySelect");
                            select.empty();
                            select.append(`<option value="" disabled>Select Category</option>`);
                            $.each(resCat.data, function (index, category) {
                                const isSelected = category.categoryId === product.categoryId ? 'selected' : '';
                                select.append(`<option value="${category.categoryId}" ${isSelected}>${category.categoryName}</option>`);
                            });
                        }
                    },
                    error: function () {
                        //  alert("Failed to fetch categories.");
                    }
                });
            } else {
                alert(res.message);
            }
        },
        error: function () {
            alert("Failed to fetch product details.");
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

$(document).on("click", ".btnSave", function () { ///////////////////////        SAVE
    var id = $(".hdnId").val();
    var createproductdto = {
        ProductItemName: $(".txtName").val(),
        ProductItemCode: $(".txtCode").val(),
        CategoryId: parseInt($(".hiddenCategoryId").val()), // Parse as integer
        Description: $(".txtDescription").val(),
        UnitPrice: parseFloat($(".txtPrice").val()), // Parse as decimal
        Thumbnail: $(".txtThumbnail").val()
    };

    if (id && id != "0") {
        createproductdto.ProductItemId = parseInt(id); // Parse as integer
        $.ajax({
            method: 'POST',
            url: '/ProductItem/Update', // Fixed: removed Url.Action, used direct path
            contentType: 'application/json',
            data: JSON.stringify(createproductdto),
            success: function (response) {
                alert("Updated successfully");
                loadTable();
                clearForm();
            },
            error: function (xhr) {
                alert("Error updating: " + (xhr.responseJSON?.message || "Unknown error"));
            }
        });
    } else {
        $.ajax({
            method: 'POST',
            url: '/ProductItem/Create', // Fixed: removed Url.Action, used direct path
            contentType: 'application/json',
            data: JSON.stringify(createproductdto),
            success: function (response) {
                alert("Product created successfully");
                loadTable();
                clearForm();
            },
            error: function (xhr) {
                alert("Error creating: " + (xhr.responseJSON?.message || "Unknown error"));
            }
        });
    }
});

// Fix the Delete button handler too
$(document).on("click", ".btnDlt", function () { ///////////////////////        DELETE
    var id = $(this).data('key');

    $.ajax({
        method: 'DELETE', // Changed from GET to DELETE to match controller
        url: '/ProductItem/Delete?id=' + id, // Fixed: removed Url.Action, used direct path
        success: function (res) {
            loadTable();
            alert(res.message);
        },
        error: function (xhr) {
            alert(xhr.responseJSON?.message || "Error deleting item");
        }
    });
});

// Fix the loadItems function that's called on document ready
function loadItems() {
    loadTable(); // This calls the correct function
}

/*$(document).on("click", ".btnSave", function () {///////////////////////        SAVE
    var id = $(".hdnId").val();
    var createproductdto = {
        ProductItemName: $(".txtName").val(),
        ProductItemCode: $(".txtCode").val(),
        CategoryId: $(".txtCategory").val(),
        Description: $(".txtDescription").val(),
        UnitPrice: $(".txtPrice").val(),
        Thumbnail: $(".txtThumbnail").val()
    };

    if (id && id != "0") {
        createproductdto.ProductItemId = id;
        $.ajax({
            method: 'POST',
            url: '@Url.Action("Update", "ProductItem")',
            contentType: 'application/json',
            data: JSON.stringify(createproductdto),
            success: function (response) {
                alert("Updated successfully");
                loadTable();
                debugger;
                clearForm();
            },
            error: function () {
                alert("Error updating");
            }
        });
    } else {
        $.ajax({
            method: 'POST',
            url: '@Url.Action("Create", "ProductItem")',
            contentType: 'application/json',
            data: JSON.stringify(createproductdto),
            success: function (response) {
                alert("Product created successfully");
                loadTable();
                clearForm();
            },
            error: function () {
                alert("Error creating");
            }
        });
    }

});
*/

