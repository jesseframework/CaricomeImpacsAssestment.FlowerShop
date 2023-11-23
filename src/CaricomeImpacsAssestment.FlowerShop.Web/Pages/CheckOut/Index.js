$('#postCart').click(function (e) {
    e.preventDefault();
    abp.ui.block({ busy: true });
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
                abp.ui.unblock();
                window.location.href = '/Order';
            } else {
                abp.notify.warn(response.statusMessage || 'Something went wrong with the order!');
                abp.ui.unblock();
            }
        },
        error: function (e) {
            abp.notify.error('An error occurred: ' + e.statusText);
        }
    });
});

//$(document).ready(function () {

//    (function () {
//        const connection = new signalR.HubConnectionBuilder()
//            .withUrl("/cartHub")
//            .configureLogging(signalR.LogLevel.Information)
//            .build();

//        connection.start().catch(err => console.error('SignalR Connection Error:', err.toString()));

//        connection.on("ReceiveCartUpdate", (shoppingCartUpdate) => {
//            updateCartUI(shoppingCartUpdate.quantity, shoppingCartUpdate.lineTotal);
//        });

//        function updateCartUI(quantity, lineTotal) {
//            $('#itemCountBadge').text(quantity);
//            $('#totalCostBadge').text('$' + lineTotal.toFixed(2));
//        }

//        // Optional: Reconnect logic in case the connection drops
//        connection.onclose(() => {
//            console.log("SignalR disconnected. Attempting to reconnect...");
//            setTimeout(() => connection.start(), 5000); // Retry after 5 seconds
//        });
//    })();
//});

