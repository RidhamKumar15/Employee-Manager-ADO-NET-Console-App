using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_USING_STRUCTURE.Helpers
{
    public static class ConversionHelper
    {
        public static decimal ConvertStringToDecimal(string input)
        {
            
            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid numeric input. Returning default value (0.0).");
                return 0;
            }
        }
    }

}
