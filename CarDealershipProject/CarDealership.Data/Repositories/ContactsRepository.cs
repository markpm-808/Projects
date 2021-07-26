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
    public class ContactsRepository : IContactsRepository
    {
        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact currentRow = new Contact();
                        currentRow.ContactID = (int)dr["ContactID"];
                        currentRow.Name = dr["Name"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.PhoneNumber = dr["PhoneNumber"].ToString();
                        currentRow.Message = dr["Message"].ToString();

                        contacts.Add(currentRow);

                    }
                }
            }
            return contacts;
        }
    
        public void Insert(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // In our db, VehiclesInsert stored procedure has @SpecialID as an int and output param
                SqlParameter param = new SqlParameter("@ContactID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", contact.Name);
                if(string.IsNullOrEmpty(contact.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                }

                if (string.IsNullOrEmpty(contact.PhoneNumber))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                }
                cmd.Parameters.AddWithValue("@Message", contact.Message);


                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactID = (int)param.Value;
            }
        }
    }
}
