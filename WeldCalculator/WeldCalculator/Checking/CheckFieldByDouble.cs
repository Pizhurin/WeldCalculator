using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldCalculator.Checking
{
    static class CheckFieldByDouble
    {
        public static bool CheckDouble(string stringInt)
        {
            double temp;
            if (stringInt != null)
            {
                if (double.TryParse(stringInt, out temp))
                    if (temp != 0) return true;
            }
            return false;
        }
    }
}
