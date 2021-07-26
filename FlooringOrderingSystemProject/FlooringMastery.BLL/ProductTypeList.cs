using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlooringMastery.BLL
{
    public class ProductTypeList
    {
        private static string _Path = @"C:\Data\Orders\Products.txt";

        public static List<Product> GetProductType()
        {
            List<Product> products = new List<Product>();

            using (StreamReader sr = new StreamReader(_Path, true))
            {
                sr.ReadLine();
                string line;
                
                while((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');

                    Product newProduct = new Product();

                    newProduct.ProductType = columns[0];
                    newProduct.CostPerSquareFoot = decimal.Parse(columns[1]);
                    newProduct.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    products.Add(newProduct);
                }
            }
            return products;
        }
    }
}
