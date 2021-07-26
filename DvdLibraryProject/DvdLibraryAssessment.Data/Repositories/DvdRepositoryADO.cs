using DvdLibraryAssessment.Data.Interfaces;
using DvdLibraryAssessment.Models.Queries;
using DvdLibraryAssessment.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAssessment.Data.ADO
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public List<Dvd> GetAll()
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.title = dr["Title"].ToString();
                        currentRow.releaseYear = (int)dr["ReleaseYear"];
                        currentRow.director = dr["Director"].ToString();
                        currentRow.rating = dr["Rating"].ToString();
                        currentRow.notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public List<Dvd> GetByDirector(string director)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByDirector", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Director", director);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.title = dr["Title"].ToString();
                        currentRow.releaseYear = (int)dr["ReleaseYear"];
                        currentRow.director = dr["Director"].ToString();
                        currentRow.rating = dr["Rating"].ToString();
                        currentRow.notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public Dvd GetById(int dvdId)
        {
            Dvd dvd = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dvd = new Dvd();
                        dvd.id = (int)dr["DvdId"];
                        dvd.title = dr["Title"].ToString();
                        dvd.releaseYear = (int)dr["ReleaseYear"];
                        dvd.director = dr["Director"].ToString();
                        dvd.rating = dr["Rating"].ToString();
                        dvd.notes = dr["Notes"].ToString();
                    }
                }
            }
            return dvd;
        }

        public List<Dvd> GetByRating(string rating)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByRating", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Rating", rating);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.title = dr["Title"].ToString();
                        currentRow.releaseYear = (int)dr["ReleaseYear"];
                        currentRow.director = dr["Director"].ToString();
                        currentRow.rating = dr["Rating"].ToString();
                        currentRow.notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public List<Dvd> GetByTitle(string title)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", title);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.title = dr["Title"].ToString();
                        currentRow.releaseYear = (int)dr["ReleaseYear"];
                        currentRow.director = dr["Director"].ToString();
                        currentRow.rating = dr["Rating"].ToString();
                        currentRow.notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public List<Dvd> GetByYear(int year)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByYear", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReleaseYear", year);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.title = dr["Title"].ToString();
                        currentRow.releaseYear = (int)dr["ReleaseYear"];
                        currentRow.director = dr["Director"].ToString();
                        currentRow.rating = dr["Rating"].ToString();
                        currentRow.notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }
            return dvds;
        }

        public void Insert(Dvd dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", dvd.title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.releaseYear);

                if (string.IsNullOrEmpty(dvd.director))
                {
                    cmd.Parameters.AddWithValue("@Director", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Director", dvd.director);
                }

                if (string.IsNullOrEmpty(dvd.rating))
                {
                    cmd.Parameters.AddWithValue("@Rating", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Rating", dvd.rating);
                }

                if (string.IsNullOrEmpty(dvd.notes))
                {
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Notes", dvd.notes);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

                dvd.id = (int)param.Value;
            }
        }
        public void Update(Dvd dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvd.id);
                cmd.Parameters.AddWithValue("@Title", dvd.title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.releaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.director);
                cmd.Parameters.AddWithValue("@Rating", dvd.rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.notes);
          
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int dvdId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<Dvd> Search(DvdSearchParameters parameters)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT DvdId, Title, ReleaseYear, Director, Rating, Notes FROM Dvds WHERE ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                //make switch
                
                if (parameters.SearchCategory == "title")
                {
                    query += "Title LIKE @Title";
                    cmd.Parameters.AddWithValue("@Title", parameters.SearchTerm + "%");
                }

                if (parameters.SearchCategory == "date")
                {
                    query += "ReleaseYear = @ReleaseYear ";
                    cmd.Parameters.AddWithValue("@ReleaseYear", parameters.SearchTerm);
                }

                if (parameters.SearchCategory == "director")
                {
                    query += "Director LIKE @Director ";
                    cmd.Parameters.AddWithValue("@Director", parameters.SearchTerm + "%");
                }
                
                if (parameters.SearchCategory == "rating")
                {
                    query += "Rating = @Rating";
                    cmd.Parameters.AddWithValue("@Rating", parameters.SearchTerm);
                }
                
                cmd.CommandText = query;


                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd row = new Dvd();

                        row.id = (int)dr["DvdId"];
                        row.title = dr["Title"].ToString();
                        row.releaseYear = (int)dr["ReleaseYear"];
                        row.director = dr["Director"].ToString();
                        row.rating = dr["Rating"].ToString();
                        row.notes = dr["Notes"].ToString();

                      
                        dvds.Add(row);
                    }
                }
            }
            return dvds;
        }
    }
}
