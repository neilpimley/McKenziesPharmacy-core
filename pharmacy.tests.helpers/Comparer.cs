using System;
using System.Collections;
using System.Collections.Generic;
using Pharmacy.Models;

namespace Pharmacy.tests.helper
{
    public class ShopComparer : IComparer, IComparer<Shop>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Shop;
            var rhs = actual as Shop;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Shop expected, Shop actual)
        {
            int temp;
            return (temp = expected.ShopId.CompareTo(actual.ShopId)) != 0 ? temp : expected.ShopName.CompareTo(actual.ShopName);
        }
    }

    public class DrugComparer : IComparer, IComparer<Drug>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Drug;
            var rhs = actual as Drug;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Drug expected, Drug actual)
        {
            int temp;
            return (temp = expected.DrugId.CompareTo(actual.DrugId)) != 0 ? temp : expected.DrugName.CompareTo(actual.DrugName);
        }
    }

    public class CustomerComparer : IComparer, IComparer<Customer>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Customer;
            var rhs = actual as Customer;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Customer expected, Customer actual)
        {
            int temp;
            return (temp = expected.CustomerId.CompareTo(actual.CustomerId)) != 0 ? temp : expected.Firstname.CompareTo(actual.Firstname);
        }
    }

    public class OrderComparer : IComparer, IComparer<Order>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Order;
            var rhs = actual as Order;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Order expected, Order actual)
        {
            int temp;
            return (temp = expected.OrderId.CompareTo(actual.OrderId)) != 0 ? temp : expected.OrderId.CompareTo(actual.OrderId);
        }
    }
}