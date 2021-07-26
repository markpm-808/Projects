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
    public class SpecialsRepository : ISpecialsRepository
    {
        public void AddSpecial(Special special)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // In our db, VehiclesInsert stored procedure has @SpecialID as an int and output param
                SqlParameter param = new SqlParameter("@SpecialID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", special.Name);
                cmd.Parameters.AddWithValue("@Description", special.Description);

                cn.Open();

                cmd.ExecuteNonQuery();

                special.SpecialID = (int)param.Value;
            }
        }

        public void DeleteSpecial(int specialID)
            {
                using (var cn = new SqlConnection(Settings.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("SpecialsDelete", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SpecialID", specialID);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        public List<Special> GetAll()
            {
                List<Special> specials = new List<Special>();

                using (var cn = new SqlConnection(Settings.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("SpecialsSelectAll", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Special row = new Special();
                            row.SpecialID = (int)dr["SpecialID"];
                            row.Name = dr["Name"].ToString();
                            row.Description = dr["Description"].ToString();

                            specials.Add(row);
                        }
                    }
                }
                return specials;
            }
        }
   }

   
