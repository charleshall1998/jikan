using JikanAPI.Exceptions;
using JikanAPI.Models.Auth;
using JikanAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JikanAPI.Repos
{
    public class UserRepo : IUserRepo
    {
        private JikanDbContext _context;

        public UserRepo(JikanDbContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Role GetRoleByName(string role)
        {
            if (role == null)
                throw new ArgumentNullException("Role is null");

            Role toReturn = _context.Roles.SingleOrDefault(r => r.Name.ToLower() == role.ToLower());

            if (toReturn == null)
                throw new InvalidNameException("Cannot find role with that name.");

            return toReturn;
        }

        public User GetUserById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid id.");

            User toReturn = _context.Users.Find(id);

            if (toReturn == null)
                throw new InvalidIdException("User with that id cannot be found.");

            return toReturn;
        }

        public int AddUser(User toAdd)
        {
            if (toAdd == null)
                throw new ArgumentNullException("User is null.");

            _context.Users.Add(toAdd);
            _context.SaveChanges();
            return toAdd.Id;
        }

        public User GetUserByUsername(string username)
        {
            if (username == null)
                throw new ArgumentNullException("Username is null");

            User toReturn = _context.Users
                .Include("UserRoles.SelectedRole")
                .SingleOrDefault(
                u => u.Username.ToLower() == username.ToLower()
                );

            if (toReturn == null)
                throw new InvalidUsernameException("User with that username cannot be found.");

            return toReturn;
        }

    }
}
