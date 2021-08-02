using JikanAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JikanAPI.Repos.InMem
{
    public class WatchInMemRepo : IWatchRepo
    {
        private List<Watch> _allWatches = new List<Watch>();
        private int _id = 1;

        public int AddWatch(Watch toAdd)
        {
            toAdd.Id = _id;
            _allWatches.Add(toAdd);
            _id++;
            return toAdd.Id;
        }

        public void DeleteWatch(int id)
        {
           _allWatches.RemoveAll(w => w.Id == id);
        }

        public void EditWatch(Watch toEdit)
        {
            _allWatches = _allWatches.Select(w => w.Id == toEdit.Id ? toEdit : w).ToList();
        }

        public List<Watch> GetAllWatches()
        {
            return _allWatches;
        }

        public Watch GetWatchById(int id)
        {
            return _allWatches.SingleOrDefault(w => w.Id == id);
        }

        public Watch GetWatchByName(string name)
        {
            return _allWatches.SingleOrDefault(w => w.Name == name);
        }

        public List<Watch> GetWatchesByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Watch> GetWatchesByPrice(decimal max)
        {
            List<Watch> toReturn = new List<Watch>();
            
            foreach(Watch w in _allWatches)
            {
                if (w.Price <= max)
                    toReturn.Add(w);
            }

            return toReturn;
        }

        public List<Watch> GetWatchesByType(string type)
        {
            List<Watch> toReturn = new List<Watch>();

            foreach (Watch w in _allWatches)
            {
                if (w.Type == type)
                    toReturn.Add(w);
            }

            return toReturn;
        }

        public List<int> GetWatchQuantityByOrderId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
