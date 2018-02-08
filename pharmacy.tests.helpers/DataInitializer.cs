using Pharmacy.Models;
using System;
using System.Collections.Generic;

namespace Pharmacy.tests.helper
{
    /// <summary>
    /// Data initializer for unit tests
    /// </summary>
    public class DataInitializer
    {
        #region private variables
        private readonly Guid _customerId1;
        private readonly Guid _customerId2;
        private readonly Guid _customerId3;
        private readonly Guid _customerId4;

        private readonly Guid _addressId1;
        private readonly Guid _addressId2;
        private readonly Guid _addressId3;
        private readonly Guid _addressId4;
                 
        private readonly Guid _titleId1;
                 
        private readonly Guid _drugId1;
        private readonly Guid _drugId2;
        private readonly Guid _drugId3;
        private readonly Guid _drugId4;
                 
        private readonly Guid _shopId1;
        private readonly Guid _shopId2;
                 
        private readonly Guid _doctorId1;
        private readonly Guid _doctorId2;
                 
        private readonly Guid _practiceId1;
        private readonly Guid _practiceId2;
                 
        private readonly Guid _orderId1;
        private readonly Guid _orderId2;
                 
        private readonly Guid _orderLineId1;
        private readonly Guid _orderLineId2;
        private readonly Guid _orderLineId3;
        private readonly Guid _orderLineId4;

        #endregion private variables

        public DataInitializer()
        {
            _addressId1 = Guid.NewGuid();
            _addressId2 = Guid.NewGuid();
            _addressId3 = Guid.NewGuid();
            _addressId4 = Guid.NewGuid();

            _customerId1 = Guid.NewGuid();
            _customerId2 = Guid.NewGuid();
            _customerId3 = Guid.NewGuid();
            _customerId4 = Guid.NewGuid();

            _titleId1 = Guid.NewGuid();

            _drugId1 = Guid.NewGuid();
            _drugId2 = Guid.NewGuid();
            _drugId3 = Guid.NewGuid();
            _drugId4 = Guid.NewGuid();

            _shopId1 = Guid.NewGuid();
            _shopId2 = Guid.NewGuid();

            _doctorId1 = Guid.NewGuid();
            _doctorId2 = Guid.NewGuid();

            _practiceId1 = Guid.NewGuid();
            _practiceId2 = Guid.NewGuid();

            _orderId1 = Guid.NewGuid();
            _orderId2 = Guid.NewGuid();

            _orderLineId1 = Guid.NewGuid();
            _orderLineId2 = Guid.NewGuid();
            _orderLineId3 = Guid.NewGuid();
            _orderLineId4 = Guid.NewGuid();
        }

        /// <summary>
        /// Dummy customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { UserId = "user1", CustomerId = _customerId1, TitleId = _titleId1, Firstname = "Joe1", Lastname = "Brown1", AddressId = _addressId1, DoctorId = _doctorId1, ShopId = _shopId1 },
                new Customer { UserId = "user2", CustomerId = _customerId2, TitleId = _titleId1, Firstname = "Joe2", Lastname = "Brown2", AddressId = _addressId2, DoctorId = _doctorId2, ShopId = _shopId2 },
                new Customer { UserId = "user3", CustomerId = _customerId3, TitleId = _titleId1, Firstname = "Joe3", Lastname = "Brown3", AddressId = _addressId3, DoctorId = _doctorId1, ShopId = _shopId1 },
                new Customer { UserId = "user4", CustomerId = _customerId4, TitleId = _titleId1, Firstname = "Joe4", Lastname = "Brown4", AddressId = _addressId4, DoctorId = _doctorId2, ShopId = _shopId2 }
            };
            return customers;
        }

        /// <summary>
        /// Dummy address
        /// </summary>
        /// <returns></returns>
        public List<Address> GetAllAddreses()
        {
            var list = new List<Address>()
            {
                new Address { AddressId = _addressId1, AddressLine1 = "1 house", Postcode = "PC1 1HR" },
                new Address { AddressId = _addressId2, AddressLine1 = "2 house", Postcode = "PC1 2HR" },
                new Address { AddressId = _addressId3, AddressLine1 = "3 house", Postcode = "PC1 3HR" },
                new Address { AddressId = _addressId4, AddressLine1 = "4 house", Postcode = "PC1 4HR" }
            };
            return list;
        }

        /// <summary>
        /// Dummy orders
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAllOrders()
        {
            var list = new List<Order>()
            {
                new Order { OrderId = _orderId1, CustomerId = _customerId1 },
                new Order { OrderId = _orderId2, CustomerId = _customerId2 }
            };
            return list;
        }

        /// <summary>
        /// Dummy order lines
        /// </summary>
        /// <returns></returns>
        public List<OrderLine> GetAllOrderLines()
        {
            var list = new List<OrderLine>()
            {
                new OrderLine { OrderLineId = _orderLineId1, OrderId = _orderId1, Qty = 1 },
                new OrderLine { OrderLineId = _orderLineId2, OrderId = _orderId1, Qty = 1 },
                new OrderLine { OrderLineId = _orderLineId3, OrderId = _orderId2, Qty = 1 },
                new OrderLine { OrderLineId = _orderLineId4, OrderId = _orderId2, Qty = 1 },
            };
            return list;
        }

        /// <summary>
        /// Dummy drugs
        /// </summary>
        /// <returns></returns>
        public List<Drug> GetAllDrugs()
        {
            var addresses = new List<Drug>()
            {
                new Drug { DrugId = _drugId1, DrugName = "drug" },
                new Drug { DrugId = _drugId2, DrugName = "drug y" },
                new Drug { DrugId = _drugId3, DrugName = "drug z" },
                new Drug { DrugId = _drugId4, DrugName = "drug yz" }
            };
            return addresses;
        }

        /// <summary>
        /// Dummy favourites
        /// </summary>
        /// <returns></returns>
        public List<Favourite> GetAllFavourites()
        {
            var list = new List<Favourite>()
            {
                new Favourite {FavouriteId = Guid.NewGuid(), DrugId = _drugId1, CustomerId = _customerId1 },
                new Favourite {FavouriteId = Guid.NewGuid(), DrugId = _drugId2, CustomerId = _customerId1 }
            };
            return list;
        }

        /// <summary>
        /// Dummy title
        /// </summary>
        /// <returns></returns>
        public List<Title> GetAllTitles()
        {
            var list = new List<Title>()
            {
                new Title {  TitleId = _titleId1, TitleName = "Mr" }
            };
            return list;
        }

        /// <summary>
        /// Dummy shops
        /// </summary>
        /// <returns></returns>
        public List<Shop> GetAllShops()
        {
            var list = new List<Shop>()
            {
                new Shop {  ShopId = _shopId1, ShopName = "Shop1" },
                new Shop {  ShopId = _shopId2, ShopName = "Shop2" }
            };
            return list;
        }

        /// <summary>
        /// Dummy doctors
        /// </summary>
        /// <returns></returns>
        public List<Doctor> GetAllDoctors()
        {
            var list = new List<Doctor>()
            {
                new Doctor {  DoctorId = _doctorId1, Surname = "Doctor1" },
                new Doctor {  DoctorId = _doctorId2, Surname = "Doctor2" }
            };
            return list;
        }

        /// <summary>
        /// Dummy pracftices
        /// </summary>
        /// <returns></returns>
        public List<Practice> GetAllPractices()
        {
            var list = new List<Practice>(){
                new Practice {  PracticeId = _practiceId1, PracticeName = "Practice1" },
                new Practice {  PracticeId = _practiceId2, PracticeName = "Practice2" }
            };
            return list;
        }

    }
}