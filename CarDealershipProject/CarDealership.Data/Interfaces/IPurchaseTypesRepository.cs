using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarDealership.Models.Tables;

namespace CarDealership.Data.Interfaces
{
    public interface IPurchaseTypesRepository
    {
        List<PurchaseType> GetAll();
    }
}
