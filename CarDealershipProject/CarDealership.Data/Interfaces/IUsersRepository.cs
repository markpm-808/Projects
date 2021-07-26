using CarDealership.Models.Queries;
using CarDealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        //void Insert(User user);
        void RemoveUser(string userID);
        void InsertRole(string userID, int roleID);
        string GetID(string email);
        UserEdit GetUser(string id);
    }
}
