using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace CaricomeImpacsAssestment.FlowerShop.Pages;

public class Index_Tests : FlowerShopWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
