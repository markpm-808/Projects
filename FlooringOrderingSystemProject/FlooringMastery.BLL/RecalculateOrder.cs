using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public static class RecalculateOrder
    {
        public static Order Recalculate(EditOrderResponse response)
        {
            Order editedOrder = new Order();

            editedOrder = response.Order;
            editedOrder.TaxRate = response.Taxes.TaxRate;

            editedOrder.MaterialCost = (editedOrder.Area * editedOrder.CostPerSquareFoot);
            editedOrder.LaborCost = (editedOrder.Area * editedOrder.LaborCostPerSquareFoot);
            editedOrder.Tax = ((editedOrder.MaterialCost + editedOrder.LaborCost) * (editedOrder.TaxRate / 100));
            editedOrder.Total = (editedOrder.MaterialCost + editedOrder.LaborCost + editedOrder.Tax);
            return editedOrder;
        }

        public static Order Recalculate(AddOrderResponse response)
        {
            Order editedOrder = new Order();

            editedOrder = response.Order;
            editedOrder.TaxRate = response.Taxes.TaxRate;

            editedOrder.MaterialCost = (editedOrder.Area * editedOrder.CostPerSquareFoot);
            editedOrder.LaborCost = (editedOrder.Area * editedOrder.LaborCostPerSquareFoot);
            editedOrder.Tax = ((editedOrder.MaterialCost + editedOrder.LaborCost) * (editedOrder.TaxRate / 100));
            editedOrder.Total = (editedOrder.MaterialCost + editedOrder.LaborCost + editedOrder.Tax);
            return editedOrder;
        }
    }
}
