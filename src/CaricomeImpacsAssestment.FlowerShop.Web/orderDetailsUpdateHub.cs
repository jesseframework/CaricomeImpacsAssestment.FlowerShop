using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Order;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace CaricomeImpacsAssestment.FlowerShop.Web
{
    //[HubRoute("/orderDetailsUpdateHub")]
    public class orderDetailsUpdateHub:AbpHub, 
        ILocalEventHandler<EntityChangedEventData<OrderDetail>>, 
        ITransientDependency
    {
        private readonly IHubContext<orderDetailsUpdateHub> _hubContext;

        public orderDetailsUpdateHub(IHubContext<orderDetailsUpdateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SubscribeToUpdates()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "updates");
        }

        public async Task DashBoardUpdate(EntityChangedEventData<OrderDetail> customerAccountData)
        {
            await _hubContext.Clients.All
                .SendAsync("ReceiveDashboardUpdate", typeof(OrderDetail), customerAccountData);
        }

        public async Task HandleEventAsync(EntityChangedEventData<OrderDetail> eventData)
        {
            await DashBoardUpdate(eventData);
        }
    }
}
