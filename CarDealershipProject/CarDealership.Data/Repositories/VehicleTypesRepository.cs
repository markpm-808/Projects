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
    public class VehicleTypesRepository : IVehicleTypesRepository
    {
        public List<VehicleType> GetAll()
        {
            List<VehicleType> types = new List<VehicleType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleType currentRow = new VehicleType();
                        currentRow.TypeID = (int)dr["TypeID"];
                        currentRow.TypeName = dr["TypeName"].ToString();

                        types.Add(currentRow);

                    }
                }
            }
            return types;
        }

        // DO I NEED THIS?
        public VehicleType GetByID(int typeID)
        {
            VehicleType type = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleTypesSelectByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TypeID", typeID);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        type = new VehicleType();
                        type.TypeID = (int)dr["TypeID"];
                        type.TypeName = dr["TypeName"].ToString();
                    }
                }
            }
            return type;
        }
    }
}
