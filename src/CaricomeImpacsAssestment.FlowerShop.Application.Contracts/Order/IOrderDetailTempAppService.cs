using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public interface IOrderDetailTempAppService : ICrudAppService<
                                        OrderDetailTempDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateOrderDetailTempDto>
    {
        Task<ResponseStatusCodesDto> CreateShoppingCart(CreateUpdateOrderDetailTempMin input);
        Task<List<OrderDetailTempDto>> GetShoppingCartByCookieId(Guid cookieId);
        Task<OrderDetailTempDto> GetShoppingCartTotalByCookieId(Guid cookieId);
        Task<OrderDetailTempDto> GetShoppingCartAmountByCookieId(Guid cookieId);
        Task<List<OrderWithItemDataDto>> GetShoppingCartForCheckOut(Guid cookieId);
    }
}
