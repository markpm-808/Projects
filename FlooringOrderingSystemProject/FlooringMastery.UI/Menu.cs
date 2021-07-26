﻿using FlooringMastery.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Program");
                Console.WriteLine();
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("5. Quit");
                Console.WriteLine();
                Console.Write("Enter selection: ");
                string selection = Console.ReadLine();

                switch(selection)
                {
                    case "1":
                        DisplayOrderWorkflow workflow = new DisplayOrderWorkflow();
                        workflow.Execute();
                        break;
                    case "2": AddOrderWorkflow addWorkFlow = new AddOrderWorkflow();
                        addWorkFlow.Execute();
                        break;
                    case "3": EditOrderWorkflow editWorkflow = new EditOrderWorkflow();
                        editWorkflow.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeWorkflow = new RemoveOrderWorkflow();
                        removeWorkflow.Execute();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Please enter a number between 1 and 5");
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
                
            }
        }
    }
}
