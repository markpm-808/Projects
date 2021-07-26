using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadOrders(string fileName);
        Order AddOrder(string filename, string date, Order order);
        Order RemoveOrder(string filename, int orderNumber);
        Order FetchOrder(string filename, int orderNumber);
        bool SaveOrders(Order order, int orderNumber, string date);
    }
}
