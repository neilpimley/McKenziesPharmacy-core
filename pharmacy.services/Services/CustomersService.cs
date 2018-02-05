using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using NLog;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using Microsoft.Extensions.Options;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Newtonsoft.Json;

namespace Pharmacy.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly string[] _allowedPostcodes;
        private static readonly char[] Digits = "0123456789".ToCharArray();
        private readonly string _auth0Domain;
        private readonly string _auth0ClientId;
        private readonly string _authClientSecret;

        public CustomersService(IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper,
            IOptions<ServiceSettings> serviceSettings)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _mapper = mapper;
            _allowedPostcodes = serviceSettings.Value.AllowedPostcodes.Split(",");
            _auth0Domain = serviceSettings.Value.Auth0Domain;
            _auth0ClientId = serviceSettings.Value.Auth0ClientId;
            _authClientSecret = serviceSettings.Value.Auth0ClientSecret;
        }

        public async Task<CustomerPoco> GetCustomerByUsername(string username)
        {
            logger.Info("GetCustomerByUsername - {0}", username);
            var _customers = await _unitOfWork.CustomerRepository.Get(c => c.UserId == username);
            var _customer = _customers.FirstOrDefault();
            if (_customer == null)
            {
                logger.Error("GetCustomerByUsername - User doesn't exist");
                return null;
            }
            var customer = _mapper.Map<CustomerPoco>(_customer);
            customer.Title = await _unitOfWork.TitleRepository.GetByID(_customer.TitleId);
            customer.Address = await _unitOfWork.AddressRepository.GetByID(_customer.AddressId);
            customer.Shop = await _unitOfWork.ShopRepository.GetByID(_customer.ShopId);
            customer.Doctor = await _unitOfWork.DoctorRepository.GetByID(_customer.DoctorId);
            return customer;
        }

        public async Task<CustomerPoco> GetCustomer(Guid id) {
            logger.Info("GetCustomer - {0}", id);
            var _customer = await _unitOfWork.CustomerRepository.GetByID(id);
            if (_customer == null)
            {
                logger.Error("GetCustomerByUsername - User doesn't exist");
                return null;
            }
            var customer = _mapper.Map<CustomerPoco>(_customer);
            customer.Title = await _unitOfWork.TitleRepository.GetByID(_customer.TitleId);
            customer.Address = await _unitOfWork.AddressRepository.GetByID(_customer.AddressId);
            customer.Shop = await _unitOfWork.ShopRepository.GetByID(_customer.ShopId);
            customer.Doctor = await _unitOfWork.DoctorRepository.GetByID(_customer.DoctorId);
            return customer;
        }

        public async Task<CustomerPoco> RegisterCustomer(CustomerPoco customer)
        {
            logger.Info("RegisterCustomer - {0}", customer.Fullname);

            IList<string> registerErrors = await ValidateCustomer(customer);
            if (registerErrors.Count > 0)
            {
                throw new Exception(registerErrors[0]);
            }

            customer.CustomerId = Guid.NewGuid();
            customer.CreatedOn = DateTime.Now;
            customer.AddressId = Guid.NewGuid();
            customer.Address.AddressId = customer.AddressId;
            customer.Address.CreatedOn = DateTime.Now;

            var _customer = _mapper.Map<Customer>(customer);
            _unitOfWork.CustomerRepository.Insert(_customer);
            _unitOfWork.AddressRepository.Insert(customer.Address);
            try
            {
                await _unitOfWork.SaveAsync();
                await UpdateUserMetaData(customer);
                await _emailService.SendRegisterConfirmation(customer);
            }
            catch (Exception ex)
            {
                logger.Error("RegisterCustomer - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
            customer.Title = await _unitOfWork.TitleRepository.GetByID(_customer.TitleId);
            customer.Shop = await _unitOfWork.ShopRepository.GetByID(_customer.ShopId);
            customer.Doctor = await _unitOfWork.DoctorRepository.GetByID(_customer.DoctorId);
            return customer;
        }

        public async Task UpdateCustomerDetails(CustomerPoco customer) {
            logger.Info("UpdateCustomerDetails - {0}", customer.CustomerId);
            var _customer = _mapper.Map<Customer>(customer);
            _unitOfWork.CustomerRepository.Update(_customer);
            _unitOfWork.AddressRepository.Update(customer.Address);
            try
            {
                await _unitOfWork.SaveAsync();
                await _emailService.SendPersonalDetailsAmended(customer);
            }
            catch (Exception ex)
            {
                logger.Error("UpdateCustomerDetails - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task ActivateCustomer(Guid id, string mobileVerificationCode)
        {
            logger.Info("ActivateCustomer - {0}", id);
            var customers = await _unitOfWork.CustomerRepository.Get(x => x.CustomerId == id);
            var customer = customers.FirstOrDefault();
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            //TODO: check mobileVerificationCode against value to be stored in DB

            customer.Active = true;
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logger.Error("ActivateCustomer - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> GetToken()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string payload = JsonConvert.SerializeObject(new
            {
                client_id = _auth0ClientId,
                client_secret = _authClientSecret,
                audience = $"https://{_auth0Domain}/api/v2/",
                grant_type = "client_credentials",
                token_endpoint_auth_method = "client_secret_post"
            });

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var stringTask = client.PostAsync($"https://{_auth0Domain}/oauth/token", content);

            var msg = await stringTask;
            var result = await msg.Content.ReadAsStringAsync();
            // TODO find a working method
            // fails with "error":"unauthorized_client","error_description":"Grant type 'client_credentials' not allowed for the client.","error_uri":"https://auth0.com/docs/clients/client-grant-types"}


            var token = JsonConvert.DeserializeObject<dynamic>(result);

            return token.access_token;
        }

        private async Task UpdateUserMetaData(CustomerPoco customer)
        {
            var token = await GetToken();
            var client = new ManagementApiClient(token, new Uri($"https://{_auth0Domain}"));
            var request = new UserUpdateRequest {
                UserMetadata =
                {
                    customer_id = customer.CustomerId,
                    address_id = customer.AddressId,
                    doctor_id = customer.DoctorId,
                    shop_id = customer.ShopId,
                    signed_up = true
                }
            };
            try
            {
                await client.Users.UpdateAsync(customer.UserId, request);
            }
            catch (Exception ex)
            {
                logger.Info("Updating user meta_data in Auth0 failed: {0}", ex.Message);
            }
        }

       

        private async Task<List<string>> ValidateCustomer(CustomerPoco customer)
        {
            var errors = new List<string>();
            var existingEmail = await EmailExists(customer.Email);
            if (existingEmail)
            {
                logger.Info("Customer with email {0} has already been registered", customer.Email);
                errors.Add("This email address has already been registered");
                return errors;
            }

            var existingMobile = await MobileExists(customer.Mobile);
            if (existingMobile)
            {
                logger.Info("Customer with mobile {0} has already been registered", customer.Mobile);
                errors.Add("This mobile number has already been registered");
                return errors;
            }

            var existingCustomer = await CustomerExists(customer);
            if (existingCustomer)
            {
                logger.Info("Customer ({0} - {1}) with email the same name, dob and doctor has already been registered", 
                    customer.Fullname, customer.Dob);
                errors.Add("It looks like you have already registered with a different email address");
                return errors;
            }

            var age = GetAge(customer.Dob);
            if (age < 18)
            {
                logger.Info("Customer ({0} - {1}) with email the same name, dob and doctor has already been registered",
                    customer.Fullname, customer.Dob);
                errors.Add("You must be at least 18 years old to use this service");
            }

            var validPostcode = ValidPostcode(customer.Address.Postcode);
            if (validPostcode == "")
            {
                logger.Info("Customer '{0}' - {1} is not in allowed list of postcodes.",
                    customer.Fullname, customer.Address.Postcode);
                errors.Add("The postcode '{0}' is invalid.");
            }
            else
            {
                if (!AllowedArea(validPostcode))
                {
                    logger.Info("Customer '{0}' - {1} is not in allowed list of postcodes.",
                        customer.Fullname, customer.Address.Postcode);
                    errors.Add("The service is currently not available in the area you live in");
                }
            }

            return errors;
        }

        private string ValidPostcode(string postcode)
        {
            var noSpaces = postcode.Replace(" ", "");
            var lastDigit = noSpaces.LastIndexOfAny(Digits);
            if (lastDigit == -1)
            {
                return "";
            }
            return noSpaces.Insert(lastDigit, " ");
        }

        private bool AllowedArea(string validPostcode)
        {
            return Array.Exists(_allowedPostcodes, element => element == validPostcode.Split(" ")[0]);
        }

        private async Task<bool> EmailExists(string email)
        {
            var customers = await _unitOfWork.CustomerRepository.Get(c => c.Email == email);
            return customers.Any();
        }

        private async Task<bool> MobileExists(string mobile)
        {
            var customers = await _unitOfWork.CustomerRepository.Get(c => c.Mobile == mobile);
            return customers.Any();
        }

        private async Task<bool> CustomerExists(CustomerPoco customer)
        {
            var customers = await _unitOfWork.CustomerRepository
                .Get(c => c.Firstname == customer.Firstname 
                    && c.Lastname == customer.Lastname
                          && c.Dob == customer.Dob
                          && c.DoctorId == customer.DoctorId);
            return customers.Any();
        }

        private int GetAge(DateTime bornDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
                age--;

            return age;
        }
}
    
}