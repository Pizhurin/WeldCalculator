using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldCalculator.Resistance
{
    static class DepthWeldRatio
    {
        // Получить коэффициент глубины провара сварного шва
        public static double GetRatio(int indexWeldType)
        {
            double ratio = 0;
            switch (indexWeldType)
            {
                case 0:
                    ratio = 0.7;
                    break;
                case 1:
                    ratio = 0.8;
                    break;
                case 2:
                    ratio = 0.9;
                    break;
                default:
                    break;
            }
            return ratio;
        }
    }
}
