using CaricomeImpacsAssestment.FlowerShop.Payment.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Payment
{
    public interface ICouponAppService : ICrudAppService<
                                        CouponDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateCouponDto>
    {
    }
}
