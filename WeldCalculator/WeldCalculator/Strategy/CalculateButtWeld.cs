using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeldCalculator.Butt;

namespace WeldCalculator.Strategy
{
    class CalculateButtWeld : IStrategyCheckingByForcesType
    {
        #region Поля
        // Длинна шва
        private double l;
        // Тольщина 1-го элемента
        private double t1;
        // Толщина 2-го элемента
        private double t2;
        // Минимальная толщина соединяемых элементов
        private double _tMin;
        // Осевое усилие
        private double n;
        // Изгибающий момент
        private double m;
        // Поперечная сила
        private double q;
        // Марка стали (принимает индекс)
        private int _steelMark = -1;
        //Контроль качества сварного шва (принимает индекс)
        private int _controlType = -1;
        // Константа перевода в см
        private const double _convertToCM = 0.1;
        // Константа перевода в кНсм
        private const int _convertToKNCM = 100;
        #endregion

        #region Свойства полей
        public double L
        {
            get => l;
            set
            {
                // Длина шва уменьшена на 1см (на непровар)
                if (value > 0) l = value-1;
                else l = 0;
            }
        }

        public double T1
        {
            get => t1;
            set
            {
                if (value > 0) t1 = value;
                else t1 = 0;
            }
        }

        public double T2
        {
            get => t2;
            set
            {
                if (value > 0) t2 = value;
                else t2 = 0;
            }
        }

        public double N
        {
            get => n;
            set
            {
                if (value != 0) n = value;
            }
        }

        public double M
        {
            get => m;
            set
            {
                if (value != 0) m = value;
            }
        }

        public double Q
        {
            get => q;
            set
            {
                if (value != 0) q = value;
            }
        }

        public int IndexSteelMark
        {
            get => _steelMark;
            set
            {
                if (value >= 0) _steelMark = value;
            }
        }

        public int IndexControlType
        {
            get => _controlType;
            set
            {
                if (value >= 0) _controlType = value;
            }
        }

        #endregion

        // Конструктор без параметров
        public CalculateButtWeld()
        {         
        }
        
        public void AddParam(params string[] param)
        {
            int i = 0;
            // Получить см (-1см на непровар шва)
            L = Convert.ToDouble(param[i++]) * _convertToCM;
            // ПОлучить см
            T1 = Convert.ToDouble(param[i++]) * _convertToCM;
            T2 = Convert.ToDouble(param[i++]) * _convertToCM;
            N = Convert.ToInt32(param[i++]);
            // Получить кНсм
            M = Convert.ToInt32(param[i++]) * _convertToKNCM;
            Q = Convert.ToInt32(param[i++]);
            IndexSteelMark = Convert.ToInt32(param[i++]);
            IndexControlType = Convert.ToInt32(param[i++]);

            if (T1 < T2) _tMin = T1;
            else _tMin = T2;
        }

        public double CheckForceN()
        {
            double result = 0;

            if (N > 0)
            {
                result = N / (_tMin * L * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));
            }
            else
            {
                result = N / (_tMin * L * WeldResistance.GetResistanceButtСompression(IndexSteelMark));
            }
            return result;
        }

        public double CheckForceM()
        {
            double result = 0;
            result = (6 * M) / (_tMin * L * L * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));
            return result;
        }

        public double CheckForceQ()
        {
            throw new NotImplementedException();
        }

        public double CheckForceMQ()
        {
            double normalTension = 0;
            double normalTensionAbsolute = 0;
            double tangentTension = 0;
            double tangentTensionAbsolute = 0;
            double tangentTensionAverageAbsolute = 0;
            double totalTensionAbsolute = 0;
            double resultTotalTension = 0;

            // Проверка нормальных напряжений в шве
            normalTension = (6 * M) / (_tMin * L * L * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));            
            if (normalTension >= 1) MessageBox.Show("Нормальные напряжения " + normalTension.ToString() + " > 1.0", "Проверка не прошла!");

            // Проверка касательных напряжений в шве
            tangentTension = (3 * Q) / (2 * _tMin * L * WeldResistance.GetResistanceButtShear(IndexSteelMark));
            if (tangentTension >= 1) MessageBox.Show("Касательные напряжения " + tangentTension.ToString() + " > 1.0", "Проверка не прошла!");

            // Получить абсолютное значение нормальных напряжений в сварном шве [кН/см2]
            normalTensionAbsolute = (6 * M) / (_tMin * L * L);
            // Получить абсолютное значение касательных напряжений в сварном шве [кН/см2]
            tangentTensionAbsolute = (3 * Q) / (2 * _tMin * L);
            // Получить абсолютное значение приведенных напряжений в сварном шве [кН/см2]
            tangentTensionAverageAbsolute = Q / (_tMin * L);
            totalTensionAbsolute = Math.Sqrt((normalTensionAbsolute * normalTensionAbsolute) + (3 * tangentTensionAverageAbsolute * tangentTensionAverageAbsolute));

            //Проверка приведенных напряжений в сварном шве
            resultTotalTension = (Math.Sqrt((normalTensionAbsolute * normalTensionAbsolute) + (3 * tangentTensionAverageAbsolute * tangentTensionAverageAbsolute))) / (1.15 * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));

            return resultTotalTension;
        }

        public double CheckForceMN()
        {
            double result = 0;
            //if (N < 0)
            //    result = ((N / (_tMin * L)) + ((6 * M) / (_tMin * L * L))) / WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType);
            //else
            //    result = ((N / (_tMin * L)) + ((6 * M) / (_tMin * L * L))) / WeldResistance.GetResistanceButtСompression(IndexSteelMark);

            result = ((N * L) + (6 * M)) / (_tMin * L * L * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));

            return result;
        }
    }
}