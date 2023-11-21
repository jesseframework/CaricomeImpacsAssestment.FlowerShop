using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class OrderWithItemDataDto
    {
        public string ItemNo { get; set; }
        public string Name { get; set; }
        public Guid ItemId { get; set; }
        public Guid DetailId { get; set; }
        public string ItemName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string IconUrl { get; set; }
        public string Group {  get; set; }        
        public double UnitPrice { get; set; }
        public double LineTotal { get; set; }
        public double SubTotal { get; set; }
        public double Shipment { get; set; }
        public double Quantity { get; set; }
        public double TaxAmount { get; set; }
        public double TotalDue { get; set; }
        public string OrderNo { get; set;}
        public string Status { get; set; }
        public string Shipping { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }        
        public double TotalDiscount { get; set; }
        public DateTime OrderDate { get; set; }

        
    }
}
