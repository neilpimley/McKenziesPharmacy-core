using System;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Repositories.Interfaces;

namespace Pharmacy.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PharmacyContext context;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<Favourite> favouriteRepository;
        private GenericRepository<Address> addressRepository;
        private GenericRepository<Drug> drugRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<OrderLine> orderLineRepository;
        private GenericRepository<OrderStatus> orderStatusRepository;
        private GenericRepository<Doctor> doctorRepository;
        private GenericRepository<Practice> practiceRepository;
        private GenericRepository<Title> titleRepository;
        private GenericRepository<Shop> shopRepository;
        private GenericRepository<Reminder> reminderRepository;
        private GenericRepository<ReminderOrder> reminderOrderRepository;
        private GenericRepository<CollectScript> collectScriptRepository;

        public UnitOfWork(PharmacyContext context)
        {
            this.context = context ?? throw new ArgumentNullException("Context was not supplied");
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<Favourite> FavouriteRepository
        {
            get
            {
                if (this.favouriteRepository == null)
                {
                    this.favouriteRepository = new GenericRepository<Favourite>(context);
                }
                return favouriteRepository;
            }
        }

        public GenericRepository<Address> AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new GenericRepository<Address>(context);
                }
                return addressRepository;
            }
        }

        public GenericRepository<Drug> DrugRepository
        {
            get
            {
                if (this.drugRepository == null)
                {
                    this.drugRepository = new GenericRepository<Drug>(context);
                }
                return drugRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }

        public GenericRepository<OrderLine> OrderLineRepository
        {
            get
            {
                if (this.orderLineRepository == null)
                {
                    this.orderLineRepository = new GenericRepository<OrderLine>(context);
                }
                return orderLineRepository;
            }
        }

        public GenericRepository<OrderStatus> OrderStatusRepository
        {
            get
            {
                if (this.orderStatusRepository == null)
                {
                    this.orderStatusRepository = new GenericRepository<OrderStatus>(context);
                }
                return orderStatusRepository;
            }
        }

        public GenericRepository<Doctor> DoctorRepository {
            get
            {
                if (this.doctorRepository == null)
                {
                    this.doctorRepository = new GenericRepository<Doctor>(context);
                }
                return doctorRepository;
            }
        }

        public GenericRepository<Practice> PracticeRepository {
            get
            {
                if (this.practiceRepository == null)
                {
                    this.practiceRepository = new GenericRepository<Practice>(context);
                }
                return practiceRepository;
            }
        }

        public GenericRepository<Title> TitleRepository {
            get
            {
                if (this.titleRepository == null)
                {
                    this.titleRepository = new GenericRepository<Title>(context);
                }
                return titleRepository;
            }
        }

        public GenericRepository<Shop> ShopRepository {
            get
            {
                if (this.shopRepository == null)
                {
                    this.shopRepository = new GenericRepository<Shop>(context);
                }
                return shopRepository;
            }
        }

        public GenericRepository<Reminder> ReminderRepository
        {
            get
            {
                if (this.reminderRepository == null)
                {
                    this.reminderRepository = new GenericRepository<Reminder>(context);
                }
                return reminderRepository;
            }
        }
        public GenericRepository<ReminderOrder> ReminderOrderRepository
        {
            get
            {
                if (this.reminderOrderRepository == null)
                {
                    this.reminderOrderRepository = new GenericRepository<ReminderOrder>(context);
                }
                return reminderOrderRepository;
            }
        }
        public GenericRepository<CollectScript> CollectScriptRepository
        {
            get
            {
                if (this.collectScriptRepository == null)
                {
                    this.collectScriptRepository = new GenericRepository<CollectScript>(context);
                }
                return collectScriptRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO: find some way of getting a meaningful error message and loggnig the error

                // Retrieve the error messages as a list of strings.
              /*  var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);*/

                // Join the list to a single string.
                //var exceptionMessage = string.Join("; ", errorMessages);

                //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                throw;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
