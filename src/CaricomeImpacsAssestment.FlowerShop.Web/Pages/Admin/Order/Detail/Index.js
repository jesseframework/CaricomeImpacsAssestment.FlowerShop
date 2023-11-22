$(document).ready(function () {
    var baseUrl = `${window.location.protocol}//${window.location.host}`;

    function updateOrder(updatecartbutton) {
        var orderStatus = $('#order-status').val();
        var id = $('#order-header-id').val();

        var url = `${baseUrl}/api/app/complete-and-check-out/order-status/${encodeURIComponent(id)}?orderStatus=${encodeURIComponent(orderStatus)}`;
        debugger
        abp.ajax({
            url: url,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
               
            }),
            success: function (response) {
                abp.notify.success('Order Status Update successfully.');
                window.location.href = '/Admin/Order';
            },
            error: function (error) {
                abp.notify.error('An error occurred while updating order status: ' + error.statusText);
            }
        });
    }

    $('#order-status-butrton').click(function (e) {
        e.preventDefault();
        var button = $(this);
        abp.message.confirm('Are you sure to ship this order?')
            .then(function (confirmed) {
                if (confirmed) {
                    abp.ui.block({ busy: true });
                    updateOrder(button);
                    abp.ui.unblock();
                    window.location.reload();
                }
            });
    });
});
