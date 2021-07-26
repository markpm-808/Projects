﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class EditOrderResponse : Response
    {
        public Order Order { get; set; }
        public Taxes Taxes { get; set; }
        public Product Product { get; set; }
    }
}
