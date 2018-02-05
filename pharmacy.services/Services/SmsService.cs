using Pharmacy.Services.Interfaces;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Pharmacy.Models;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services
{
    public class SmsService : ISmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public SmsService(IOptions<ServiceSettings> serviceSettings)
        {
            _accountSid = serviceSettings.Value.TwilloAccountSid;
            _authToken = serviceSettings.Value.TwilloAuthToken;
            _fromNumber = serviceSettings.Value.TwilloNumber;
        }

        public Task SendRegisterConfirmation(CustomerPoco customer)
        {
            logger.Info("SendRegisterConfirmation - Customer {0}", JsonConvert.SerializeObject(customer));
            TwilioClient.Init(_accountSid, _authToken);

            return MessageResource.CreateAsync(
                    from: new PhoneNumber(_fromNumber), 
                    to: new PhoneNumber(customer.Mobile), 
                    body: $"Hey {customer.Fullname} registration confirmed");
        }

        public Task SendPersonalDetailsAmended(CustomerPoco customer)
        {
            throw new NotImplementedException();
        }

        public Task SendOrderConfirmation(OrderPoco order)
        {
            throw new NotImplementedException();
        }

        public Task SendOrderReadyToColect(OrderPoco order)
        {
            throw new NotImplementedException();
        }

        public Task SendOrderReminder(ReminderPoco reminder)
        {
            throw new NotImplementedException();
        }

        



    }
}
