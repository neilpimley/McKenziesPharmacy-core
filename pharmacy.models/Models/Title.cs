using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Title
    {
        public Guid TitleId { get; set; }
        public string TitleName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
