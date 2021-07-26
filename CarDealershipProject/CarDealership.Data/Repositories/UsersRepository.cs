using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UsersSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        User currentRow = new User();
                        currentRow.UserID = dr["Id"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.Role = dr["RoleName"].ToString();

                        users.Add(currentRow);

                    }
                }
            }
            return users;
        }

        public string GetID(string email)
        {
            string id = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UsersGetID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        id = dr["Id"].ToString();
                  
                    }
                }
            }
            return id;
        }

        public UserEdit GetUser(string id)
        {
            UserEdit user = new UserEdit();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        user.UserID = dr["Id"].ToString();
                        user.FirstName = dr["FirstName"].ToString();
                        user.LastName = dr["LastName"].ToString();
                        user.Email = dr["Email"].ToString();
                        user.RoleID = (int)dr["RoleID"];

                    }
                }
            }
            return user;
        }

        public void InsertRole(string userID, int roleID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UserRoleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@UserId", userID);
                cmd.Parameters.AddWithValue("@RoleId", roleID);
          

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveUser(string userID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", userID);


                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
