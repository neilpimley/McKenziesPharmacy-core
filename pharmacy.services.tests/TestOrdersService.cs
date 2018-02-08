using System;
using System.Collections;
using Moq;
using Xunit;
using Pharmacy.Services;
using Pharmacy.tests.helper;
using Pharmacy.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.DynamicProxy.Contributors;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;

namespace Phamracy.services.tests
{
    public sealed class TestOrdersService : IDisposable
    {
        private DataInitializer dataInitializer;
        private readonly IOrdersService _orderService;
        private Mock<ICustomersService> _customersService;
        private Mock<IReminderService> _reminderService;
        private Mock<IDrugsService> _drugsService;
        private Mock<IEmailService> _emailService;
        private Mock<GenericRepository<Order>> _ordersRepository;
        private Mock<GenericRepository<Customer>> _customersRepository;
        private Mock<GenericRepository<Drug>> _drugsRepository;
        private Mock<GenericRepository<OrderLine>> _orderlinesRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;

        public TestOrdersService()
        {
            dataInitializer = new DataInitializer();

            _customersService = new Mock<ICustomersService>();
            _reminderService = new Mock<IReminderService>();
            _drugsService = new Mock<IDrugsService>();
            _emailService = new Mock<IEmailService>();

            _unitOfWork = new Mock<IUnitOfWork>();

            _ordersRepository = new Mock<GenericRepository<Order>>();
            _drugsRepository = new Mock<GenericRepository<Drug>>();
            _orderlinesRepository = new Mock<GenericRepository<OrderLine>>();

            _orderService = new OrdersService(_unitOfWork.Object, _emailService.Object, _customersService.Object,
                _reminderService.Object, _mapper);
        }

        

        [Fact]
        public void GetCurrentOrder()
        {

        }

        [Fact]
        public void GetOrder()
        {
        }

        [Fact]
        public void GetOrders_With_No_CustomerId()
        {
            // Arrange
            // Nothing and we expected it to fail 

            // Act
            var response = _orderService.GetOrders(Guid.Empty);

            // Assert
            
        }

        [Fact]
        public void GetOrders_With_CustomerId()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _customersService.Setup(x => x.GetCustomer(customerId)).ReturnsAsync(new CustomerPoco
            {
                CustomerId = customerId
            });

            _ordersRepository.Setup(x => x.Get(null, null,""))
                .ReturnsAsync(dataInitializer.GetAllOrders());


            _drugsRepository.Setup(x => x.Get(null, null, ""))
                .ReturnsAsync(dataInitializer.GetAllDrugs());

            _orderlinesRepository.Setup(x => x.Get(null, null, ""))
                .ReturnsAsync(dataInitializer.GetAllOrderLines());

            // Act
            var orders = _orderService.GetOrders(customerId);

            // Assert
            //Assert.Equal();
        }

        [Fact]
        public void GetOrderLine()
        {
            
        }

        [Fact]
        public void GetOrderLines()
        {
            
        }

        [Fact]
        public void SubmitOrder()
        {

        }

        [Fact]
        public void AddToOrder()
        {
            
        }

        [Fact]
        public void DeleteFromOrder()
        {
            
        }

        [Fact]
        public void Passing_Fact_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void Failing_Fact_Test()
        {
            Assert.True(false);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Theory_Test(bool boolean)
        {
            Assert.True(boolean);
        }

        public void Dispose()
        {
            //clean up
        }
    }

}
