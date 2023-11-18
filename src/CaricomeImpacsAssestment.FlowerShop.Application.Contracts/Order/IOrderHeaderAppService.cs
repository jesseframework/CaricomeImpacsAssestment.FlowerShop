using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using System;
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
    }
}
