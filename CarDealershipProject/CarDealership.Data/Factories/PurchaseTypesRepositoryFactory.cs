using CarDealership.Data.Interfaces;
using CarDealership.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Factories
{
    public static class PurchaseTypesRepositoryFactory
    {
        public static IPurchaseTypesRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new PurchaseTypesRepository();
                //case "QA":
                //    Implement in-memory repository
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
