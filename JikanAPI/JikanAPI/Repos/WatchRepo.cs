using JikanAPI.Exceptions;
using JikanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Repos
{
    public class WatchRepo : IWatchRepo
    {
        private JikanDbContext _context;

        public WatchRepo(JikanDbContext context)
        {
            _context = context;
        }

        public int AddWatch(Watch toAdd)
        {
            if (toAdd == null)
                throw new ArgumentNullException("Watch is null.");

            _context.Watches.Add(toAdd);
            _context.SaveChanges();
            return toAdd.Id;
        }

        public List<Watch> GetAllWatches()
        {
            return _context.Watches.ToList();
        }

        public Watch GetWatchById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");

            Watch toReturn = _context.Watches.Find(id);

            if (toReturn == null)
                throw new WatchNotFoundException("Could not find watch with that Id.");

            return toReturn;
        }

        public Watch GetWatchByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name is null.");

            Watch toReturn = _context.Watches.Where(w => w.Name == name).SingleOrDefault();

            if (toReturn == null)
                throw new WatchNotFoundException("Cannot find watch with that name.");

            return toReturn;
        }

        public List<Watch> GetWatchesByType(string type)
        {
            if (type == null)
                throw new ArgumentNullException("Type is null.");

            List<Watch> toReturn = _context.Watches.Where(w => w.Type == type).ToList();

            return toReturn;
        }

        public List<Watch> GetWatchesByOrderId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");
            if (_context.Orders.Find(id) == null)
                throw new InvalidIdException("Cannot find order with that id.");

            List<OrderDetail> ordersList = _context.OrderDetails.Where(od => od.OrderId == id).ToList();
            List<Watch> toReturn = new List<Watch>();
            foreach(OrderDetail od in ordersList)
            {
                Watch toAdd = _context.Watches.Find(od.WatchId);
                toReturn.Add(toAdd);
            }

            return toReturn;
        }

        public List<Watch> GetWatchesByPrice(decimal max)
        {
            if (max <= 0.0m)
                throw new ArgumentException("Max cannot be less than or equal to 0.");

            List<Watch> toReturn = _context.Watches.Where(w => w.Price <= max).ToList();
            return toReturn;
        }

        public void EditWatch(Watch toEdit)
        {
            if (toEdit == null)
                throw new ArgumentNullException("Watch is null.");
            if (toEdit.Id <= 0)
                throw new InvalidIdException("Invalid id.");
            if (_context.Watches.Find(toEdit.Id) == null)
                throw new WatchNotFoundException("Watch with that id cannot be found.");

            _context.Attach(toEdit);
            _context.Entry(toEdit).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteWatch(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");

            Watch toDelete = _context.Watches.Find(id);

            if (toDelete == null)
                throw new WatchNotFoundException("Watch with that id cannot be found.");

            _context.Attach(toDelete);
            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public List<int> GetWatchQuantityByOrderId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");
            if (_context.Orders.Find(id) == null)
                throw new InvalidIdException("Cannot find order with that id.");

            List<OrderDetail> ordersList = _context.OrderDetails.Where(od => od.OrderId == id).ToList();
            List<int> toReturn = new List<int>();
            foreach (OrderDetail od in ordersList)
            {
                int toAdd = od.Quantity;
                toReturn.Add(toAdd);
            }

            return toReturn;
        }
    }
}
