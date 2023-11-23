using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web
{
    public class CartHub : Hub
    {
        public async Task UpdateCart(OrderDetailTempDto orderDetailTempDto)
        {
            await Clients.All.SendAsync("ReceiveCartUpdate", orderDetailTempDto);
        }
    }
}
