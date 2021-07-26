using CarDealership.Data.Interfaces;
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
    public class ModelsRepository : IModelsRepository
    {
        public List<Model> GetAll()
        {
            List<Model> models = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model currentRow = new Model();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.UserEmail = dr["Email"].ToString();

                        models.Add(currentRow);

                    }
                }
            }
            return models;
        }

        public void Insert(Model model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // In our db, VehiclesInsert stored procedure has @SpecialID as an int and output param
                SqlParameter param = new SqlParameter("@ModelID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@MakeID", model.MakeName);
                cmd.Parameters.AddWithValue("@UserID", this.GetUserID(model.UserEmail));

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelID = (int)param.Value;
            }
        }

        public int GetMake(string make)
        {
            int makeID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesGetMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeName", make);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        makeID = (int)dr["MakeID"];
                    }
                }
            }
            return makeID;
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
