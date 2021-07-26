using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.BLL
{
    public class StateVerification
    {
        private static string _Path = @"C:\Data\Orders\Taxes.txt";
        
        public static Taxes CheckState(string state)
        {
            Taxes newTax = new Taxes();

            List<Taxes> States = new List<Taxes>();

            using (StreamReader sr = new StreamReader(_Path, true))
            {
                sr.ReadLine();
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    Taxes newState = new Taxes();
                    string[] columns = line.Split(',');
                    newState.StateAbbreviation = columns[0];
                    newState.StateName = columns[1];
                    newState.TaxRate = decimal.Parse(columns[2]);

                    States.Add(newState);
                }
            }
            foreach(var item in States)
            {
                if(item.StateAbbreviation == state.ToUpper() || item.StateName == char.ToUpper(state[0]) + state.ToLower().Substring(1))
                {
                    newTax.StateAbbreviation = item.StateAbbreviation;
                    newTax.TaxRate = item.TaxRate;
                    return newTax;
                }
            }
            return null;
        }
    }
}
