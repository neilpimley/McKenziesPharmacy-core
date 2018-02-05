using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class CollectScript
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public int OrderStatus { get; set; }
        public int NumItems { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ShopId { get; set; }
        public string Notes { get; set; }
    }
}
