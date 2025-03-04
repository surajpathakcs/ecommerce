$(document).on('click', '.btnKhalti', function () {
    var amount = parseFloat($('.grandTotal').data('amount'));
    console.log("Amount from Data Attribute:", amount);
    var ok = confirm('Are you sure to pay via Khalti?')
    if (ok) {
        payload = {
            Amount: amount,
            RedirectUrl: 'http://localhost:5230',
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

