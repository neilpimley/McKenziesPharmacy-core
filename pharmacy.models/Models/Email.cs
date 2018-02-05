using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Email
    {
        public Guid EmailId { get; set; }
        public Guid CustomerId { get; set; }
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string CcAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Sent { get; set; }
    }
}
