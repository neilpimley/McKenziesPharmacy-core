using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;

namespace Pharmacy.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ReminderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Reminder> AddReminder(ReminderPoco reminder)
        {
            logger.Info("AddReminder - {0}", JsonConvert.SerializeObject(reminder));
            var _reminder = new Reminder()
            {
                ReminderId = Guid.NewGuid(),
                SendTime = DateTime.Now.AddDays(14),
                Sent = false,
                CreatedOn = DateTime.Now,
                CustomerId = reminder.CustomerId
            };
            var _reminderOrder = new ReminderOrder()
            {
                ReminderOrderId = Guid.NewGuid(),
                ReminderId = _reminder.ReminderId,
                OrderId = reminder.OrderId
            };
            
            _unitOfWork.ReminderRepository.Insert(_reminder);
            _unitOfWork.ReminderOrderRepository.Insert(_reminderOrder);
            try
            {
                await _unitOfWork.SaveAsync();
                logger.Info("AddReminder - Success");
            }
            catch (Exception ex)
            {
                logger.Error("AddReminder - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
            return reminder;
        }

        public async Task DeleteReminder(Guid id)
        {
            logger.Info("DeleteReminder -id {0}", id);
            var reminderOrders = await _unitOfWork.ReminderOrderRepository
                .Get(o => o.ReminderId == id);
            
            var reminderOrder = reminderOrders.FirstOrDefault();
            if (reminderOrder != null) { 
                _unitOfWork.ReminderOrderRepository.Delete(reminderOrder.ReminderOrderId);
                _unitOfWork.ReminderRepository.Delete(id);
                
            }
            // TODO: add deletes from other reminders
            try
            {
                await _unitOfWork.SaveAsync();
                logger.Info("DeleteReminder - Success");
            }
            catch (Exception ex)
            {
                logger.Error("DeleteReminder - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
