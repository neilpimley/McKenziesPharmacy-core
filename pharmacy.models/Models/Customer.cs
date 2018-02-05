using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Customer
    {
        public Guid CustomerId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public Guid TitleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public string Home { get; set; }
        public DateTime Dob { get; set; }
        public Guid AddressId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ShopId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
    }
}
