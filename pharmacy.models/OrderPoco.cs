using System.Collections.Generic;

namespace Pharmacy.Models.Pocos
{
    public class OrderPoco : Order
    {
        public CustomerPoco Customer { get; set; }
        public IEnumerable<DrugPoco> Items { get; set; }
        public bool SmsReminder { get; set; }
        public bool EmailReminder { get; set; }

    }
}
