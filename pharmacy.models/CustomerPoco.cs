using System;

namespace Pharmacy.Models.Pocos
{
    public class CustomerPoco : Customer
    {
        public Title Title { get; set; }

        public string Fullname {
            get
            {
                return Firstname + " " + Lastname;
            }
        }
        public Doctor Doctor { get; set; }
        public Shop Shop { get; set; }
        public Address Address { get; set; }
        public Guid PracticeId { get; set;  }

        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}