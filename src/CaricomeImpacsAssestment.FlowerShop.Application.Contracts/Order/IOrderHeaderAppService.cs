using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public interface IOrderHeaderAppService : ICrudAppService<
                                        OrderHeaderDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateOrderHeaderDto>
    {
        Task<ResponseStatusCodesDto> CreateCompleteOrderAsync(CreateUpdateOrderHeaderDtoMin input);
        Task<ResponseStatusCodesDto> CreateOrderConfirmation(string tenderType, string paymentNo);
        Task<OrderHeaderDto> GetOrderConfimTotalByCookieId(Guid cookieId);
        Task<List<OrderTrackingDto>> GetAllOrderWithCustomer();
        Task<List<OrderWithItemDataDto>> GetOrderAllOrderSum();
        Task<OrderHeaderDto> GetOrderHeaderByOrderNo(string orderNo);
        Task<List<OrderWithItemDataDto>> GetOrderAllOrderWithItems(Guid Id);
    }
}
