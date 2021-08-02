using JikanAPI.Models;
using JikanAPI.Models.Auth;
using JikanAPI.Models.ViewModels.Requests;
using System.Collections.Generic;

namespace JikanAPI.Service.Interfaces
{
    public interface IJikanService
    {
        int AddOrder(Order toAdd);
        int AddWatch(Watch toAdd);
        void DeleteOrder(int id);
        void DeleteWatch(int id);
        void EditWatch(Watch toEdit);
        List<Order> GetAllOrders();
        List<User> GetAllUsers();
        List<Watch> GetAllWatches();
        Order GetOrderById(int id);
        List<Order> GetOrdersByUserId(int curUserId);
        User GetUserById(int id);
        Watch GetWatchById(int id);
        Watch GetWatchByName(string name);
        List<Watch> GetWatchesByOrderId(int id);
        List<Watch> GetWatchesByPrice(decimal max);
        List<Watch> GetWatchesByType(string type);
        List<int> GetWatchQuantityByOrderId(int id);
        Dictionary<Watch, int> GetWatchQuantityPair(int id);
        string Login(LoginViewModel vm);
        void RegisterUser(RegisterUserViewModel vm);
    }
}