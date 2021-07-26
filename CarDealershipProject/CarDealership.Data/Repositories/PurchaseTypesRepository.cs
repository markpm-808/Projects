using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories
{
    public class PurchaseTypesRepository : IPurchaseTypesRepository
    {
        public List<PurchaseType> GetAll()
        {
            List<PurchaseType> types = new List<PurchaseType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseType currentRow = new PurchaseType();
                        currentRow.PurchaseTypeID = (int)dr["PurchaseTypeID"];
                        currentRow.PurchaseTypeName = dr["PurchaseTypeName"].ToString();

                        types.Add(currentRow);

                    }
                }
            }
            return types;
        }
    }
}
