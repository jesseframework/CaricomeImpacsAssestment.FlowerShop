using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class CountryAppService : CrudAppService<
        Country,
        CountryDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCountryDto>,
        ICountryAppService
    {
        private readonly IRepository<Country, Guid> _countryRepository;
        public CountryAppService(
            IRepository<Country, Guid> countryRepository) : base(countryRepository)
        {
            _countryRepository = countryRepository;
        }
    }
}
