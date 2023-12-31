﻿using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class CookieAppService : CrudAppService<
        CookieTracker,
        CookieTrackerDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCookieTrackerDto>,
        ICookieTrackerAppService
    {
        private readonly IRepository<CookieTracker, Guid> _cookieTrackerRepository;
        public CookieAppService(
            IRepository<CookieTracker, Guid> cookieTrackerRepository) : base(cookieTrackerRepository)
        {
            _cookieTrackerRepository = cookieTrackerRepository;
        }
        public async Task<Guid> GetByCookieUUID(string cookieId)
        {
           
            Guid _cookieFromDb = Guid.Empty;
            var cookieData = await _cookieTrackerRepository.GetListAsync(p => p.Value.Equals(cookieId));
            if (cookieData.Any())
            {
                
                _cookieFromDb = cookieData[0].Id;
            }

            return _cookieFromDb;


        }
    }
}
