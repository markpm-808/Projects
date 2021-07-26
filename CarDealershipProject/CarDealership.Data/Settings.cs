using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class Settings
    {
        private static string _connectonString;
        private static string _repositoryType;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectonString))
            {
                _connectonString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
           
         return _connectonString;
        }

            public static string GetRepositoryType()
            {
                if(string.IsNullOrEmpty(_repositoryType))
                {
                    _repositoryType = ConfigurationManager.AppSettings["Mode"].ToString();
                }
             return _repositoryType;
            }
    }
}
