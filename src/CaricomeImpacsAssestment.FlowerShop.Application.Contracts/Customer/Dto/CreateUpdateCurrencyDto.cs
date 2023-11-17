namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class CreateUpdateCurrencyDto
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
