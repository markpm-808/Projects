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
    public class EditOrderWorkflow
    {
        private GetOrderResponse response;
        private EditOrderResponse response2;
        DateTime inputDate;
        private static string date;
        private int number;
        string fileName;

        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Edit an Order");
                Console.WriteLine();

                Console.Write("Enter the Date of Order: ");

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

            while (true)
            {

                try
                {
                    Console.Clear();
                    Console.WriteLine("Edit an Order");
                    Console.WriteLine();
                    Console.Write("Enter Order Number: ");
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
                catch (FileNotFoundException ex )
                {
                    Console.WriteLine("Error:{0}", ex.Message);
                    Console.WriteLine("There are no open orders for the selected date");
                    Console.Write("Press any key to return to menu...");
                    Console.ReadKey();
                    break;
                }


                if (response.Success == true)
                {

                    Order EditOrder = response.Order;
                    string input;

                    Console.Clear();
                    ConsoleIO.DisplayOrders(response.Order, date);
                    Console.WriteLine();
                    Console.Write($"Enter customer name ({EditOrder.CustomerName}): ");
                    if ((input = Console.ReadLine()) != "")
                    {
                        EditOrder.CustomerName = input;
                    }
                    Console.Write($"Enter state ({EditOrder.State}): ");
                    if ((input = Console.ReadLine()) != "")
                    {
                        EditOrder.State = input.ToUpper();
                    }

                    List<Product> products = ProductTypeList.GetProductType();

                    while (true)
                    {
                        Product newProduct = ConsoleIO.DisplayProductTypes(products, EditOrder.ProductType);

                        if (newProduct == null)
                        {
                            Console.WriteLine("Please enter a valid product type");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (newProduct.ProductType == null)
                        {
                            break;
                        }
                        else
                        {
                            EditOrder.ProductType = newProduct.ProductType;
                            EditOrder.CostPerSquareFoot = newProduct.CostPerSquareFoot;
                            EditOrder.LaborCostPerSquareFoot = newProduct.LaborCostPerSquareFoot;
                            break;
                        }

                    }
                    while(true)
                    {
                        try
                        {
                            Console.Write($"Enter an Area in square feet ({EditOrder.Area}): ");
                            if ((input = Console.ReadLine()) != "")
                            {
                                EditOrder.Area = decimal.Parse(input);
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
                    Console.WriteLine("Attempting to edit order...");
                    response2 = manager.EditOrder(EditOrder);

                    if (response2.Success == true)
                    {
                        EditOrder = RecalculateOrder.Recalculate(response2);
                    }
                    else
                    {
                        Console.WriteLine($"{response2.Message}");
                        Console.Write("Press any key to return to menu...");
                        Console.ReadKey();
                        return;
                    }


                    while (true)
                    {
                        Console.Clear();
                        ConsoleIO.DisplayOrders(EditOrder, date);
                        Console.WriteLine();
                        Console.Write("Are you sure you would like to edit this order? (Y/N): ");
                        string confirmation;

                        switch (confirmation = Console.ReadLine())
                        {
                            case "Y":
                            case "y":

                                response2 = manager.SaveEditOrder(fileName, number, EditOrder);

                                if (response2.Success == true)
                                {
                                    Console.WriteLine("Item edited");
                                    Console.Write("Press any key to continue...");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine($"ERROR: {response2.Message}");
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
