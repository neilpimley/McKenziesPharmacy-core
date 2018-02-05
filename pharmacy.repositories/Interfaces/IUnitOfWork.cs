using System;
using System.Threading.Tasks;
using Pharmacy.Models;

namespace Pharmacy.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<Favourite> FavouriteRepository { get; }
        GenericRepository<Address> AddressRepository { get; }
        GenericRepository<Drug> DrugRepository { get; }
        GenericRepository<Order> OrderRepository { get; }
        GenericRepository<OrderLine> OrderLineRepository { get; }
        GenericRepository<OrderStatus> OrderStatusRepository { get; }
        GenericRepository<Doctor> DoctorRepository { get; }
        GenericRepository<Practice> PracticeRepository { get; }
        GenericRepository<Title> TitleRepository { get; }
        GenericRepository<Shop> ShopRepository { get; }
        GenericRepository<Reminder> ReminderRepository { get; }
        GenericRepository<ReminderOrder> ReminderOrderRepository { get; }
        GenericRepository<CollectScript> CollectScriptRepository { get; }
        void Save();
        Task SaveAsync();
    }
}