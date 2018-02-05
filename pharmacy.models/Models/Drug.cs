using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Drug
    {
        public Guid DrugId { get; set; }
        public string DrugName { get; set; }
        public string DrugDose { get; set; }
        public int PackSize { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
