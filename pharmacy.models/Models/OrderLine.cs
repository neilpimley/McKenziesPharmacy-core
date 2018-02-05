using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class OrderLine
    {
        public Guid OrderLineId { get; set; }
        public Guid OrderId { get; set; }
        public Guid DrugId { get; set; }
        public int Qty { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? OrderLineStatus { get; set; }
    }
}
