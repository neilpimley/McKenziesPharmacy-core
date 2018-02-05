using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Doctor
    {
        public Guid DoctorId { get; set; }
        public Guid PracticeId { get; set; }
        public Guid TitleId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public Practice Practice { get; set; }
        public Title Title { get; set; }
    }
}
