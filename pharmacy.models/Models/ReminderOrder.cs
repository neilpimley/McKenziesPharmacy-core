using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class ReminderOrder
    {
        public Guid ReminderOrderId { get; set; }
        public Guid ReminderId { get; set; }
        public Guid OrderId { get; set; }
    }
}
