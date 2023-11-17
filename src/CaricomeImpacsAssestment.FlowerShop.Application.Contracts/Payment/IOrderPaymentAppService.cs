using CaricomeImpacsAssestment.FlowerShop.Payment.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Payment
{
    public interface IOrderPaymentAppService : ICrudAppService<
                                        OrderPaymentDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateOrderPaymentDto>
    {
    }
}
