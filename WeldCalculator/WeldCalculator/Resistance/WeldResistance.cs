using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldCalculator.Butt
{
    // Расчетные сопротивления сварных швов
    static class WeldResistance
    {
        // Получить расчетное сопротивление сварного СТЫКОВОГО соединения СЖАТИЮ
        public static int GetResistanceButtСompression(int indexSteelMark)
        {
            int resistanceWeldСompression = 0;
            switch (indexSteelMark)
            {
                case 0:
                case 1:
                    resistanceWeldСompression = 21;
                    break;
                case 2:
                    resistanceWeldСompression = 26;
                    break;
                case 3:
                    resistanceWeldСompression = 29;
                    break;
                case 4:
                    resistanceWeldСompression = 34;
                    break;
                case 5:
                    resistanceWeldСompression = 38;
                    break;
                case 6:
                    resistanceWeldСompression = 44;
                    break;
                default:
                    break;
            }
            return resistanceWeldСompression;
        }

        // Получить расчетное сопротивление сварного СТЫКОВОГО соединения РАСТЯЖЕНИЮ
        public static int GetResistanceButtStretching(int indexSteelMark, int indexControlType)
        {
            int resistanceWeldStretching = 0;

            // Получить расчетное сопротивление сварного СТЫКОВОГО соединения РАСТЯЖЕНИЮ с ВИЗУАЛЬНЫМ контролем качества шва
            if (indexControlType == 0)
            {
                switch (indexSteelMark)
                {
                    case 0:
                    case 1:
                        resistanceWeldStretching = 18;
                        break;
                    case 2:
                        resistanceWeldStretching = 22;
                        break;
                    case 3:
                        resistanceWeldStretching = 25;
                        break;
                    case 4:
                        // По справочнику Михайлова прочерк " - "
                        resistanceWeldStretching = 25;
                        break;
                    case 5:
                        // По справочнику Михайлова прочерк " - "
                        resistanceWeldStretching = 25;
                        break;
                    case 6:
                        // По справочнику Михайлова прочерк " - "
                        resistanceWeldStretching = 25;
                        break;
                    default:
                        break;
                }
            }

            // Получить расчетное сопротивление сварного СТЫКОВОГО соединения РАСТЯЖЕНИЮ с ФИЗИЧЕСКИМ контролем качества шва
            if (indexControlType==1)
            {
                switch (indexSteelMark)
                {
                    case 0:
                    case 1:
                        resistanceWeldStretching = 21;
                        break;
                    case 2:
                        resistanceWeldStretching = 26;
                        break;
                    case 3:
                        resistanceWeldStretching = 29;
                        break;
                    case 4:
                        resistanceWeldStretching = 34;
                        break;
                    case 5:
                        resistanceWeldStretching = 38;
                        break;
                    case 6:
                        resistanceWeldStretching = 44;
                        break;
                    default:
                        break;
                }
            }
            return resistanceWeldStretching;
        }

        // Получить расчетное сопротивление сварного СТЫКОВОГО соединения СРЕЗУ
        public static int GetResistanceButtShear(int indexSteelMark)
        {
            int resistanceWeldShear = 0;
            switch (indexSteelMark)
            {
                case 0:
                case 1:
                    resistanceWeldShear = 13;
                    break;
                case 2:
                    resistanceWeldShear = 15;
                    break;
                case 3:
                    resistanceWeldShear = 17;
                    break;
                case 4:
                    resistanceWeldShear = 20;
                    break;
                case 5:
                    resistanceWeldShear = 23;
                    break;
                case 6:
                    resistanceWeldShear = 26;
                    break;
                default:
                    break;
            }
            return resistanceWeldShear;
        }

        // Получить расчетное сопротивление сварного УГЛОВОГО соединения СРЕЗУ
        public static int GetResistanceFilletShear(int indexSteelMark)
        {
            int resistanceWeldShear = 0;
            switch (indexSteelMark)
            {
                case 0:
                case 1:
                    resistanceWeldShear = 15;
                    break;
                case 2:
                    resistanceWeldShear = 18;
                    break;
                case 3:
                    resistanceWeldShear = 20;
                    break;
                case 4:
                    resistanceWeldShear = 22;
                    break;
                case 5:
                    resistanceWeldShear = 24;
                    break;
                case 6:
                    resistanceWeldShear = 28;
                    break;
                default:
                    break;
            }
            return resistanceWeldShear;
        }
    }
}
