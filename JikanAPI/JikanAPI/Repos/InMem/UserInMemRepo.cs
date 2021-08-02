using JikanAPI.Models.Auth;
using JikanAPI.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Repos.InMem
{
    public class UserInMemRepo : IUserRepo
    {
        private List<User> _allUsers = new List<User>();
        private List<Role> _allRoles = new List<Role>();
        private int _id = 1;

        public UserInMemRepo()
        {
            _allRoles.Add(new Role { Id = 1, Name = "Admin" });
            _allRoles.Add(new Role { Id = 2, Name = "User" });
        }

        public int AddUser(User toAdd)
        {
            toAdd.Id = _id;
            _allUsers.Add(toAdd);
            _id++;
            return toAdd.Id;
        }

        public void DeleteUser(int id)
        {
            _allUsers.RemoveAll(u => u.Id == id);
        }

        public List<User> GetAllUsers()
        {
            return _allUsers;
        }

        public Role GetRoleByName(string role)
        {
            return _allRoles.SingleOrDefault(r => r.Name.ToLower() == role.ToLower());
        }

        public User GetUserById(int id)
        {
            return _allUsers.SingleOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _allUsers.SingleOrDefault(u => u.Username == username);
        }
    }
}
