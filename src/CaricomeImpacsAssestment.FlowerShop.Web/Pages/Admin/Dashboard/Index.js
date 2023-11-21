$(function () {
    abp.log.debug('Dashboard Index.js initialized!');
    var connection = new signalR.HubConnectionBuilder().withUrl("/orderDetailsUpdateHub").build();

    connection.start().then(function () {
        connection.invoke("SubscribeToUpdates");
        connection.on("ReceiveDashboardUpdate", function (entitytype, entityData) {
            console.log("Entity Data Received:", entityData);
            runDashBoard();
        });

    }).catch(function (err) {
        return console.error(err.toString());
    });

    var runDashBoard = function () {
        var startDate = '2023-11-01';
        var endDate = '2023-11-30';
        fshop.fetchSalesData(startDate, endDate);
        fshop.fetchDashBoard(startDate, endDate);
    }

    runDashBoard();
});

