﻿using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using System;
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
    }
}
