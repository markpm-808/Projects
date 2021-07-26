using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class OrderProdRepository : IOrderRepository
    {
        private string Path = @"C:\Data\Orders\";
        private int tally = 0;

        public List<Order> LoadOrders(string filename)
        {
            List<Order> Orders = new List<Order>();

            using (StreamReader sr = new StreamReader(Path + filename, true))
            {
                // reads header
                sr.ReadLine();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Order newOrder = new Order();
                    string[] columns = line.Split(',');
                    string combine = "";

                    if(columns.Length > 12)
                    {
                        int diff = columns.Length - 12;
                        int tally = diff;
                        int removed = 0;
                        string[] temp = new string[12];
                        var columnsList = columns.ToList();

                        for(int i = 1; i <= diff + 1; i++)
                        {
                            combine += columns[i];
                            if(tally > 0)
                            {
                                combine += ",";
                                tally--;
                            }
                            if(i != 1)
                            {
                                columnsList.Remove(columnsList[i-removed]);
                                removed++;
                            }
                        }
                        columns = columnsList.ToArray();
                        
                        for(int i = 0; i <12; i++)
                        {
                            if(i == 1)
                            {
                                temp[i] = combine;
                            }
                            else
                            {
                                temp[i] = columns[i];
                            }
                        }
                        columns = temp;
                    }
                    
                    newOrder.OrderNumber = int.Parse(columns[0]);
                    newOrder.CustomerName = columns[1];
                    newOrder.State = columns[2];
                    newOrder.TaxRate = decimal.Parse(columns[3]);
                    newOrder.ProductType = columns[4];
                    newOrder.Area = decimal.Parse(columns[5]);
                    newOrder.CostPerSquareFoot = decimal.Parse(columns[6]);
                    newOrder.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    newOrder.MaterialCost = decimal.Parse(columns[8]);
                    newOrder.LaborCost = decimal.Parse(columns[9]);
                    newOrder.Tax = decimal.Parse(columns[10]);
                    newOrder.Total = decimal.Parse(columns[11]);

                    Orders.Add(newOrder);
                }
            }
            return Orders;
        }

        public Order AddOrder(string filename, string date, Order order)
        {
            if (!File.Exists(Path + filename))
            {
                List<Order> noOrders = new List<Order>();
                CreateOrderFile(noOrders, Path + filename);
            }

            List<Order> orders = this.LoadOrders(filename);
            orders.Add(order);

            CreateOrderFile(orders, Path + filename);

            return order;
        }
        
        public Order RemoveOrder(string filename, int removeOrderNumber)
        {
            List<Order> Orders = new List<Order>();

            Orders = this.LoadOrders(filename);

            foreach (var item in Orders)
            {
                if (item.OrderNumber == removeOrderNumber)
                {
                    Orders.Remove(item);
                    CreateOrderFile(Orders, Path + filename);
                    return item;
                }
            }
            return null;
        }
        
        public Order FetchOrder(string filename, int orderNumber)
        {
            List<Order> Orders = new List<Order>();

            Orders = this.LoadOrders(filename);

            foreach (var item in Orders)
            {
                if (item.OrderNumber == orderNumber)
                {
                    return item;
                }
            }
            return null;
        }

        private string CreateCsvForOrders(Order order)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                order.OrderNumber, order.CustomerName, order.State, order.TaxRate, order.ProductType, order.Area,
                order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
        }

        private void CreateOrderFile(List<Order> orders, string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            using (StreamWriter sr = new StreamWriter(filePath))
            {
                sr.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost," +
                    "LaborCost,Tax,Total");
                foreach (var item in orders)
                {
                    item.OrderNumber = ++tally;
                    sr.WriteLine(CreateCsvForOrders(item));
                }
            }
        }
        
        public bool SaveOrders(Order order, int orderNumber, string filename)
        {
            List<Order> orders = this.LoadOrders(filename);

            for(int i = 0; i < orders.Count; i++)
            {
                if(orders[i].OrderNumber == orderNumber)
                {
                    orders[i] = order;
                    CreateOrderFile(orders, Path + filename);
                    return true;
                }
            }
            CreateOrderFile(orders, Path + filename);
            return false;
        }
    }
}