﻿using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IAddOrder
    {
        AddOrderResponse AddOrder(string date, Order order);
    }
}
