using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class CategoryAppService : CrudAppService<
        Category,
        CategoryDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto>,
        ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        public CategoryAppService(
            IRepository<Category, Guid> categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public override Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
        public async Task<List<CategoryDto>> GetAllCategoryWithImages()
        {
            var categoryData = await _categoryRepository.GetListAsync();

            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(categoryData);
        }

    }
}
