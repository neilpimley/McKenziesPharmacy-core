using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Practice
    {
        public Guid PracticeId { get; set; }
        public string PracticeName { get; set; }
        public Guid AddressId { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
