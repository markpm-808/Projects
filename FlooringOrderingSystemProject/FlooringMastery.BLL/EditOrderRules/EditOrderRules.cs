using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlooringMastery.BLL.EditOrderRules
{
    public class EditOrderRules : IEditOrder
    {
        Taxes newTax = new Taxes();

        public EditOrderResponse EditOrder(Order order)
        {
            EditOrderResponse response = new EditOrderResponse();

            if (!Regex.IsMatch(order.CustomerName, @"^[a-zA-Z0-9,.]+$"))
            {
                response.Success = false;
                response.Message = "Error: Customer name can include only the characters A-Z, 0-9, commas, and periods.";
                return response;
            }

            if ((newTax = StateVerification.CheckState(order.State)) == null)
            {
                response.Success = false;
                response.Message = "Error: Invalid state. Sorry, we do not currently sell to your state";
                return response;
            }
            if (order.Area < 0)
            {
                response.Success = false;
                response.Message = "Error: Area jmust be a positive decimal";
                return response;
            }
            if (order.Area < 100)
            {
                response.Success = false;
                response.Message = "Error: Minimum order size is 100 square feet";
                return response;
            }
            response.Success = true;
            response.Order = order;
            response.Taxes = newTax;
            return response;
        }
    }
}
