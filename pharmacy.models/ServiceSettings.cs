using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Models
{
    public class ServiceSettings
    {
        public string SendGridApiKey { get; set; }

        public string TwilloAccountSid { get; set; }
        public string TwilloAuthToken { get; set; }
        public string TwilloNumber { get; set; }

        public string GetAddressApiKey { get; set; }

        public string AllowedPostcodes { get; set; }

        public string Auth0Domain { get; set; }
        public string Auth0ClientId { get; set; }
        public string Auth0ApiIdentifier { get; set; }
        public string Auth0ClientSecret { get; set; }
        public string Auth0Audiencet { get; set; }
    }
}
