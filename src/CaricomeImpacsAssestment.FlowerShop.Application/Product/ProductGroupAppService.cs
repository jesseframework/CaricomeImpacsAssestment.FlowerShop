using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class ProductGroupAppService : CrudAppService<
        ProductGroup,
        ProductGroupDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductGroupDto>,
        IProductGroupAppService
    {
        private readonly IRepository<ProductGroup, Guid> _productGroupRepository;
        public ProductGroupAppService(IRepository<ProductGroup, Guid> productGroupRepository)
            : base(productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;

        }
    }
}
