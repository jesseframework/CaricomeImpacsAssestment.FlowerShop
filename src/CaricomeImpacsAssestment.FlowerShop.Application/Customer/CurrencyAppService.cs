using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class CurrencyAppService : CrudAppService<
        Currency,
        CurrencyDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCurrencyDto>,
        ICurrencyAppService
    {
        private readonly IRepository<Currency, Guid> _currencyRepository;
        public CurrencyAppService(
            IRepository<Currency, Guid> currencyRepository) : base(currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
    }
}
