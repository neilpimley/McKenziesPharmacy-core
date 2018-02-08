using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services;
using Pharmacy.Services.Interfaces;

namespace pharmacy.services.integration.tests
{
    public class Startup
    {
        public Startup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<PharmacyContext>(options => options
                .UseSqlServer($"Data Source=.;Initial Catalog=testchemist;Integrated Security=True;multipleactiveresultsets=True;application name=EntityFramework;"));

            services.AddTransient<IDrugsService, DrugsService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IDrugsService, DrugsService>();
            services.AddTransient<IFavouritesService, FavouritesService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IReminderService, ReminderService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerPoco, Customer>();
                cfg.CreateMap<OrderPoco, Order>();
                cfg.CreateMap<DrugPoco, Drug>();
                cfg.CreateMap<ReminderPoco, Reminder>();

                cfg.CreateMap<Customer, CustomerPoco>();
                cfg.CreateMap<Order, OrderPoco>();
                cfg.CreateMap<Drug, DrugPoco>();
                cfg.CreateMap<Reminder, ReminderPoco>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            /*
            var serviceProvider = services
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            
            var builder = new DbContextOptionsBuilder<PharmacyContext>();

            builder.UseSqlServer($"Data Source=.;Initial Catalog=testchemist;Integrated Security=True;multipleactiveresultsets=True;application name=EntityFramework;")
                .UseInternalServiceProvider(serviceProvider);


            _context = new PharmacyContext(builder.Options);*/
        }

    }
}
