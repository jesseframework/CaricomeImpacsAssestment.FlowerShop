let fshop = (function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/orderDetailsUpdateHub").build();
    connection.start().then(function () {
        connection.invoke("SubscribeToUpdates");
        connection.on("ReceiveDateUpdate", function (entitytype,entityData) {
            console.log("Entity Data Received:", entityData);

            switch (entityData.type) {
                case 'Item':
                    updateItem(entityData);
                    break;

                case 'OrderHeader':
                case 'OrderDetail':
                case 'CustomerAccount':
                    var startDate = '2023-11-01';
                    var endDate = '2023-11-30';
                    fetchSalesData(startDate, endDate);
                    fetchDashBoard(startDate, endDate);
                    break;
                
                default:
                    console.log("Unknown entity type:", entityData.type);
            }
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });

    function updateItem(itemData) {
        $('[data-item-id="' + itemData.id + '"]').find('.item-name').text(itemData.name);
    }

    var fetchDashBoard = function (startDate, endDate) {
        var endpoint = `${baseUrl()}/api/app/sales-reporting/dashboard-stats`;
        var fullurl = endpoint + '?StartDateTime=' + startDate + '&EndDateTime=' + endDate;

        return $.ajax({
            url: fullurl,
            method: 'GET',
            success: function (apiResponse) {
                console.log("Dashboard apiResponse", apiResponse);
                renderCardData(apiResponse);
            },
            error: function (xhr, status, error) {
                console.error('Request failed with status:', status);
            }
        });
    }

    var fetchSalesData = function (startDate, endDate) {

        var endpoint = `${baseUrl()}/api/app/sales-reporting/category-counts`;
        var fullurl = endpoint + '?StartDateTime=' + startDate + '&EndDateTime=' + endDate;

        return $.ajax({
            url: fullurl,
            method: 'GET',
            success: function (apiResponse) {
                console.log("apiResponse", apiResponse);
                const formattedData = formatChartData(apiResponse);
                renderBarChart(formattedData);
            },
            error: function (xhr, status, error) {
                console.error('Request failed with status:', status);
            }
        });
    }

    var baseUrl = function () {
        var protocol = window.location.protocol;
        var host = window.location.host;
        var baseUrl = protocol + '//' + host;

        return baseUrl;
    }

    var formatChartData = function (apiResponse) {
        const labels = [];
        const dataValues = [];

        apiResponse.forEach(item => {
            labels.push(item.categoryName);
            dataValues.push(item.count);
        });

        const data = {
            labels: labels,
            datasets: [{
                label: 'My First Dataset',
                data: dataValues,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(255, 205, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                ],
                borderColor: [
                    'rgb(255, 99, 132)',
                    'rgb(255, 159, 64)',
                    'rgb(255, 205, 86)',
                    'rgb(75, 192, 192)',
                    'rgb(54, 162, 235)',
                    'rgb(153, 102, 255)',
                ],
                borderWidth: 1
            }]
        };

        return data; // Return the Chart.js data structure
    }

    var renderBarChart = function (data) {
        const ctx = document.getElementById('BarChart').getContext('2d');
        const config = {
            type: 'bar',
            data: data,
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            },
        };
        const myChart = new Chart(ctx, config);
    }

    var renderCardData = function (apiResponse) {
        $('#total-order').text(apiResponse.totalOrders);
        $('#total-revenue').text(apiResponse.totalOrderAmount);
        $('#total-customers').text(apiResponse.totalCustomers);
    }

    return {
        fetchSalesData : fetchSalesData,
        fetchDashBoard : fetchDashBoard
    };
})();