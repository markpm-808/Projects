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
    public class BodyStylesRepository : IBodyStylesRepository
    {
        public List<BodyStyle> GetAll()
        {
            List<BodyStyle> styles = new List<BodyStyle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyStylesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyle currentRow = new BodyStyle();
                        currentRow.BodyStyleID = (int)dr["BodyStyleID"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();

                        styles.Add(currentRow);

                    }
                }
            }
            return styles;
        }
    }
}
