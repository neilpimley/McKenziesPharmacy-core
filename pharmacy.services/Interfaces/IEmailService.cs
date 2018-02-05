using System.Threading.Tasks;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendRegisterConfirmation(CustomerPoco customer);
        Task SendPersonalDetailsAmended(CustomerPoco customer);
        Task SendOrderConfirmation(OrderPoco order);
        Task SendOrderReadyToColect(OrderPoco order);
        Task SendOrderReminder(ReminderPoco reminder);
    }
}
