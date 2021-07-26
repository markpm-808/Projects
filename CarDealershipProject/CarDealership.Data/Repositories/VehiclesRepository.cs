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
    public class VehiclesRepository : IVehiclesRepository
    {
        public void Delete(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public DetailsVehicle GetDetails(int vehicleId)
        {
            DetailsVehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        vehicle = new DetailsVehicle();
                        vehicle.VehicleID = (int)dr["VehicleID"];
                        vehicle.VinNumber = dr["VinNumber"].ToString();
                        vehicle.Year = (int)dr["Year"];
                        vehicle.Interior = dr["Interior"].ToString();
                        vehicle.Mileage = dr["Mileage"].ToString();
                        vehicle.Transmission = dr["Transmission"].ToString();
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.PictureFileName = dr["PictureFileName"].ToString();
                        vehicle.MakeName = dr["MakeName"].ToString();
                        vehicle.ModelName = dr["ModelName"].ToString();
                        vehicle.BodyStyleName = dr["BodyStyleName"].ToString();
                        vehicle.ColorName = dr["ColorName"].ToString();
                    }
                }
            }
            return vehicle;
        }

        public Vehicle GetByID(int vehicleId)
        {
            Vehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();
                        vehicle.VehicleID = (int)dr["VehicleID"];
                        vehicle.VinNumber = dr["VinNumber"].ToString();
                        vehicle.Year = (int)dr["Year"];
                        vehicle.Interior = dr["Interior"].ToString();
                        vehicle.Mileage = dr["Mileage"].ToString();
                        vehicle.Transmission = dr["Transmission"].ToString();
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.PictureFileName = dr["PictureFileName"].ToString();
                        vehicle.Featured = (int)dr["Featured"];
                        vehicle.MakeName = dr["MakeName"].ToString();
                        vehicle.TypeName = dr["TypeName"].ToString();
                        vehicle.ModelName = dr["ModelName"].ToString();
                        vehicle.BodyStyleName = dr["BodyStyleName"].ToString();
                        vehicle.ColorName = dr["ColorName"].ToString();
                    }
                }
            }
            return vehicle;
        }

        public vehicleEdit EditGetByID(int vehicleId)
        {
            vehicleEdit vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new vehicleEdit();
                        vehicle.VehicleID = (int)dr["VehicleID"];
                        vehicle.VinNumber = dr["VinNumber"].ToString();
                        vehicle.Year = (int)dr["Year"];
                        vehicle.Interior = dr["Interior"].ToString();
                        vehicle.Mileage = dr["Mileage"].ToString();
                        vehicle.Trans = dr["Transmission"].ToString();
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.PictureFileName = dr["PictureFileName"].ToString();
                        vehicle.Featured = (int)dr["Featured"];
                        vehicle.Make = dr["MakeName"].ToString();
                        vehicle.Type = dr["TypeName"].ToString();
                        vehicle.Model = dr["ModelName"].ToString();
                        vehicle.BodyStyle = dr["BodyStyleName"].ToString();
                        vehicle.Color = dr["ColorName"].ToString();
                    }
                }
            }
            return vehicle;
        }

        public void Insert(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // In our db, VehiclesInsert stored procedure has @VehicleID as an int and output param
                SqlParameter param = new SqlParameter("@VehicleID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Interior", vehicle.Interior);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Transmission", vehicle.Transmission);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@PictureFileName", vehicle.PictureFileName);
                cmd.Parameters.AddWithValue("@FeaturedID", vehicle.Featured);
                cmd.Parameters.AddWithValue("@MakeID", vehicle.MakeName);
                cmd.Parameters.AddWithValue("@TypeID", vehicle.TypeName);
                cmd.Parameters.AddWithValue("@ModelID", vehicle.ModelName);
                cmd.Parameters.AddWithValue("@BodyStyleID", vehicle.BodyStyleName);
                cmd.Parameters.AddWithValue("@ColorID", vehicle.ColorName);

                cn.Open();

                cmd.ExecuteNonQuery();

                vehicle.VehicleID = (int)param.Value;
            }
        }

        public void Update(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("VehicleID", vehicle.VehicleID);
                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Interior", vehicle.Interior);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Transmission", vehicle.Transmission);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@FeaturedID", vehicle.Featured);
                cmd.Parameters.AddWithValue("@MakeID", vehicle.MakeName);
                cmd.Parameters.AddWithValue("@TypeID", vehicle.TypeName);
                cmd.Parameters.AddWithValue("@ModelID", vehicle.ModelName);
                cmd.Parameters.AddWithValue("@BodyStyleID", vehicle.BodyStyleName);
                cmd.Parameters.AddWithValue("@ColorID", vehicle.ColorName);
                cmd.Parameters.AddWithValue("@PictureFileName", vehicle.PictureFileName);
                



                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<SearchVehicle> SearchNew(SearchVehicleParameters parameters)
        {
            List<SearchVehicle> vehicles = new List<SearchVehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                // 1=1 is always true so if user doesn't enter anything in form, nothing will be filtered
                // so top 20 will always be shown. 
                string query = "SELECT TOP 20 v.VehicleID, b.BodyStyleName, c.ColorName, ma.MakeName, mo.ModelName,Year,FORMAT(MSRP,'c0') MSRP," +
                    "FORMAT(SalePrice,'c0') SalePrice, PictureFileName, Transmission, Interior, Mileage, VinNumber  FROM Vehicles v " +
                    "LEFT JOIN Makes ma ON v.MakeID = ma.MakeID LEFT JOIN Models mo ON v.ModelID = mo.ModelID " +
                    "LEFT JOIN BodyStyles b ON v.BodyStyleID = b.BodyStyleID LEFT JOIN Colors c ON V.ColorID = C.ColorID " +
                    "WHERE 1 = 1 AND Mileage = 'New' ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND SalePrice >= @MinPrice ";
                    // we use the parameter @MinRate to avoid sql injection attack. This will allow
                    // user input to be treated as a string and not as a sql command. 
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear != 0)
                {
                    query += "AND Year >= @MinYear ";
                    // we use the parameter @MinRate to avoid sql injection attack. This will allow
                    // user input to be treated as a string and not as a sql command. 
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (parameters.MaxYear != 0)
                {
                    query += "AND Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (!string.IsNullOrEmpty(parameters.Make))
                {
                    // this LIKE condition with % allows for conditional matching 
                    // i.e. you only have to put part of city name in to get results for similar cities
                    query += "AND ma.MakeName LIKE @Make ";
                    cmd.Parameters.AddWithValue("@Make ", parameters.Make + "%");
                }

                if (!string.IsNullOrEmpty(parameters.Model))
                {
                    query += "AND mo.ModelName LIKE @Model ";
                    cmd.Parameters.AddWithValue("@Model", parameters.Model + "%");
                }

                if (parameters.Year != 0)
                {
                    query += "AND Year LIKE @Year ";
                    cmd.Parameters.AddWithValue("@Year", parameters.Year + "%");
                }


                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;


                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SearchVehicle row = new SearchVehicle();

                        row.VehicleID = (int)dr["VehicleID"];
                        row.MakeName = dr["MakeName"].ToString();
                        row.ModelName = dr["ModelName"].ToString();
                        row.BodyStyleName = dr["BodyStyleName"].ToString();
                        row.ColorName = dr["ColorName"].ToString();
                        row.Year = (int)dr["Year"];
                        row.MSRP = dr["MSRP"].ToString();
                        row.SalePrice = dr["SalePrice"].ToString();
                        row.PictureFileName = dr["PictureFileName"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.Interior = dr["Interior"].ToString();
                        row.Mileage = dr["Mileage"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();
                       
                        vehicles.Add(row);
                    }
                }
            }
            return vehicles;
        }

        public List<SearchVehicle> SearchUsed(SearchVehicleParameters parameters)
        {
            List<SearchVehicle> vehicles = new List<SearchVehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                // 1=1 is always true so if user doesn't enter anything in form, nothing will be filtered
                // so top 20 will always be shown. 
                string query = "SELECT TOP 20 v.VehicleID, b.BodyStyleName, c.ColorName, ma.MakeName, mo.ModelName,Year,FORMAT(MSRP,'c0') MSRP," +
                    "FORMAT(SalePrice,'c0') SalePrice, PictureFileName, Transmission, Interior, Mileage, VinNumber  FROM Vehicles v " +
                    "LEFT JOIN Makes ma ON v.MakeID = ma.MakeID LEFT JOIN Models mo ON v.ModelID = mo.ModelID " +
                    "LEFT JOIN BodyStyles b ON v.BodyStyleID = b.BodyStyleID LEFT JOIN Colors c ON V.ColorID = C.ColorID " +
                    "WHERE 1 = 1 AND Mileage != 'New' ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND SalePrice >= @MinPrice ";
                    // we use the parameter @MinRate to avoid sql injection attack. This will allow
                    // user input to be treated as a string and not as a sql command. 
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear != 0)
                {
                    query += "AND Year >= @MinYear ";
                    // we use the parameter @MinRate to avoid sql injection attack. This will allow
                    // user input to be treated as a string and not as a sql command. 
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (parameters.MaxYear != 0)
                {
                    query += "AND Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (!string.IsNullOrEmpty(parameters.Make))
                {
                    // this LIKE condition with % allows for conditional matching 
                    // i.e. you only have to put part of city name in to get results for similar cities
                    query += "AND ma.MakeName LIKE @Make ";
                    cmd.Parameters.AddWithValue("@Make ", parameters.Make + "%");
                }

                if (!string.IsNullOrEmpty(parameters.Model))
                {
                    query += "AND mo.ModelName LIKE @Model ";
                    cmd.Parameters.AddWithValue("@Model", parameters.Model + "%");
                }

                if (parameters.Year != 0)
                {
                    query += "AND Year LIKE @Year ";
                    cmd.Parameters.AddWithValue("@Year", parameters.Year + "%");
                }


                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;


                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SearchVehicle row = new SearchVehicle();

                        row.VehicleID = (int)dr["VehicleID"];
                        row.MakeName = dr["MakeName"].ToString();
                        row.ModelName = dr["ModelName"].ToString();
                        row.BodyStyleName = dr["BodyStyleName"].ToString();
                        row.ColorName = dr["ColorName"].ToString();
                        row.Year = (int)dr["Year"];
                        row.MSRP = dr["MSRP"].ToString();
                        row.SalePrice = dr["SalePrice"].ToString();
                        row.PictureFileName = dr["PictureFileName"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.Interior = dr["Interior"].ToString();
                        row.Mileage = dr["Mileage"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();

                        vehicles.Add(row);
                    }
                }
            }
            return vehicles;
        }

        public List<FeaturedVehicle> GetAllFeaturedVehicles()
        {
            List<FeaturedVehicle> featured = new List<FeaturedVehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectAllFeatured", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FeaturedVehicle currentRow = new FeaturedVehicle();
                        currentRow.VehicleID = (int)dr["VehicleID"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.PictureFileName = dr["PictureFileName"].ToString();

                        featured.Add(currentRow);

                    }
                }
            }
            return featured;
        }

        public int GetFeatured(bool featured)
        {
            int featuredID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesGetFeatured", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Featured", featured);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        featuredID = (int)dr["FeaturedID"];
                    }
                }
            }
            return featuredID;
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

        public int GetModel(string model)
        {
            int modelID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesGetModel", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModelName", model);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        modelID = (int)dr["ModelID"];
                    }
                }
            }
            return modelID;
        }

        public int GetBodyStyle(string bodyStyle)
        {
            int bodyStyleID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesGetBodyStyle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BodyStyleName", bodyStyle);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        bodyStyleID = (int)dr["BodyStyleID"];
                    }
                }
            }
            return bodyStyleID;
        }

        public int GetColor(string color)
        {
            int colorID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesGetColor", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ColorName", color);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        colorID = (int)dr["ColorID"];
                    }
                }
            }
            return colorID;
        }

        public int GetType(string type)
        {
            int typeID = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesGetType", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TypeName", type);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        typeID = (int)dr["TypeID"];
                    }
                }
            }
            return typeID;
        }

        public List<InventoryReport> GetInventoryNew()
        {
            List<InventoryReport> vehicles = new List<InventoryReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesInventoryGetNew", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReport currentRow = new InventoryReport();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["MakeName"].ToString();
                        currentRow.Model = dr["ModelName"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["StockValue"];

                        vehicles.Add(currentRow);

                    }
                }
            }
            return vehicles;
        }

        public List<InventoryReport> GetInventoryUsed()
        {
            List<InventoryReport> vehicles = new List<InventoryReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesInventoryGetUsed", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReport currentRow = new InventoryReport();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["MakeName"].ToString();
                        currentRow.Model = dr["ModelName"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["StockValue"];

                        vehicles.Add(currentRow);

                    }
                }
            }
            return vehicles;
        }

        public List<SearchVehicle> SearchSales(SearchVehicleParameters parameters)
        {
            List<SearchVehicle> vehicles = new List<SearchVehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                // 1=1 is always true so if user doesn't enter anything in form, nothing will be filtered
                // so top 20 will always be shown. 
                string query = "SELECT TOP 20 v.VehicleID, b.BodyStyleName, c.ColorName, ma.MakeName, mo.ModelName,Year,FORMAT(MSRP,'c0') MSRP," +
                    "FORMAT(SalePrice,'c0') SalePrice, PictureFileName, Transmission, Interior, Mileage, VinNumber  FROM Vehicles v " +
                    "LEFT JOIN Makes ma ON v.MakeID = ma.MakeID LEFT JOIN Models mo ON v.ModelID = mo.ModelID " +
                    "LEFT JOIN BodyStyles b ON v.BodyStyleID = b.BodyStyleID LEFT JOIN Colors c ON V.ColorID = C.ColorID " +
                    "LEFT JOIN Sales s ON v.VehicleID = s.VehicleID WHERE 1 = 1 AND s.SaleID IS NULL ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND SalePrice >= @MinPrice ";
                    // we use the parameter @MinRate to avoid sql injection attack. This will allow
                    // user input to be treated as a string and not as a sql command. 
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear != 0)
                {
                    query += "AND Year >= @MinYear ";
                    // we use the parameter @MinRate to avoid sql injection attack. This will allow
                    // user input to be treated as a string and not as a sql command. 
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (parameters.MaxYear != 0)
                {
                    query += "AND Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (!string.IsNullOrEmpty(parameters.Make))
                {
                    // this LIKE condition with % allows for conditional matching 
                    // i.e. you only have to put part of city name in to get results for similar cities
                    query += "AND ma.MakeName LIKE @Make ";
                    cmd.Parameters.AddWithValue("@Make ", parameters.Make + "%");
                }

                if (!string.IsNullOrEmpty(parameters.Model))
                {
                    query += "AND mo.ModelName LIKE @Model ";
                    cmd.Parameters.AddWithValue("@Model", parameters.Model + "%");
                }

                if (parameters.Year != 0)
                {
                    query += "AND Year LIKE @Year ";
                    cmd.Parameters.AddWithValue("@Year", parameters.Year + "%");
                }


                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;


                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SearchVehicle row = new SearchVehicle();

                        row.VehicleID = (int)dr["VehicleID"];
                        row.MakeName = dr["MakeName"].ToString();
                        row.ModelName = dr["ModelName"].ToString();
                        row.BodyStyleName = dr["BodyStyleName"].ToString();
                        row.ColorName = dr["ColorName"].ToString();
                        row.Year = (int)dr["Year"];
                        row.MSRP = dr["MSRP"].ToString();
                        row.SalePrice = dr["SalePrice"].ToString();
                        row.PictureFileName = dr["PictureFileName"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.Interior = dr["Interior"].ToString();
                        row.Mileage = dr["Mileage"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();

                        vehicles.Add(row);
                    }
                }
            }
            return vehicles;
        }

        public void UpdateFileName(string vinNumber, string pictureFileName)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesUpdateFileName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", vinNumber);
                cmd.Parameters.AddWithValue("@PictureFileName", pictureFileName);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
