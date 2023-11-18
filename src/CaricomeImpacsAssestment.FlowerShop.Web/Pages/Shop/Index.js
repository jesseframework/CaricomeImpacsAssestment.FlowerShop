$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/dateUpdateHub").build();   
    connection.on("ReceiveDateUpdate", function (updatedDateMessage) {      
        
        $('#DateUpdateContainer').text(updatedDateMessage);
    });
   
    connection.start().catch(function (err) {
        return console.error(err.toString());
    });
});





