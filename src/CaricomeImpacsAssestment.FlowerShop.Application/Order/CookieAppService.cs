using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
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
        //public async Task GetByCookieUUID(string cookie)
        //{
        //    var cookieData = await _cookieTrackerRepository.GetAsync(p=>p.Value.Equals(cookie));
        //    if (cookieData != null)
        //    {

        //    }
        //}
    }
}
