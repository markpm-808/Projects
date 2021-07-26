using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringMastery.UI.Workflows
{
    public class DisplayOrderWorkflow
    {
        private DisplayOrderResponse response;

        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            DateTime inputDate;

            while(true)
            {
                Console.Clear();
                Console.WriteLine("Display Orders");
                Console.WriteLine();
                Console.Write("Enter date of order: ");

                if (DateTime.TryParse(Console.ReadLine(), out inputDate))
                {
                    string date = inputDate.ToString("MMddyyyy");

                    string fileName = $"Orders_{date}.txt";
                    
                    try
                    {
                        response = manager.DisplayOrders(fileName);
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                        Console.WriteLine("Please check that path to file is valid");
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }

                    if (response.Success == true)
                    {
                        ConsoleIO.DisplayOrders(response.OrderList, date);
                        Console.Write("Press any key to return to menu...");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, there are no open orders for the current date");
                        Console.Write("Press any key to return to menu...");
                        Console.ReadKey();
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect date");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
