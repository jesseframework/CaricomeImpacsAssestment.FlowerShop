using System;
using System.Collections.Generic;
using System.Text;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class CreateUpdateOrderDetailTempMin
    {
        public string OrderNo { get; set; }       
        public Guid ItemId { get; set; }        
        public double Quantity { get; set; }
        
    }
}
