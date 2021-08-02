using JikanAPI.Exceptions;
using JikanAPI.Models;
using JikanAPI.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JikanAPI.Repos
{
    public class OrderRepo : IOrderRepo
    {
        private JikanDbContext _context;

        public OrderRepo(JikanDbContext context)
        {
            _context = context;
        }

        public int AddOrder(Order toAdd)
        {
            if(toAdd == null)
                throw new ArgumentNullException("Cannot add null order.");

            _context.Orders.Add(toAdd);
            _context.SaveChanges();

            foreach(OrderDetail od in toAdd.OrderDetails)
            {
                od.OrderId = toAdd.Id;
                _context.OrderDetails.Add(od);
            }

            return toAdd.Id;
        }

        public Order GetOrderById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");

            Order toReturn = _context.Orders.Find(id);

            if(toReturn == null)
                throw new InvalidIdException("Order with that id does not exist.");

            toReturn.OrderDetails = _context.OrderDetails.Where(od => od.OrderId == id).ToList();

            return toReturn;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> toReturn = _context.Orders.ToList();
            foreach(Order o in toReturn)
            {
                o.OrderDetails = _context.OrderDetails.Where(od => od.OrderId == o.Id).ToList();
            }

            return toReturn;
        }

        public void DeleteOrder(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");

            Order toDelete = _context.Orders.Find(id);

            if (toDelete == null)
                throw new InvalidIdException("Order with that id cannot be found.");

            _context.Attach(toDelete);
            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByUserId(int curUserId)
        {
            if (curUserId <= 0)
                throw new InvalidIdException("Invalid id.");
            if (_context.Users.Find(curUserId) == null)
                throw new InvalidIdException("Cannot find user with that id.");

            return _context.Orders.Where(o => o.Purchaser.Id == curUserId).ToList();
        }
    }
}
