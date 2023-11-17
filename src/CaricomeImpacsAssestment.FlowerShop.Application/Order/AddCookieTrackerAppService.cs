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
    public class AddCookieTrackerAppService : CrudAppService<
        CookieTracker,
        CookieTrackerDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCookieTrackerDto>,
        ICookieTrackerAppService
    {
        private readonly IRepository<CookieTracker, Guid> _cookietrackerRepository;
        public AddCookieTrackerAppService(
            IRepository<CookieTracker, Guid> cookietrackerRepository) : base(cookietrackerRepository) 
        {
            _cookietrackerRepository = cookietrackerRepository;
        }

        
    }
}
