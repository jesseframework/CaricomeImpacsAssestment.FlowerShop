﻿using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public interface ICookieTrackerAppService : ICrudAppService<
                                        CookieTrackerDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateCookieTrackerDto>
    {
        Task<Guid> GetByCookieUUID(string cookieId);
    }
}
