using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL.EditOrderRules
{
    public class EditOrderRulesFactory
    {
        public static IEditOrder Create()
        {
            return new EditOrderRules();
        }

    }
}
