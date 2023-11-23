$(document).ready(function () {
    
    
    var baseUrl = `${window.location.protocol}//${window.location.host}`;
    function showFullDescription() {
        document.querySelector('.short-description').style.display = 'none';
        document.querySelector('.full-description').style.display = 'block';
    }

    function completeOrder() {
       
        var yourCouponCode = $('#cart-checkout').data('yourCouponCode'); 

        var orderData = {
            yourCouponCode: yourCouponCode
        };

        abp.ajax({
            url: baseUrl + '/api/app/complete-and-check-out/complete-order',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(orderData),
            success: function (response) {
                abp.notify.success('Cart uploaded successfully.');                
                window.location.href = baseUrl + '/checkout'; 
            },
            error: function (e) {
                abp.notify.error('An error occurred: ' + e.statusText);
            }
        });
    }

    function updateCartItem(updatecartbutton) {
        var itemId = updatecartbutton.attr('id').split('_')[1];
        var quantity = $('#quantity_' + itemId).val();
        var updateUrl = baseUrl + '/api/app/add-to-shopping-card/shopping-cart/' + itemId + '?quantity=' + quantity;

        abp.ajax({
            url: updateUrl,
            type: 'PUT',
            success: function (response) {
                abp.notify.success('Item updated successfully');
                                
            },
            error: function (err) {
                abp.notify.error('Error updating item: ' + err.statusText);
            }
        });
    }

    $('.update-cart').click(function () {        
        
        updateCartItem($(this));
    });
    
    $('.itemRemove').click(function (e) {
        e.preventDefault(); 

        var itemId = $(this).attr('id').split('_')[1];
       
        var deleteUrl = '/api/app/add-to-shopping-card/' + itemId;

        abp.ajax({
            url: deleteUrl,
            type: 'DELETE',
            success: function (response) {
                abp.notify.success('Item removed successfully');
                
                
            },
            error: function (err) {
                abp.notify.error('Error removing item: ' + err.statusText);
            }
        });
    });

    $('#cart-checkout').click(function (e) {
        e.preventDefault();
        completeOrder();
    });

    
    //const connection = new signalR.HubConnectionBuilder()
    //    .withUrl("/cartHub")
    //    .configureLogging(signalR.LogLevel.Information)
    //    .build();

    //connection.start().catch(err => console.error(err.toString()));

    //// This function is triggered when the cart is updated
    //connection.on("ReceiveCartUpdate", (shoppingCartUpdate) => {
    //    // Assuming shoppingCartUpdate contains quantity and lineTotal
    //    updateCartUI(shoppingCartUpdate.quantity, shoppingCartUpdate.lineTotal);
    //});

    //function updateCartUI(quantity, lineTotal) {
    //    $('#itemCountBadge').text(quantity);
    //    $('#totalCostBadge').text('$' + lineTotal.toFixed(2));
    //}

    // //Optional: Reconnect logic in case the connection drops
    //    connection.onclose(() => {
    //        console.log("SignalR disconnected. Attempting to reconnect...");
    //        setTimeout(() => connection.start(), 5000); // Retry after 5 seconds
    //    });

});

//(function () {
//    const connection = new signalR.HubConnectionBuilder()
//        .withUrl("/cartHub")
//        .configureLogging(signalR.LogLevel.Information)
//        .build();

//    connection.start().catch(err => console.error('SignalR Connection Error:', err.toString()));

//    connection.on("ReceiveCartUpdate", (shoppingCartUpdate) => {
//        updateCartUI(shoppingCartUpdate.quantity, shoppingCartUpdate.lineTotal);
//    });

//    function updateCartUI(quantity, lineTotal) {
//        $('#itemCountBadge').text(quantity);
//        $('#totalCostBadge').text('$' + lineTotal.toFixed(2));
//    }

//    // Optional: Reconnect logic in case the connection drops
//    connection.onclose(() => {
//        console.log("SignalR disconnected. Attempting to reconnect...");
//        setTimeout(() => connection.start(), 5000); // Retry after 5 seconds
//    });
//})();

