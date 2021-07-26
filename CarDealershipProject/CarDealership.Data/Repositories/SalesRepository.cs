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
    public class SalesRepository : ISalesRepository
    {
        public Sale GetByID(int saleId)
        {
            Sale sale = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesSelectByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SaleID", saleId);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        sale = new Sale();
                        sale.SaleID = (int)dr["SaleID"];
                        sale.Name = dr["Name"].ToString();
                        sale.PhoneNumber = dr["PhoneNumber"].ToString();
                        sale.Email = dr["Email"].ToString();
                        sale.Address1 = dr["Address1"].ToString();
                        sale.Address2 = dr["Address2"].ToString();
                        sale.City = dr["City"].ToString();
                        sale.State = (int)dr["StateAbbr"];
                        sale.Zipcode = dr["Zipcode"].ToString();
                        sale.PurchasePrice = (decimal)dr["PurchasePrice"];
                        sale.SaleDate = (DateTime)dr["SaleDate"];
                        sale.VehicleID = (int)dr["VehicleID"];
                        sale.PurchaseType = dr["PurchaseTypeName"].ToString();
                        sale.UserID = dr["UserID"].ToString();
                    }
                }
            }
            return sale;
        }

        public void Insert(Sale sale)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // In our db, VehiclesInsert stored procedure has @SpecialID as an int and output param
                SqlParameter param = new SqlParameter("@SaleID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", sale.Name);
                cmd.Parameters.AddWithValue("@Address1", sale.Address1);
                cmd.Parameters.AddWithValue("@City", sale.City);
                cmd.Parameters.AddWithValue("@StateID", sale.State); 
                cmd.Parameters.AddWithValue("@Zipcode", sale.Zipcode);
                cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);
                cmd.Parameters.AddWithValue("@VehicleID", sale.VehicleID);
                cmd.Parameters.AddWithValue("@PurchaseTypeID", sale.PurchaseType);
                cmd.Parameters.AddWithValue("@UserID", sale.UserID);

                if (string.IsNullOrEmpty(sale.Address2))
                {
                    cmd.Parameters.AddWithValue("@Address2", "None");
                }
                
                if (string.IsNullOrEmpty(sale.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", "None");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", sale.Email);
                }

                if (string.IsNullOrEmpty(sale.PhoneNumber))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", "None");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", sale.PhoneNumber);
                }



                cn.Open();

                cmd.ExecuteNonQuery();

                sale.SaleID = (int)param.Value;
            }
        }
        

        // put code in separate class to be referenced by repositories

        public int GetPurchaseType(string purchaseTypeName)
        {
            int purchaseTypeID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesGetPurchaseType", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PurchaseTypeName", purchaseTypeName);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        purchaseTypeID = (int)dr["PurchaseTypeID"];
                    }
                }
            }
            return purchaseTypeID;
        }

        public int GetStateID(string stateName)
        {
            int stateID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("StatesGetID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StateAbbr", stateName);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        stateID = (int)dr["StateID"];
                    }
                }
            }
            return stateID;
        }


        public List<SaleReport> SearchSalesReport(SaleReportParameters parameters)
        {
            List<SaleReport> sales = new List<SaleReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
               
                string query ="SELECT u.FirstName, u.LastName, FORMAT(SUM(s.PurchasePrice),'C0') TotalSales, COUNT(v.VehicleID) TotalVehicles " +
                    "FROM AspNetUsers u " +
                    "INNER JOIN Sales s ON u.Id = s.UserID " +
                    "INNER JOIN Vehicles v ON s.VehicleID = v.VehicleID " +
                    "WHERE 1=1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(parameters.User))
                {
                    query += "AND u.id = @User ";
                    cmd.Parameters.AddWithValue("@User", parameters.User);
                }

                if (!string.IsNullOrEmpty(parameters.FromDate))
                {
                    query += "AND s.SaleDate >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }

                if (!string.IsNullOrEmpty(parameters.ToDate))//parameters.ToDate.HasValue
                {
                    query += "AND s.SaleDate <= @ToDate ";
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }
                
                query += "GROUP BY u.FirstName,u.LastName ORDER BY TotalSales DESC";
                cmd.CommandText = query;


                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SaleReport row = new SaleReport();

                        row.FirstName = dr["FirstName"].ToString();
                        row.LastName = dr["LastName"].ToString();
                        row.TotalSales = dr["TotalSales"].ToString();
                        row.TotalVehicles = (int)dr["TotalVehicles"];
                 
                        sales.Add(row);
                    }
                }
            }
            return sales;
        }

        public bool CheckSale(int vehicleID)
        {
          
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CheckSale", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleID", vehicleID);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
