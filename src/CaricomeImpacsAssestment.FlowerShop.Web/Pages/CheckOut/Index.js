$('#postCart').click(function (e) {
    e.preventDefault();

    var tenderType = $('#tenderType').val();
    var paymentNo = $('#paymentNo').val();
    var baseUrl = `${window.location.protocol}//${window.location.host}`;
    var url = `${baseUrl}/api/app/complete-and-check-out/order-confirmation?tenderType=${encodeURIComponent(tenderType)}&paymentNo=${encodeURIComponent(paymentNo)}`;

    abp.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({

        }),
        success: function (response) {
            if (response.statusCode === 800) {
                abp.notify.success(response.statusMessage || 'Order completed successfully.');
                window.location.href = '/Order';
            } else {
                abp.notify.warn(response.statusMessage || 'Something went wrong with the order!');
            }
        },
        error: function (e) {
            abp.notify.error('An error occurred: ' + e.statusText);
        }
    });
});
