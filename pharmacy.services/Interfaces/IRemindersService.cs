using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IRemindersService
    {
        Task<IEnumerable<ReminderPoco>> GetCustomerReminders(Guid customerId);
        Task<IEnumerable<ReminderPoco>> GetAllUnsentReminders(DateTime day);
    }
}
