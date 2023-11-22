using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Product.item
{
    public class IndexModel : PageModel
    {
        private readonly IItemAppService _itemAppService;
        public IndexModel(IItemAppService itemAppService)
        {
            _itemAppService = itemAppService;
        }

        public List<ItemsAllJoinDto> itemsAllJoinDtos = new List<ItemsAllJoinDto>();
        public async Task OnGetAsync()
        {
            itemsAllJoinDtos = await _itemAppService.GetAllItems();
        }
    }
}
