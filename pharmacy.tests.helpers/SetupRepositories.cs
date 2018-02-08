using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;
using Pharmacy.tests.helper;
using Moq;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Interfaces;

namespace pharmacy.tests.helpers
{
    public class SetupRepositories
    {
        private DataInitializer dataInitializer;


        public  GenericRepository<Drug> SetUpDrugsRepository()
        {
            var drugs = dataInitializer.GetAllDrugs();
            foreach (Drug drug in drugs)
                drug.DrugId = Guid.NewGuid();

            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Drug>>();

            // Setup mocking behavior
            mockRepo.Setup(x => x.Get(null, null, ""))
                .ReturnsAsync(drugs);

            mockRepo.Setup(x => x.GetByID(It.IsAny<int>()))
                .ReturnsAsync(new Func<int, Drug>(
                    id => drugs.Find(p => p.DrugId.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Drug>())))
                .Callback(new Action<Drug>(newDrug =>
                {
                    newDrug.DrugId = Guid.NewGuid();
                    drugs.Add(newDrug);
                }));

            mockRepo.Setup(p => p.Update(It.IsAny<Drug>()))
                .Callback(new Action<Drug>(drug =>
                {
                    var oldDrug = drugs.Find(a => a.DrugId == drug.DrugId);
                    oldDrug = drug;
                }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        public GenericRepository<Order> SetUpOrdersRepository()
        {
            var orders = dataInitializer.GetAllOrders();
            foreach (Order order in orders)
                order.OrderId = Guid.NewGuid();

            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Order>>();

            // Setup mocking behavior
            mockRepo.Setup(x => x.Get(null, null, ""))
                .ReturnsAsync(orders);

            mockRepo.Setup(x => x.GetByID(It.IsAny<int>()))
                .ReturnsAsync(new Func<int, Order>(
                    id => orders.Find(p => p.OrderId.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Order>())))
                .Callback(new Action<Order>(newOrder =>
                {
                    newOrder.OrderId = Guid.NewGuid();
                    orders.Add(newOrder);
                }));

            mockRepo.Setup(p => p.Update(It.IsAny<Order>()))
                .Callback(new Action<Order>(order =>
                {
                    var old = orders.Find(a => a.OrderId == order.OrderId);
                    oldOrder = order;
                }));

            // Return mock implementation object
            return mockRepo.Object;
        }
    }
}
