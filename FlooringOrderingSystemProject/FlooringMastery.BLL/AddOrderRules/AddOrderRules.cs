using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlooringMastery.BLL.AddOrderRules
{
    public class AddOrderRules : IAddOrder
    {
        Taxes newTax = new Taxes();

        public AddOrderResponse AddOrder(string date, Order order)
        {
            AddOrderResponse response = new AddOrderResponse();

            DateTime orderDate = DateTime.ParseExact(date, "MMddyyyy", null);

            if(orderDate < DateTime.Now.Date)
            {
                response.Success = false;
                response.Message = "Error: The order date must be in the future. Please try again.";
                return response;
            }
            if(order.CustomerName == "")
            {
                response.Success = false;
                response.Message = "Error: Customer name cannot be blank";
                return response;
            }
            if (!Regex.IsMatch(order.CustomerName, @"^[a-zA-Z0-9,.]+$"))
            {
                response.Success = false;
                response.Message = "Error: Customer name can include only the characters A-Z, 0-9, commas, and periods. Please try again.";
                return response;
            }
            if ((newTax = StateVerification.CheckState(order.State)) == null)
            {
                response.Success = false;
                response.Message = "Error: Sorry, we do not currently sell to your state. Please select a different state.";
                return response;
            }
            if(order.Area < 0)
            {
                response.Success = false;
                response.Message = "Error: Area must be a positive number. Please try again.";
                return response;
            }
            if(order.Area < 100)
            {
                response.Success = false;
                response.Message = "Error: Minimum order size is 100 square feet. Please try again.";
                return response;
            }
            response.Success = true;
            response.Order = order;
            response.Taxes = newTax;
            return response;
        }
    }
}
