$(document).ready(function () {
    loadItems();
});

function loadItems() {
    var pro = localStorage.getItem('ls_product') || '[]';
    var obj = JSON.parse(pro);  // []

    var htmls = '';

    $.each(obj, function (i, x) {
        htmls += '<tr>';
        htmls += '<td>' + (i + 1) + '</td>';
        htmls += '<td>' + x.ProductName + '</td>';
        htmls += '<td>' + x.UnitPrice + '</td>';
        htmls += '<td><input data-key="' + x.Id + '" type="text" class="form-control txtQuantity" value="' + x.Quantity + '"></td>';
        htmls += '<td>' + x.Total + '</td>';
        htmls += '<td><button data-key="' + x.Id + '" type="button" class="btn btn-danger btn-sm btnRemoveItem"><i class="fas fa-trash"></i> Del</button></td>';
        htmls += '</tr>';
    })

    $('.body-item').html(htmls);
}

//clear cart
$(document).on('click', '.btnClearCart', function () {
    var ok = confirm('Are you sure to clear cart?')
    if (ok) {
        localStorage.removeItem('ls_product');
        loadItems();
    }
})

//Remove item
$(document).on('click', '.btnRemoveItem', function () {
    var ok = confirm('Are you sure to remove this product?')
    if (ok) {
        var id = $(this).data('key');

        var pro = localStorage.getItem('ls_product') || '[]';
        var obj = JSON.parse(pro);  // []

        obj = obj.filter(x => x.Id != id);

        localStorage.setItem('ls_product', JSON.stringify(obj));
        loadItems();
    }
})

//Quantity Changing
$(document).on('change', '.txtQuantity', function () {

    var id = $(this).data('key'); // 3
    var newQuantity = $(this).val();

    var pro = localStorage.getItem('ls_product') || '[]';
    var oldItem = JSON.parse(pro);  // []

    var selectedItemArray = oldItem.filter(x => x.Id == id);
    if (selectedItemArray.length <= 0) {
        alert('Product Item Not Found')
        return;
    }

    var selectedItem = selectedItemArray[0];
    selectedItem.Quantity = newQuantity;
    selectedItem.Total = +newQuantity * +selectedItem.UnitPrice


    localStorage.setItem('ls_product', JSON.stringify(oldItem));
    loadItems();
});