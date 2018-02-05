using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using System.Text;
using Microsoft.Extensions.Options;
using NLog;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EmailService(IOptions<ServiceSettings> serviceSettings)
        {
            _apiKey = serviceSettings.Value.SendGridApiKey;
        }

        public async Task SendRegisterConfirmation(CustomerPoco customer)
        {
            logger.Info("SendRegisterConfirmation - email: {0}", customer.Email);
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("prescriptions@mckenziespharmacy.com", "McKenzies Pharmacy");
            var subject = "Registration Confirmation - McKenzies Pharmacy";
            var to = new EmailAddress(customer.Email, customer.Fullname);
            var plainTextContent = "not implemented";
            var htmlContent = new StringBuilder();
            htmlContent.AppendLine("<h2>Registration Confirmation</h2>");
            htmlContent.AppendFormat("<h4>{0}</h4>", customer.Fullname);
            htmlContent.AppendLine("<p>").Append(customer.Address.AddressLine1);
            htmlContent.AppendLine(customer.Address.AddressLine2);
            htmlContent.AppendLine(customer.Address.AddressLine3);
            htmlContent.AppendLine(customer.Address.Postcode).Append("</p>");
            htmlContent.AppendFormat("<p>Mob: {0} </p>", customer.Mobile);
            htmlContent.AppendFormat("<p>Doctor: Dr. {0},", customer.Doctor.Surname);
            htmlContent.AppendFormat("Shop: {0} </p>", customer.Shop.ShopName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent.ToString());
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendPersonalDetailsAmended(CustomerPoco customer)
        {
            logger.Info("SendPersonalDetailsAmended - email: {0}", customer.Email);
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("prescriptions@mckenziespharmacy.com", "McKenzies Pharmacy");
            var subject = "Registration Confirmation - McKenzies Pharmacy";
            var to = new EmailAddress(customer.Email, customer.Fullname);
            var plainTextContent = "not implemented";
            var htmlContent = new StringBuilder();
            htmlContent.AppendLine("<h2>Ameneded Details Confirmation</h2>");
            htmlContent.AppendFormat("<h4>{0}</h4>", customer.Fullname);
            htmlContent.AppendLine("<p>").Append(customer.Address.AddressLine1);
            htmlContent.AppendLine(customer.Address.AddressLine2);
            htmlContent.AppendLine(customer.Address.AddressLine3);
            htmlContent.AppendLine(customer.Address.Postcode).Append("</p>");
            htmlContent.AppendFormat("<p>Mob: {0} </p>", customer.Mobile);
            htmlContent.AppendFormat("<p>Doctor: Dr. {0},", customer.Doctor.Surname);
            htmlContent.AppendFormat("Shop: {0} </p>", customer.Shop.ShopName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent.ToString());
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendOrderConfirmation(OrderPoco order)
        {
            logger.Info("SendOrderConfirmation - email: {0}", order.Customer.Email);
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("prescriptions@mckenziespharmacy.com", "McKenzies Pharmacy - " + order.Customer.Shop.ShopName);
            var subject = "Order Confirmation - McKenzies Pharmacy - " + order.Customer.Shop.ShopName;
            var to = new EmailAddress(order.Customer.Email, order.Customer.Fullname);
            var plainTextContent = "not implemented";
            var lines = new StringBuilder();
            lines.AppendFormat("<p>Prescriptions for the following items will be requested from Dr. {0} abd delivered by {1}", order.Customer.Doctor.Surname, order.Customer.Shop.ShopName);
            lines.Append("<ul>");
            foreach (var item in order.Items)
            {
                lines.AppendFormat("<li>{0} x {1}</li>", item.DrugName, item.Qty);
            }
            lines.Append("</ul>");
            var htmlContent = lines.ToString();
          
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        public async Task SendOrderReadyToColect(OrderPoco order)
        {
            logger.Info("SendOrderReadyToColect - email: {0}", order.Customer.Email);
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("prescriptions@mckenziespharmacy.com", "McKenzies Pharmacy - " + order.Customer.Shop.ShopName);
            var subject = "Order Ready - McKenzies Pharmacy - " + order.Customer.Shop.ShopName;
            var to = new EmailAddress(order.Customer.Email, order.Customer.Fullname);
            var plainTextContent = "not implemented";
            var lines = new StringBuilder();
            lines.AppendFormat("<p>Prescriptions for the following items will be requested from Dr. {0} abd delivered by {1}", order.Customer.Doctor.Surname, order.Customer.Shop.ShopName);
            lines.Append("<ul>");
            foreach (var item in order.Items)
            {
                lines.AppendFormat("<li>{0} x {1}</li>", item.DrugName, item.Qty);
            }
            lines.Append("</ul>");
            var htmlContent = lines.ToString();

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendOrderReminder(ReminderPoco reminder)
        {
            logger.Info("SendOrderReminder - email: {0}", reminder.Email);
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("prescriptions@mckenziespharmacy.com", "McKenzies Pharmacy");
            var subject = "Reminder";
            var to = new EmailAddress(reminder.Email, reminder.Email);
            var plainTextContent = "not implemented";
            var lines = new StringBuilder();
            // TODO: add content to email

            var htmlContent = lines.ToString();

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}