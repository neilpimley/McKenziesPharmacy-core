using System;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IReminderService
    {
        Task<Reminder> AddReminder(ReminderPoco reminder);
        Task DeleteReminder(Guid id);
    }
}
