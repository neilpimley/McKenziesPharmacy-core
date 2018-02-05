using System;
using System.Collections.Generic;

namespace Pharmacy.Models.Pocos
{
    public class ReminderPoco : Reminder
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid OrderId { get; set; }
        public IEnumerable<DrugPoco> Drugs { get; set; }
    }
}
