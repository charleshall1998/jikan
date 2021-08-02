using JikanAPI.Exceptions;
using JikanAPI.Models;
using JikanAPI.Models.Auth;
using JikanAPI.Models.ViewModels.Requests;
using JikanAPI.Repos;
using JikanAPI.Repos.Interfaces;
using JikanAPI.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JikanAPI.Service
{
    public class JikanService : IJikanService
    {
        IWatchRepo _watchRepo;
        IOrderRepo _orderRepo;
        IUserRepo _userRepo;
        public JikanService(IOrderRepo orderRepo, IWatchRepo watchRepo, IUserRepo userRepo)
        {
            _watchRepo = watchRepo;
            _orderRepo = orderRepo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Generates a unique token for a particular user.
        /// </summary>
        /// <param name="curUser">The current user to generate a token for.</param>  
        /// <returns>The generated token string.</returns>
        private string GenerateToken(User curUser)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    curUser.UserRoles.Select(r => new Claim(ClaimTypes.Role, r.SelectedRole.Name))
                    .Append(new Claim(ClaimTypes.NameIdentifier, curUser.Id.ToString()))
                ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descriptor);
            string tokenStr = tokenHandler.WriteToken(token);

            return tokenStr;
        }

        /// <summary>
        /// Gets a user by their id.
        /// </summary>
        /// <param name="id">The id of the user to be searched.</param>  
        /// <returns>The user with the corresponding id.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public User GetUserById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Id cannot be less than or equal to 0.");

            return _userRepo.GetUserById(id);
        }

        /// <summary>
        /// Validates a given password.
        /// </summary>
        /// <param name="password">The id of the user to be searched.</param>
        /// <param name="passwordSalt">The password salt to encode with.</param>  
        /// <param name="passwordHash">The password hash to compare to.</param> 
        /// <returns>True if valid password, false otherwise.</returns>
        private bool ValidatePassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hMac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                byte[] hashedPass = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hashedPass.Length; i++)
                {
                    if (hashedPass[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Attempts to register a user with the provided user information.
        /// </summary>
        /// <param name="vm">A RegisterUserViewModel representing a username, email, name, and password.</param>
        /// <exception cref="ArgumentNullException">Thrown if the username, email, name, or password is null.</exception>
        /// <exception cref="InvalidUsernameException">Thrown if the username provided is already taken.</exception>
        public void RegisterUser(RegisterUserViewModel vm)
        {
            if (vm.Username == null)
                throw new ArgumentNullException("Username is null");
            if(vm.Email == null)
                throw new ArgumentNullException("Email is null");
            if (vm.Name == null)
                throw new ArgumentNullException("Name is null");
            if (vm.Password == null)
                throw new ArgumentNullException("Password is null");

            User prevUsed = _userRepo.GetUserByUsername(vm.Username);

            if (prevUsed != null)
                throw new InvalidUsernameException("Username already taken.");

            Role basicRole = _userRepo.GetRoleByName("user");
            UserRole bridgeRow = new UserRole();
            bridgeRow.RoleId = basicRole.Id;
            bridgeRow.SelectedRole = basicRole;

            User toAdd = new User();
            bridgeRow.EnrolledUser = toAdd;
            toAdd.UserRoles.Add(bridgeRow);
            toAdd.Email = vm.Email;
            toAdd.Name = vm.Name;
            toAdd.Username = vm.Username;

            using (var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                byte[] saltBytes = hMac.Key;
                byte[] hash = hMac.ComputeHash(Encoding.UTF8.GetBytes(vm.Password));
                toAdd.PasswordSalt = saltBytes;
                toAdd.PasswordHash = hash;
            }

            _userRepo.AddUser(toAdd);
        }

        /// <summary>
        /// Attempts to login a user with given credentials and generate a unique token string.
        /// </summary>
        /// <param name="vm">A LoginViewModel representing a username and password.</param>  
        /// <returns>The generated token string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the username or password is null.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password does not match the username provided.</exception>
        public string Login(LoginViewModel vm)
        {
            if (vm.Username == null)
                throw new ArgumentNullException("Username is null.");
            if (vm.Password == null)
                throw new ArgumentNullException("Password is null.");

            User curUser = _userRepo.GetUserByUsername(vm.Username);
            bool valid = ValidatePassword(vm.Password, curUser.PasswordSalt, curUser.PasswordHash);
            if (!valid)
            {
                throw new InvalidPasswordException("Invalid Password");
            }
            string token = GenerateToken(curUser);

            return token;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        /// <summary>
        /// Adds a watch.
        /// </summary>
        /// <param name="toAdd">The watch to be added</param>  
        /// <returns>The watch that was added.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the name or type of the watch to be added is null.</exception>
        /// <exception cref="InvalidNameException">Thrown if the name is empty, white spaces, or greater than 50 characters.</exception>
        /// <exception cref="InvalidTypeException">Thrown if the type is empty, white spaces, or greater than 50 characters.</exception>
        /// <exception cref="InvalidPriceException">Thrown if the precision or scale of the price of the watch is not correct.</exception>
        public int AddWatch(Watch toAdd)
        {
            if (toAdd.Name == null)
                throw new ArgumentNullException("Cannot create a watch with a null name.");
            if (toAdd.Name == "" || toAdd.Name.Length > 50 || toAdd.Name.Trim().Length == 0)
                throw new InvalidNameException("Invalid name, cannot be empty, white spaces, or greater than 50 characters.");
            if (toAdd.Type == null)
                throw new ArgumentNullException("Cannot create a watch with a null type.");
            if (toAdd.Type == "" || toAdd.Type.Length > 50 || toAdd.Type.Trim().Length == 0)
                throw new InvalidTypeException("Invalid type, cannot be empty, white spaces, or greater than 50 characters.");

            string priceStr = toAdd.Price.ToString();

            if (priceStr.Length > 6 || priceStr.Substring(priceStr.IndexOf('.') + 1).Length > 2)
                throw new InvalidPriceException("Invalid Price, 5 digit precision, 2 digit scale."); 

            return _watchRepo.AddWatch(toAdd);
        }

        /// <summary>
        /// Gets a watch by its id.
        /// </summary>
        /// <param name="id">The id of the watch to be searched.</param>  
        /// <returns>The watch with the corresponding id.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public Watch GetWatchById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id, cannot be <= 0.");

            return _watchRepo.GetWatchById(id);
        }

        /// <summary>
        /// Gets a watch by its name.
        /// </summary>
        /// <param name="name">The name of the watch to be searched.</param>  
        /// <returns>The watch with the corresponding name.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the name to search by is null</exception>
        /// <exception cref="InvalidNameException">Thrown if the name to search by is empty or white space.</exception>
        public Watch GetWatchByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Cannot search by null name.");
            if (name == "" || name.Length > 50 || name.Trim().Length == 0)
                throw new InvalidNameException("Invalid name, cannot be empty, white spaces, or too long.");

            return _watchRepo.GetWatchByName(name);
        }

        /// <summary>
        /// Gets all watches.
        /// </summary>
        /// <returns>A list of all watches.</returns>
        public List<Watch> GetAllWatches()
        {
            return _watchRepo.GetAllWatches();
        }

        /// <summary>
        /// Gets a list of watches of a particular type.
        /// </summary>
        /// <param name="type">The type of the watches to be searched.</param>  
        /// <returns>A list of watches with the corresponding type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the type to search by is null</exception>
        /// <exception cref="InvalidNameException">Thrown if the type to search by is empty or white space.</exception>
        public List<Watch> GetWatchesByType(string type)
        {
            if (type == null)
                throw new ArgumentNullException("Cannot search by null type.");
            if (type == "" || type.Length > 50 || type.Trim().Length == 0)
                throw new InvalidNameException("Invalid type, cannot be empty, white spaces, or too long.");

            return _watchRepo.GetWatchesByType(type);
        }

        /// <summary>
        /// Gets the quantity of a watch by an order id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>A list of watch quantities with the associated order id.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public List<int> GetWatchQuantityByOrderId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Id cannot be less than or equal to 0.");

            return _watchRepo.GetWatchQuantityByOrderId(id);
        }

        /// <summary>
        /// Gets all watches of a particular order by an order id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>A list of watches with the associated order id.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public List<Watch> GetWatchesByOrderId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Id cannot be less than or equal to 0.");

            return _watchRepo.GetWatchesByOrderId(id);
        }

        /// <summary>
        /// Gets watch with its corresponding quantity by its order id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>A dictionary of each watch with its corresponding quantity with the associated order id.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public Dictionary<Watch, int> GetWatchQuantityPair(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Id cannot be less than or equal to 0.");

            List<Watch> watches = GetWatchesByOrderId(id);
            List<int> quantities = GetWatchQuantityByOrderId(id);
            Dictionary<Watch, int> toReturn = new Dictionary<Watch, int>();

            for (int i = 0; i < watches.Count; i++)
            {
                if (!toReturn.ContainsKey(watches[i]))
                {
                    toReturn.Add(watches[i], quantities[i]);
                }
            }

            return toReturn;

        }

        /// <summary>
        /// Gets a watch by its maximum price.
        /// </summary>
        /// <param name="max">The maximum price of the watch to be searched.</param>  
        /// <returns>A list of watches with a price less than or equal to the maximum price.</returns>
        /// <exception cref="InvalidPriceException">Thrown if the precision or scale of the price of the watch is not correct.</exception>
        public List<Watch> GetWatchesByPrice(decimal max)
        {
            string maxStr = max.ToString();

            if (maxStr.Length > 6 || maxStr.Substring(maxStr.IndexOf('.') + 1).Length > 2)
                throw new InvalidPriceException("Invalid Total, 5 digit precision, 2 digit scale.");

            return _watchRepo.GetWatchesByPrice(max);
        }

        /// <summary>
        /// Edits a particular watch.
        /// </summary>
        /// <param name="toEdit">The watch to be edited.</param>  
        /// <exception cref="ArgumentNullException">Thrown if any property of the watch is null.</exception>
        /// <exception cref="InvalidNameException">Thrown if the name of the watch is empty or white space.</exception>
        /// <exception cref="InvalidTypeException">Thrown if the type of the watch is empty or white space.</exception>
        /// <exception cref="InvalidPriceException">Thrown if the precision or scale of the price of the watch is not correct.</exception>
        public void EditWatch(Watch toEdit)
        {
            if (toEdit.Name == null)
                throw new ArgumentNullException("Cannot create a watch with a null name.");
            if (toEdit.Name == "" || toEdit.Name.Length > 50 || toEdit.Name.Trim().Length == 0)
                throw new InvalidNameException("Invalid name, cannot be empty, white spaces, or greater than 50 characters.");
            if (toEdit.Type == null)
                throw new ArgumentNullException("Cannot create a watch with a null type.");
            if (toEdit.Type == "" || toEdit.Type.Length > 50 || toEdit.Type.Trim().Length == 0)
                throw new InvalidTypeException("Invalid type, cannot be empty, white spaces, or greater than 50 characters.");

            string priceStr = toEdit.Price.ToString();

            if (priceStr.Length > 6 || priceStr.Substring(priceStr.IndexOf('.') + 1).Length > 2)
                throw new InvalidPriceException("Invalid Price, 5 digit precision, 2 digit scale.");

            _watchRepo.EditWatch(toEdit);
        }

        /// <summary>
        /// Deletes a watch by its id.
        /// </summary>
        /// <param name="id">The id of the watch to be deleted.</param>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public void DeleteWatch(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id, cannot be <= 0.");

            _watchRepo.DeleteWatch(id);
        }

        /// <summary>
        /// Adds an order.
        /// </summary>
        /// <param name="toAdd">The order to be added.</param>  
        /// <returns>The order that was added.</returns>
        /// <exception cref="ArgumentNullException">Thrown if any property of the order is null.</exception>
        /// <exception cref="InvalidNameException">Thrown if the name, city, address, or email of the order is empty or white space.</exception>
        /// <exception cref="InvalidPriceException">Thrown if the precision or scale of the price of the order is not correct.</exception>
        public int AddOrder(Order toAdd)
        {
            if (toAdd.Name == null)
                throw new ArgumentNullException("Name is null.");
            if (toAdd.Name == "" || toAdd.Name.Trim().Length == 0)
                throw new InvalidNameException("Invalid name, cannot be empty or white spaces.");
            if (toAdd.City == null)
                throw new ArgumentNullException("City is null.");
            if (toAdd.City == "" || toAdd.City.Trim().Length == 0)
                throw new InvalidNameException("Invalid city, cannot be empty or white spaces.");
            if (toAdd.DeliveryAddress == null)
                throw new ArgumentNullException("Address is null.");
            if (toAdd.DeliveryAddress == "" || toAdd.DeliveryAddress.Trim().Length == 0)
                throw new InvalidNameException("Invalid address, cannot be empty or white spaces.");
            if (toAdd.Email == null)
                throw new ArgumentNullException("Email is null.");
            if (toAdd.Email == "" || toAdd.Email.Trim().Length == 0)
                throw new InvalidNameException("Invalid email, cannot be empty or white spaces.");

            string totalStr = toAdd.Total.ToString();

            if (totalStr.Length > 6 || totalStr.Substring(totalStr.IndexOf('.') + 1).Length > 2)
                throw new InvalidPriceException("Invalid Total, 5 digit precision, 2 digit scale.");

            foreach (OrderDetail od in toAdd.OrderDetails)
            {
                if (od.WatchId <= 0)
                    throw new InvalidIdException("Invalid Id, cannot be <= 0.");
                if (od.Quantity <= 0)
                    throw new InvalidQuantityException("Invalid quantity, cannot be <= 0.");
            }

            toAdd.Purchaser = _userRepo.GetUserByUsername(toAdd.Purchaser.Username);

            return _orderRepo.AddOrder(toAdd);
        }

        /// <summary>
        /// Gets an order by its id.
        /// </summary>
        /// <param name="id">The id of the order to be searched.</param>  
        /// <returns>The order with the corresponding id.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public Order GetOrderById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id, cannot be less than or equal to 0.");

            return _orderRepo.GetOrderById(id);
        }

        /// <summary>
        /// Gets all orders for a particular user.
        /// </summary>
        /// <returns>A list of all orders for a particular user.</returns>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public List<Order> GetOrdersByUserId(int curUserId)
        {
            if (curUserId <= 0)
                throw new InvalidIdException("Id cannot be less than or equal to 0.");

            return _orderRepo.GetOrdersByUserId(curUserId);
        }

        /// <summary>
        /// Gets all orders. For Admin user use.
        /// </summary>
        /// <returns>A list of all orders added.</returns>
        public List<Order> GetAllOrders()
        {
            return _orderRepo.GetAllOrders();
        }

        /// <summary>
        /// Deletes an order by its id.
        /// </summary>
        /// <param name="id">The id of the order to be deleted.</param>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public void DeleteOrder(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id, cannot be less than or equal to 0.");

            _orderRepo.DeleteOrder(id);
        }

        /// <summary>
        /// Deletes a user by its id.
        /// </summary>
        /// <param name="id">The id of the user to be deleted.</param>
        /// <exception cref="InvalidIdException">Thrown if the id is less than or equal to 0.</exception>
        public void DeleteUser(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id, cannot be less than or equal to 0.");

            _userRepo.DeleteUser(id);
        }
    }
}
