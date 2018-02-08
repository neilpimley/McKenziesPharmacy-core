using System;
using System.Text;
using System.Collections.Generic;
using Pharmacy.Services;
using Moq;
using System.Net.Http;
using Pharmacy.tests.helper;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;

namespace Pharmacy.ServiceTests
{
    /// <summary>
    /// Summary description for TestCustomerService
    /// </summary>
    public class TestCustomersService : IDisposable
    {
        private ICustomersService _customersService;
        private IUnitOfWork _unitOfWork;

        private List<CustomerPoco> _customerPocos;
        private List<Customer> _customers;
        private List<Address> _addresses;
        private List<Title> _titles;
        private List<Shop> _shops;
        private List<Doctor> _doctors;

        private GenericRepository<Customer> _customersRepository;
        private GenericRepository<Address> _addressRepository;
        private GenericRepository<Title> _titlesRepository;
        private GenericRepository<Shop> _shopsRepository;
        private GenericRepository<Doctor> _doctorsRepository;

        private HttpClient _client;

        private HttpResponseMessage _response;
        private const string ServiceBaseURL = "http://localhost:50875/";

        private DataInitializer dataInitializer;

        public TestCustomersService()
        {
            dataInitializer = new DataInitializer();
        }

        private List<Address> SetUpAddresses()
        {
            var addresses = dataInitializer.GetAllAddreses();
            foreach (Address address in addresses)
                address.AddressId = Guid.NewGuid();
            return addresses;
        }

        private List<Customer> SetUpCustomers()
        {
            var customers = dataInitializer.GetAllCustomers();
            foreach (Customer customer in customers)
                customer.CustomerId = Guid.NewGuid();
            return customers;
        }

        private List<Title> SetUpTitles()
        {
            var titles = dataInitializer.GetAllTitles();
            foreach (Title title in titles)
                title.TitleId = Guid.NewGuid();
            return titles;
        }

        private GenericRepository<Customer> SetUpCustomerRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Customer>>();

            // Setup mocking behavior
           // mockRepo.Setup(x => x.Get(c => c.UserId == "test1", 
                //c => c.OrderBy(cu => cu.Lastname), string.Empty)).Returns(Task.FromResult( _customers));

            mockRepo.Setup(x => x.GetByID(It.IsAny<int>()))
            .Returns(new Func<int, Customer>(
            id => _customers.Find(p => p.CustomerId.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Customer>())))
            .Callback(new Action<Customer>(newCustomer =>
            {
                newCustomer.CustomerId = Guid.NewGuid();
                _customers.Add(newCustomer);
            }));

            mockRepo.Setup(p => p.Update(It.IsAny<Customer>()))
            .Callback(new Action<Customer>(customer =>
            {
                var oldCustomer = _customers.Find(a => a.CustomerId == customer.CustomerId);
                oldCustomer = customer;
            }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        private GenericRepository<Practice> SetUpPracticeRepository()
        {
            throw new NotImplementedException();
        }

        private GenericRepository<Doctor> SetUpDoctorRepository()
        {
            throw new NotImplementedException();
        }

        private GenericRepository<Shop> SetUpShopRepository()
        {
            throw new NotImplementedException();
        }

        private GenericRepository<Title> SetUpTitlesRepository()
        {
            throw new NotImplementedException();
        }


        
        public void Dispose()
        {
            _customersService = null;
            _unitOfWork = null;

            _customerPocos = null;
            _customers = null;
            _titles = null;
            _shops = null;
            _doctors = null;

            _customersRepository = null;
            _titlesRepository = null;
            _shopsRepository = null;
            _doctorsRepository = null;
            _customersRepository = null;

            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }

    }
}
