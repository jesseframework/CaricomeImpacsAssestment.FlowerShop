﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class CreateUpdateCookieTrackerDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime? Expiry { get; set; }
        public bool IsSecure { get; set; }
        public bool IsHttpOnly { get; set; }
        public Guid OrderId { get; set; }
        public string OrderNo { get; set; }
        public Guid UserId { get; set; }
    }
}
