using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.BLL.AddOrderRules;
using FlooringMastery.BLL.EditOrderRules;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    class ProdOrderTests
    {
        string _filename = @"Orders_06012013.txt";

        [Test]
        public void CanLoadProdData()
        {

            OrderManager manager = OrderManagerFactory.Create();
            DisplayOrderResponse response = manager.DisplayOrders(_filename);

            Assert.IsNotNull(response.OrderList);
            Assert.IsTrue(response.Success);
        }
        

        [TestCase("12202020", "Bob", "PA", "Wood", 300, true)]
        [TestCase("03312020", "Bob", "PA", "Wood", 105, false)]    //order not in future
        [TestCase("12202020", "Bob#$", "PA", "Wood", 1200, false)] //invalid name
        [TestCase("12312020", "Bob", "HI", "Wood", 100, false)]    //incorrect state
        [TestCase("12312020", "Bob", "PA", "Wood", 25, false)]    // order size too small
        [TestCase("12202020", "Bob1009.", "PA", "Tile", 100, true)]
        public void AddOrderRuleTest(string orderDate, string customerName, string state, string productType, decimal area, bool expectedResult)
        {
            IAddOrder rule = new AddOrderRules();
            Order order = new Order();

            order.CustomerName = customerName;
            order.State = state;
            order.ProductType = productType;
            order.Area = area;

            AddOrderResponse response = rule.AddOrder(orderDate, order);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("12312020", "Carol", "PA", "Wood", 300, true)]
        [TestCase("12312020", "Carol$$", "PA", "Wood", 300, false)]
        [TestCase("12312020", "Jeremy", "MI", "laminate", 50, false)]
        [TestCase("12312020", "Jeremy", "FL", "tile", 150, false)]
        public void EditOrderRuleTest(string orderDate, string customerName, string state, string productType, decimal area, bool expectedResult)
        {
            IEditOrder rule = new EditOrderRules();
            Order order = new Order();

            order.CustomerName = customerName;
            order.State = state;
            order.ProductType = productType;
            order.Area = area;

            EditOrderResponse response = rule.EditOrder(order);
            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
