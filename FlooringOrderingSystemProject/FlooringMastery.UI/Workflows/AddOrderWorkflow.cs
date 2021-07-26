using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class AddOrderWorkflow
    {
        DateTime inputDate;
        private static string date;
        string fileName;

        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Order newOrder = new Order();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add an Order");
                Console.WriteLine();

                Console.Write("Enter Order Date: ");

                if (DateTime.TryParse(Console.ReadLine(), out inputDate))
                {
                    date = inputDate.ToString("MMddyyyy");
                    fileName = $"Orders_{date}.txt";
                    break;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect date");
                    Console.Write("Press any key to try again...");
                    Console.ReadKey();
                }
            }

            Console.Write("Enter Customer Name: "); 
            string input = Console.ReadLine();
            newOrder.CustomerName = input;

            Console.Write("Enter State: ");
            newOrder.State = Console.ReadLine();

            List<Product> products = ProductTypeList.GetProductType();

            while (true)
            {
                Product newProduct = ConsoleIO.DisplayProductTypes(products, newOrder.ProductType);

                if (newProduct == null)
                {
                    Console.WriteLine("Please enter a valid product type");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (newProduct.ProductType == null)
                {
                    Console.WriteLine("Please enter a product type");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    newOrder.ProductType = newProduct.ProductType;
                    newOrder.CostPerSquareFoot = newProduct.CostPerSquareFoot;
                    newOrder.LaborCostPerSquareFoot = newProduct.LaborCostPerSquareFoot;
                    break;
                }

            }

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter Area in square feet: ");

                    if ((input = Console.ReadLine()) == "")
                    {
                        Console.WriteLine("Error: Please enter the area as an integer value");
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        newOrder.Area = decimal.Parse(input);
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please enter the area as an integer value");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                break;
            }

            Console.WriteLine();
            Console.WriteLine("Attempting to add order...");
            AddOrderResponse response = manager.AddOrder(fileName, date, newOrder);

            if(response.Success == true)
            {
                newOrder = RecalculateOrder.Recalculate(response);
            }
            else
            {
                Console.WriteLine($"{response.Message}");
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                return;
            }
            
            
            while (true)
                {
                    Console.Clear();
                    ConsoleIO.DisplayOrders(newOrder, date);   
                    Console.WriteLine();
                    Console.Write("Are you sure you would like to Add this order? (Y/N): ");
                    string confirmation;

                    switch (confirmation = Console.ReadLine())
                    {
                        case "Y":
                        case "y":
                        
                            manager.SaveAddOrder(fileName, date, newOrder);
                        
                            Console.WriteLine("Item saved");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            return;
                       
                    case "N":
                        case "n":
                            Console.WriteLine("Returning to Menu");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            return;
                        default:
                            Console.WriteLine("Please enter Y or N");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }

                }
        }
    }
}
