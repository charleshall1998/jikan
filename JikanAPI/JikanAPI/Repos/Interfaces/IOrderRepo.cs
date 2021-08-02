using JikanAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Repos.Interfaces
{
    public interface IOrderRepo
    {
        int AddOrder(Order toAdd);
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
        void DeleteOrder(int id);
        List<Order> GetOrdersByUserId(int curUserId);
    }
}
