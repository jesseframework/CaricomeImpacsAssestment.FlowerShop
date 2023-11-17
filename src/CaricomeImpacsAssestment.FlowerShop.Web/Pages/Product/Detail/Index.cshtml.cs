using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Product.Detail
{
    public class IndexModel : PageModel
    {
        private readonly IItemAppService _itemAppService;
        public IndexModel(IItemAppService itemAppService)
        {
            _itemAppService = itemAppService;
        }
        public List<ItemsAllJoinDto> ItemsList = new List<ItemsAllJoinDto>();

        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            ProductId = id;
            //ItemsList = await _itemAppService.GetItems(id);
        }
    }
}
