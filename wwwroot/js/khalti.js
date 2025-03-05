$(document).on('click', '.btnKhalti', function () {
    var amount = parseFloat($('.grandTotal').data('amount'));

    var fullName = $('.fullName').text().trim(); // Get name from the table
    var email = $('.email').text().trim(); // Get email from the table
    var address = $('.address').text().trim() || "N/A"; // Get address, default if empty
    var purchase_order_id = $('.ProductOrderMID').val();

    var ok = confirm('Are you sure to pay via Khalti?')
    if (ok) {
        payload = {
            Amount: amount,
            RedirectUrl: 'http://localhost:5230',
            FullName: fullName,
            Email: email,
            Address: address,
            PurchaseOrderId: purchase_order_id
        };

        $.ajax({
            method: 'post',
            contentType: "application/json; charset=utf-8",
            url: "/KhaltiPaymentInitiate",
            data: JSON.stringify(payload),
            success: function (rsp) {
                if (rsp.success) {
                    var khalti = JSON.parse(rsp.data);
                    debugger;
                    window.location.href = khalti.payment_url;
                } else {
                    alert(rsp.message)
                }
            },
            error: function (er) {

            },
        })
    }
})

