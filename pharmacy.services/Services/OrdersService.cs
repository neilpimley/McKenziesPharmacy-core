using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;

namespace Pharmacy.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ICustomersService _customerService;
        private readonly IReminderService _reminderService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;

        public OrdersService(IUnitOfWork unitOfWork, IEmailService emailService, 
            ICustomersService customerService, IReminderService reminderService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _customerService = customerService;
            _reminderService = reminderService;
            _mapper = mapper;
        }

        public async Task<Order> GetCurrentOrder(Guid customerId, int orderStatus)
        {
            logger.Info("GetCurrentOrder - CustomerId: {0}, Status: {1}", orderStatus);
            var orders = await _unitOfWork.OrderRepository
                .Get(o => o.CustomerId == customerId
                && o.OrderStatus == orderStatus);
            var order = orders.FirstOrDefault();
            if (order == null)
                order = _createOrder(customerId).Result;
            return order; 
        }

        private async Task<Order> _createOrder(Guid customerId)
        {
            logger.Info("_createOrder - CustomerId: {0}", customerId);
            var order = new Order()
            {
                OrderId = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                CustomerId = customerId,
                OrderStatus = (int)Status.Inbasket
            };
             _unitOfWork.OrderRepository.Insert(order);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logger.Error("createOrder - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
            return order;
        }

        public async Task<OrderPoco> GetOrder(Guid id)
        {
            logger.Info("GetOrder - OrderId: {0}", id);
            var _order = await _unitOfWork.OrderRepository.GetByID(id);
            var order = _mapper.Map<OrderPoco>(_order);
            order.Customer = await _customerService.GetCustomer(_order.CustomerId.Value); ;
            order.Items = await GetOrderLines(order.OrderId);
            return order;
        }

        public async Task<IEnumerable<OrderPoco>> GetOrders(Guid customerId)
        {
            logger.Info("GetOrders - CustomerId: {0}", customerId);
            var customer = await _customerService.GetCustomer(customerId);
            // don't get the current order
            var _orders = _unitOfWork.OrderRepository
                .Get(o => o.CustomerId == customerId 
                && o.OrderStatus != (int)Status.Inbasket, 
                    o => o.OrderByDescending(d => d.OrderDate));
            var orders = _mapper.Map<List<OrderPoco>>(_orders);
            foreach (var order in orders)
            {
                order.Customer = customer;
                order.Items = await GetOrderLines(order.OrderId);
            }
            return orders;
        }

        public async Task<OrderLine> GetOrderLine(Guid id)
        {
            logger.Info("GetOrderLine - OrderLineID: {0}", id);
            return await _unitOfWork.OrderLineRepository.GetByID(id);
        }

        public async Task<IEnumerable<DrugPoco>> GetOrderLines(Guid id)
        {
            logger.Info("GetOrderLines - OrderId: {0}", id);
            var drugs = await _unitOfWork.DrugRepository.Get();
            return (from o in await _unitOfWork.OrderLineRepository
                        .Get(filter: o => o.OrderId == id)
                    join d in drugs on o.DrugId equals d.DrugId
                    select new DrugPoco()
                    {
                        DrugId = o.DrugId,
                        OrderLineId = o.OrderLineId,
                        DrugName = d.DrugName,
                        Qty = o.Qty
                    });
        }

        public async Task SubmitOrder(OrderPoco order)
        {
            logger.Info("SubmitOrder - Order: {0}", JsonConvert.SerializeObject(order));
            // update the order status
            var _order = await _unitOfWork.OrderRepository.GetByID(order.OrderId);
            _order.OrderDate = DateTime.Now;
            _order.OrderStatus = (int)Status.Ordered;
            _unitOfWork.OrderRepository.Update(_order);
            logger.Info("SubmitOrder - Status Update");

            // add a new status
            var status = new OrderStatus()
            {
                OrderStatusId = Guid.NewGuid(),
                Status = (int) Status.Ordered,
                StatusSetDate = DateTime.Now,
                OrderId = order.OrderId
            };
            _unitOfWork.OrderStatusRepository.Insert(status);
            order.Items = await GetOrderLines(order.OrderId);

            var customer = await _customerService.GetCustomer(order.CustomerId.Value);
            order.Customer = customer;

            var reminder = new ReminderPoco()
            {
                CustomerId = customer.CustomerId,
                OrderId = order.OrderId
            };
            await _reminderService.AddReminder(reminder);
            logger.Info("SubmitOrder - Reminder Added");

            try
            {
                await _unitOfWork.SaveAsync();
                await _emailService.SendOrderConfirmation(order);
                logger.Info("SubmitOrder - Success");
            }
            catch(Exception ex) 
            {
                logger.Error("SubmitOrder - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderLine> AddToOrder(OrderLine orderLine) {
            logger.Info("AddToOrder - OrderLine: {0}", JsonConvert.SerializeObject(orderLine));
            // check it drug isn't in basket. Ifit is then just inrease the quantity
            var existingOrderLines = await _unitOfWork.OrderLineRepository
                .Get(o => o.DrugId == orderLine.DrugId && o.OrderId == orderLine.OrderId);

            var existingOrderLine = existingOrderLines.FirstOrDefault();
            if (existingOrderLine == null) {
                orderLine.CreatedOn = DateTime.Now;
                orderLine.OrderLineId = Guid.NewGuid();
                _unitOfWork.OrderLineRepository.Insert(orderLine);
            } else {
                existingOrderLine.Qty++;
                existingOrderLine.CreatedOn = DateTime.Now;
                _unitOfWork.OrderLineRepository.Update(existingOrderLine);
                orderLine = existingOrderLine;
            }
            try
            {
                _unitOfWork.Save();
                logger.Info("AddToOrder - Success");
            }
            catch (Exception ex)
            {
                logger.Error("AddToOrder - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
            return orderLine;
        }

        public async Task DeleteFromOrder(OrderLine orderLine) {
            logger.Info("DeleteFromOrder - OrderLine: {0}", JsonConvert.SerializeObject(orderLine));
            _unitOfWork.OrderLineRepository.Delete(orderLine.OrderLineId);
            try
            {
                await _unitOfWork.SaveAsync();
                logger.Info("DeleteFromOrder - Success");
            }
            catch (Exception ex)
            {
                logger.Error("DeleteFromOrder - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }


        
    }
}