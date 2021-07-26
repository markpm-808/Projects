using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static List<Order> _orders = new List<Order>()
        {
            new Order
            {
                OrderNumber = 1,
                CustomerName = "Wise",
                State = "OH",
                TaxRate = 6.25M,
                ProductType = "Wood",
                Area = 100.00M,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M,
                MaterialCost = 515.00M,
                LaborCost = 475.00M,
                Tax = 61.88M,
                Total = 1051.88M
            },
            new Order
            {
                OrderNumber = 2,
                CustomerName = "Bobby",
                State = "PA",
                TaxRate = 6.75M,
                ProductType = "Tile",
                Area = 100.00M,
                CostPerSquareFoot = 3.50M,
                LaborCostPerSquareFoot = 4.15M,
                MaterialCost = 350.00M,
                LaborCost = 415.00M,
                Tax = 51.64M,
                Total = 816.64M
            }
        };

        //used to display orders
        public List<Order> LoadOrders(string fileName)
        {
            return _orders;
        }

        public Order AddOrder(string filename, string date, Order order)
        {
            _orders.Add(order);
            for (int i = 1; i <= _orders.Count; i++)
            {
                _orders[i - 1].OrderNumber = i;
            }
            return order;
        }

        public Order FetchOrder(string filename, int orderNumber)
        {
            foreach(var order in _orders)
            {
                if(order.OrderNumber == orderNumber)
                {
                    return order;
                }
            }
            return null;
        }
        
        public Order RemoveOrder(string filename, int orderNumber)
        {
            foreach(var order in _orders)
            {
                if(order.OrderNumber == orderNumber)
                {
                    _orders.Remove(order);

                    for (int i = 1; i <= _orders.Count; i++)
                    {
                        _orders[i-1].OrderNumber = i;
                    }
                    return order;
                }
            }
            return null;
        }
       
        public bool SaveOrders(Order order, int orderNumber, string date)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].OrderNumber == orderNumber)
                {
                    _orders[i] = order;
                    return true;
                }
            }
            return false;
        }
    }
}
