using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Shop
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Guid AddressId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
