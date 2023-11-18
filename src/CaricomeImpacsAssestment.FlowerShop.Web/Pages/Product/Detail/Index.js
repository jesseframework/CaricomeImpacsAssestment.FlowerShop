$(document).ready(function () {
    var baseUrl = `${window.location.protocol}//${window.location.host}`;

    $('#addToCartBtn').click(function (e) {
        e.preventDefault();
        var itemId = $(this).data('itemid');
        var quantity = $('#itemQuantity').val();
        var orderNo = ""; 
        abp.ajax({
            url: baseUrl + '/api/app/add-to-shopping-card/shopping-cart',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                orderNo: orderNo,
                itemId: itemId,
                quantity: quantity
            }),
            success: function (response) {
               
                if (response.statusCode === 800) {
                    abp.notify.success(response.statusMessage || 'Item added to cart successfully.');
                    debugger
                    updateCartAmount();
                } else {
                   
                    abp.notify.warn(response.statusMessage || 'Something went wrong!');
                }
            },
            error: function (e) {
                
                abp.notify.error('An error occurred: ' + e.statusText);
            }
        });
    });

    function updateCartAmount() {
        var baseUrl = `${window.location.protocol}//${window.location.host}`;
        var cookieId = ck;
        debugger
        var url = baseUrl + '/api/app/add-to-shopping-card/shopping-cart-amount-by-cookie-id/' + cookieId;
        debugger
        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {

                updateCartDisplay(data.quantity, data.lineTotal);
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }

    function updateCartDisplay(quantity, lineTotal) {
        $('#itemCountBadge').text(quantity);
        $('#totalCostBadge').text('$' + lineTotal.toFixed(2));
    }

    (function () {

        $("#cart").on("click", function () {
            $(".shopping-cart").fadeToggle("fast");
        });

    })();
});
