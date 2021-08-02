using JikanAPI.Models.Auth;
using System.Collections.Generic;

namespace JikanAPI.Repos.Interfaces
{
    public interface IUserRepo
    {
        int AddUser(User toAdd);
        List<User> GetAllUsers();
        User GetUserById(int id);
        Role GetRoleByName(string role);
        User GetUserByUsername(string username);
        void DeleteUser(int id);
    }
}
