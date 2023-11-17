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
        Task<ResponseStatusCodesDto> CreateShoppingCart(CreateUpdateOrderDetailTempDto input);
    }
}
