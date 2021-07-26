using FlooringMastery.BLL.AddOrderRules;
using FlooringMastery.BLL.EditOrderRules;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public DisplayOrderResponse DisplayOrders(string fileName)
        {
            DisplayOrderResponse response = new DisplayOrderResponse();

            response.OrderList = _orderRepository.LoadOrders(fileName);

            if (!response.OrderList.Any())
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public RemoveOrderResponse RemoveOrder(string fileName, int orderNumber)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();

            response.Order = _orderRepository.RemoveOrder(fileName, orderNumber);

            if(response.Order == null)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        
        public GetOrderResponse GetOrder(string fileName, int orderNumber)
        {
            GetOrderResponse response = new GetOrderResponse();

            response.Order = _orderRepository.FetchOrder(fileName, orderNumber);

            if (response.Order == null)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public EditOrderResponse EditOrder(Order order)
        {
            EditOrderResponse response = new EditOrderResponse();

            IEditOrder editOrderRule = EditOrderRulesFactory.Create();

            response = editOrderRule.EditOrder(order);

            return response;
        }

        public EditOrderResponse SaveEditOrder(string fileName, int orderNumber, Order order)
        {
            EditOrderResponse response = new EditOrderResponse();

            bool success = _orderRepository.SaveOrders(order, orderNumber, fileName);

            if (success == true)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
            return response;
        }

        public AddOrderResponse AddOrder(string filename, string date, Order order)
        {
            AddOrderResponse response = new AddOrderResponse();

            IAddOrder addOrderRule = AddOrderRulesFactory.Create();

            response = addOrderRule.AddOrder(date, order);
            
            return response;
        }

        public void SaveAddOrder(string fileName, string date, Order order)
        {
            _orderRepository.AddOrder(fileName, date, order);
        }
    }
}
