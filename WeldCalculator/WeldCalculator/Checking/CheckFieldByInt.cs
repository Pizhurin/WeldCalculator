using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldCalculator.Butt
{
    static class CheckFieldByInt
    {
        public static bool CheckInt(string stringInt)
        {
            int temp;
            if (stringInt != null)
            {
                if (int.TryParse(stringInt, out temp))
                    if (temp != 0) return true;
            }
            return false;
        }

        public static bool CheckIntWithoutNull(string stringInt)
        {
            int temp;
            if (stringInt != null)
            {
                if (int.TryParse(stringInt, out temp))
                    return true;
            }
            return false;
        }
    }
}
