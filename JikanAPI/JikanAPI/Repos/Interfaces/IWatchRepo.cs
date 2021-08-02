using JikanAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Repos
{
    public interface IWatchRepo
    {
        int AddWatch(Watch toAdd);
        Watch GetWatchById(int value);
        Watch GetWatchByName(string name);
        List<Watch> GetAllWatches();
        List<Watch> GetWatchesByType(string type);
        List<Watch> GetWatchesByPrice(decimal max);
        void EditWatch(Watch toEdit);
        void DeleteWatch(int id);
        List<int> GetWatchQuantityByOrderId(int id);
        List<Watch> GetWatchesByOrderId(int id);
    }
}
