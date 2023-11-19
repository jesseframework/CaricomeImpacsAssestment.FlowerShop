using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus.Distributed;

namespace CaricomeImpacsAssestment.FlowerShop.Web
{
    public class DateUpdateHub : AbpHub,
        IDistributedEventHandler<EntityChangedEventData<Item>>,
        ITransientDependency
    {
        private readonly IItemAppService _itemAppService;
        private readonly IHubContext<DateUpdateHub> _hubContext;

        public DateUpdateHub(IItemAppService itemAppService, IHubContext<DateUpdateHub> hubContext)
        {
            _itemAppService = itemAppService;
            _hubContext = hubContext;
        }

        public async Task SubscribeToUpdates()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "updates");
        }

        public async Task ItemDateUpdate(ItemDto itemData)
        {
            // This will send the message to all connected clients

            await _hubContext.Clients.All
                .SendAsync("ReceiveDateUpdate", itemData);

        }

        public async Task HandleEventAsync(EntityChangedEventData<Item> eventData)
        {
            var Id = eventData.Entity.Id;            
            var hubItem = await _itemAppService.GetAsync(Id);
            await ItemDateUpdate(hubItem);
        }


    }
}
