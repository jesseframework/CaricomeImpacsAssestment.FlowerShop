$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/dateUpdateHub").build();

    connection.start().then(function () {
        
        connection.invoke("SubscribeToUpdates");

        connection.on("ReceiveDateUpdate", function (message) {
            
            console.log("message:", message);

            $('[data-product-id="' + message.id + '"]').find('.product-name').text(message.name);
        });

    }).catch(function (err) {
        return console.error(err.toString());
    });

});