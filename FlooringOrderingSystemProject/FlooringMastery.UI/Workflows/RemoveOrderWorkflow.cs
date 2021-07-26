using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        private GetOrderResponse response;
        private RemoveOrderResponse response2;
        private static string date;
        DateTime inputDate;
        private int number;
        string fileName;
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Remove an Order");
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

            while(true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Remove an Order");
                    Console.WriteLine();
                    Console.Write("Enter order number: ");
                    number = int.Parse(Console.ReadLine());
                    response = manager.GetOrder(fileName, number);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error:{0}", ex.Message);
                    Console.WriteLine("Please enter the order number as an integer value");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Error:{0}", ex.Message);
                    Console.WriteLine("There are no open orders for the selected date");
                    Console.Write("Press any key to return to menu...");
                    Console.ReadKey();
                    break;
                }

                if (response.Success == true)
                {
                    while (true)
                    {
                        Console.Clear();
                        ConsoleIO.DisplayOrders(response.Order, date);
                        Console.WriteLine();
                        Console.Write("Are you sure you would like to remove this order? (Y/N): ");
                        string confirmation;

                        switch (confirmation = Console.ReadLine())
                        {
                            case "Y":
                            case "y":
                                response2 = manager.RemoveOrder(fileName, number);
                                if (response2.Success == true)
                                {
                                    Console.WriteLine("Item removed");
                                    Console.Write("Press any key to continue...");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("ERROR");
                                    Console.Write("Press any key to continue...");
                                    Console.ReadKey();
                                    return;
                                }

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
                else
                {
                    Console.WriteLine("Sorry, order number does not exist");
                    Console.Write("Press any key to return to menu...");
                    Console.ReadKey();
                    return;
                }
            }
        }

    }
}
