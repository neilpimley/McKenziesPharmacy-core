using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class OrderStatus
    {
        public Guid OrderStatusId { get; set; }
        public Guid? UserId { get; set; }
        public Guid OrderId { get; set; }
        public int Status { get; set; }
        public DateTime StatusSetDate { get; set; }
        public int? OrderLineStatus { get; set; }
    }
}
