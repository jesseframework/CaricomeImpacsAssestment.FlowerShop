using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public interface ICategoryAppService : ICrudAppService<
                                        CategoryDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateCategoryDto>
    {
        Task<List<CategoryDto>> GetAllCategoryWithImages();

    }
}
