using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services;
using Pharmacy.Services.Interfaces;
using Xunit;

namespace pharmacy.services.integration.tests
{
    public class SimpleIntegrationTest : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ICustomersService _customerService;
        private readonly IReminderService _reminderService;
        private readonly IMapper _mapper;

        public SimpleIntegrationTest(IUnitOfWork unitOfWork, IEmailService emailService,
            ICustomersService customerService, IReminderService reminderService,
            IMapper mapper)
        {
           
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _customerService = customerService;
            _reminderService = reminderService;
            _mapper = mapper;
    }



        [Fact]
        public async Task GetAllOrder()
        {
            var customerId = new Guid();

            var _ordersService = new OrdersService(_unitOfWork,_emailService,_customerService,
                _reminderService, _mapper);
            var orders = await _ordersService.GetOrders(customerId);

            Assert.Equal(orders.First().CustomerId, customerId);
        }

        public void Dispose()
        {
        }
    }
}
