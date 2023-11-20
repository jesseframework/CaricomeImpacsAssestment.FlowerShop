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
                window.location.reload();
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
                window.location.reload(); 
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



});