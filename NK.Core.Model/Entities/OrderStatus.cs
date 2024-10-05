﻿using NK.Core.Model.Enums;

namespace NK.Core.Model.Entities
{
    public class OrderStatus
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; } = string.Empty;
        public StatusOrder Status { get; set; }
        public DateTime Time { get; set; }
        public string? Note { get; set; }

        public Order? Order { get; set; }
    }
}
