using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order.Manager;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.RealTime
{
    public class CartHub : Hub
    {
        
        
        private readonly IHubContext<CartHub> _cartHubContext;
        public CartHub(
           
            IHubContext<CartHub> cartHubContext) 
        {
           
           
            _cartHubContext = cartHubContext;

        }
        public async Task UpdateCart(shoppingCartUpdateDto shoppingCartUpdateDto)
        {
            
            
            await _cartHubContext.Clients.All.SendAsync("ReceiveCartUpdate", shoppingCartUpdateDto);
        }



    }
}
