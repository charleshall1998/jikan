using JikanAPI.Models;
using JikanAPI.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Repos.InMem
{
    public class OrderInMemRepo : IOrderRepo
    {
        private List<Order> _allOrders = new List<Order>();
        private int _id = 1;

        public int AddOrder(Order toAdd)
        {
            toAdd.Id = _id;
            _allOrders.Add(toAdd);
            _id++;
            return toAdd.Id;
        }

        public void DeleteOrder(int id)
        {
            _allOrders.RemoveAll(o => o.Id == id);
        }

        public List<Order> GetAllOrders()
        {
            return _allOrders;
        }

        public Order GetOrderById(int id)
        {
            return _allOrders.SingleOrDefault(o => o.Id == id);
        }

        public List<Order> GetOrdersByUserId(int curUserId)
        {
            List<Order> toReturn = new List<Order>();

            foreach(Order o in _allOrders)
            {
                if (o.Purchaser.Id == curUserId)
                    toReturn.Add(o);
            }

            return toReturn;
        }
    }
}
