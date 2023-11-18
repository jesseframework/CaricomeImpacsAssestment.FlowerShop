using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Product;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace CaricomeImpacsAssestment.FlowerShop.Web
{
    public class DateUpdateHub : AbpHub
    {
        private readonly IItemAppService _itemAppService;
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;
        public DateUpdateHub(IItemAppService itemAppService,
            IOrderDetailTempAppService orderDetailTempAppService)
        {
            _itemAppService = itemAppService;
            _orderDetailTempAppService = orderDetailTempAppService;
        }

        public async Task BroadcastDateUpdate(DateTime updatedDate)
        {
            var message = $"Date updated to: {updatedDate}";

            // This will send the message to all connected clients
            await Clients.All.SendAsync("ReceiveDateUpdate", message);
        }

    }
}
