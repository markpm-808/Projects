using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringMastery.Data;

namespace FlooringMastery.BLL
{
    //created to instantiate OrderManager objects
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch(mode)
            {
                case "Test":
                    return new OrderManager(new OrderTestRepository());
                case "Prod":
                    return new OrderManager(new OrderProdRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
