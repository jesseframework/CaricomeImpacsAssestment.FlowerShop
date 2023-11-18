using CaricomeImpacsAssestment.FlowerShop.Payment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Payment
{
    public class AddCouponAppService : CrudAppService<
        Coupon,
        CouponDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCouponDto>,
        ICouponAppService
    {
        private readonly IRepository<Coupon, Guid> _couponRepository;
        public AddCouponAppService(
            IRepository<Coupon, Guid> couponRepository
            ) : base(couponRepository)
        {
            _couponRepository = couponRepository;
        }
    }
}
