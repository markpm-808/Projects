using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class ConsoleIO
    {
        private static DateTime _date;

        public static Product DisplayProductTypes(List<Product> products, string productType)
        {
            Product newProduct = new Product();

            Console.WriteLine();
            Console.WriteLine($"| {"Product Type",-17}| {"Cost Per Square foot",-20}| {"Labor Cost Per Square Foot",-15}");

            foreach (var item in products)
            {
                Console.WriteLine(ProductFormat(item));
            }
            Console.WriteLine();
            
            if(productType != null)
            {
                Console.Write($"Enter a product type from the list above ({productType}):");
            }
            else
            {
                Console.Write($"Enter a product type from the list above:");
            }
            string input = Console.ReadLine();

            if(input == "")
            {
                return new Product();
            }
           foreach(var item in products)
            {
                if(item.ProductType.ToLower() == input.ToLower())
                {
                    newProduct.ProductType = item.ProductType;
                    newProduct.CostPerSquareFoot = item.CostPerSquareFoot;
                    newProduct.LaborCostPerSquareFoot = item.LaborCostPerSquareFoot;
                    return newProduct;
                }
            }
            return null;
        }
        
        private static string ProductFormat(Product product)
        {
            return string.Format($"  {product.ProductType,-19}{product.CostPerSquareFoot,-20}{ product.LaborCostPerSquareFoot,6}");
        }
        
        public static void DisplayOrders(List<Order> list, string date)
        {
            _date = DateTime.ParseExact(date, "MMddyyyy",null);
            
            foreach (var item in list)
            {
                decimal materials = item.MaterialCost;
                decimal labor = item.LaborCost;
                decimal tax = item.Tax;
                decimal total = item.Total;

                Console.WriteLine();
                Console.WriteLine($"{item.OrderNumber} | {_date.ToString("MM-dd-yyyy")}");
                Console.WriteLine($"{item.CustomerName}");
                Console.WriteLine($"{item.State}");
                Console.WriteLine($"Product: {item.ProductType}");
                Console.WriteLine("Materials: {0:C}", materials);
                Console.WriteLine("Labor: {0:C}", labor);
                Console.WriteLine("Tax: {0:C}", tax);
                Console.WriteLine("Total: {0:C}", total);
                Console.WriteLine();
                Console.WriteLine("**********************************************");
            }
        }

        public static void DisplayOrders(Order singleOrder, string date)
        {
            _date = DateTime.ParseExact(date, "MMddyyyy", null);

            decimal materials = singleOrder.MaterialCost;
            decimal labor = singleOrder.LaborCost;
            decimal tax = singleOrder.Tax;
            decimal total = singleOrder.Total;
            
            Console.WriteLine();
            Console.WriteLine($"{singleOrder.OrderNumber} | {_date.ToString("MM-dd-yyyy")}");
            Console.WriteLine($"{singleOrder.CustomerName}");
            Console.WriteLine($"{singleOrder.State}");
            Console.WriteLine($"Product: {singleOrder.ProductType}");
            Console.WriteLine("Materials: {0:C}", materials);
            Console.WriteLine("Labor: {0:C}", labor);
            Console.WriteLine("Tax: {0:C}", tax);
            Console.WriteLine("Total: {0:C}", total);
            Console.WriteLine();
            Console.WriteLine("**********************************************");
        }
    }
}
