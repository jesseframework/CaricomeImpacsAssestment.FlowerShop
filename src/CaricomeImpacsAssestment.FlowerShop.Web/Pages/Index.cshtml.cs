using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages;

public class IndexModel : FlowerShopPageModel
{
    private readonly ICategoryAppService _categoryAppService;
    public IndexModel(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }
    public List<CategoryDto> CategoryList = new List<CategoryDto>();

    public async Task OnGetAsync()
    {
        CategoryList = await _categoryAppService.GetAllCategoryWithImages();
    }
}
