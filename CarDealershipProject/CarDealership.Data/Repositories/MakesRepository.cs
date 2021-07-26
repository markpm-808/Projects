using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories
{
    public class MakesRepository : IMakesRepository
    {
        public List<Make> GetAll()
        {
            List<Make> makes = new List<Make>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Make currentRow = new Make();
                        currentRow.MakeID = (int)dr["MakeID"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.UserEmail = dr["Email"].ToString();

                        makes.Add(currentRow);

                    }
                }
            }
            return makes;
        }

        public void Insert(Make make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // In our db, VehiclesInsert stored procedure has @SpecialID as an int and output param
                SqlParameter param = new SqlParameter("@MakeID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@UserID", this.GetUserID(make.UserEmail));

                cn.Open();

                cmd.ExecuteNonQuery();

                make.MakeID = (int)param.Value;
            }
        }
        public string GetUserID(string userEmail)
        {
            string userID = "";

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UsersGetID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", userEmail);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        userID = dr["Id"].ToString();
                    }
                }
            }
            return userID;
        }

    }
}
