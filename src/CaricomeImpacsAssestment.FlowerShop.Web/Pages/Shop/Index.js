$(function () {
    //var connection = new signalR.HubConnectionBuilder().withUrl("/dateUpdateHub").build();

    ////connection.start().then(function () {

    ////    connection.invoke("SubscribeToUpdates");

    ////    connection.on("ReceiveDateUpdate", function (message) {

    ////        console.log("message:", message);

    ////        $('[data-product-id="' + message.id + '"]').find('.product-name').text(message.name);
    ////    });

    ////}).catch(function (err) {
    ////    return console.error(err.toString());
    ////});

    //const connection = new signalR.HubConnectionBuilder()
    //    .withUrl("/cartHub")
    //    .configureLogging(signalR.LogLevel.Information)
    //    .build();

    //connection.start().catch(err => console.error(err.toString()));

    //connection.on("ReceiveCartUpdate", (itemCount) => {

    //    updateCartDisplay(itemCount);
    //});

    //function updateCartDisplay(itemCount) {
    //    updateCartAmount();
    //}

    //function updateCartAmount() {
    //    var baseUrl = `${window.location.protocol}//${window.location.host}`;
    //    var cookieId = ck;

    //    var url = baseUrl + '/api/app/add-to-shopping-card/shopping-cart-amount-by-cookie-id/' + cookieId;

    //    fetch(url)
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(data => {

    //            updateCartDisplay(data.quantity, data.lineTotal);
    //        })
    //        .catch(error => {
    //            console.error('Error:', error);
    //        });
    //}

    //function updateCartDisplay(quantity, lineTotal) {
    //    $('#itemCountBadge').text(quantity);
    //    $('#totalCostBadge').text('$' + lineTotal.toFixed(2));
    //}

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

    ////Optional: Reconnect logic in case the connection drops
    //connection.onclose(() => {
    //    console.log("SignalR disconnected. Attempting to reconnect...");
    //    setTimeout(() => connection.start(), 5000); // Retry after 5 seconds
    //});

});