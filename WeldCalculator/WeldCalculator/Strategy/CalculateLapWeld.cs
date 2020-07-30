using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeldCalculator.Butt;
using WeldCalculator.Resistance;

namespace WeldCalculator.Strategy
{
    class CalculateLapWeld : IStrategyCheckingByForcesType
    {

        #region Закрыты поля
        // Длина шва L1
        private double l1;
        // Длина шва L2
        private double l2;
        // Длина шва L3
        private double l3;
        // Длина шва L4
        private double l4;
        // Общая длинна шва
        private double totalL;
        // Толщина первого элемента
        private double t1;
        // Толщина второго элемента
        private double t2;
        // Минимальная толщина соединяемых элементов
        private double _tMin;
        // Катет сварного шва
        private double kf;
        // Ексентриситет приложения силы P (перерезающая сила Q)
        private double e;
        // Осевое усилие
        private double n;
        // Изгибающий момент
        private double m;
        // Перерезающее усилие
        private double q;
        // Марка стали (принимает индекс)
        private int _steelMark = -1;
        //Контроль качества сварного шва (принимает индекс)
        private int _controlType = -1;
        // Тип исполнения сварного шва
        private int _typeWeld = -1;
        // Константа перевода в см
        private const double _convertToCM = 0.1;
        // Константа перевода в кНсм
        private const int _convertToKNCM = 100;
        #endregion

        #region Свойства
        public double L1
        {
            get => l1;
            set
            {
                if (value > 0) l1 = value-1;
                else l1 = 0;
            }
        }

        public double L2
        {
            get => l2;
            set
            {
                if (value > 0) l2 = value-1;
                else l2 = 0;
            }
        }

        public double L3
        {
            get => l3;
            set
            {
                if (value > 0) l3 = value-1;
                else l3 = 0;
            }
        }

        public double L4
        {
            get => l4;
            set
            {
                if (value > 0) l4 = value-1;
                else l4 = 0;
            }
        }

        public double TotalL
        {
            get => L1 + L2 + L3 + L4;
            set
            {
                totalL = L1+L2+L3+L4;                
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

        public double Kf
        {
            get => kf;
            set
            {
                if (value > 0) kf = value;
                else kf = 0;
            }
        }

        public double E
        {
            get => e;
            set
            {
                if (value > 0) e = value;
                else e = 0;
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

        public int IndexTypeWeld
        {
            get => _typeWeld;
            set
            {
                if (value >= 0) _typeWeld = value;
            }
        }

        #endregion

        // Конструктор без параметров
        public CalculateLapWeld()
        {
        }

        public void AddParam(params string[] param)
        {
            int i = 0;
            // Получить длину шва в см
            L1 = Convert.ToDouble(param[i++]) * _convertToCM;
            L2 = Convert.ToDouble(param[i++]) * _convertToCM;
            L3 = Convert.ToDouble(param[i++]) * _convertToCM;
            L4 = Convert.ToDouble(param[i++]) * _convertToCM;
            // Получить толщины соединяемых элементов в см
            T1 = Convert.ToDouble(param[i++]) * _convertToCM;
            T2 = Convert.ToDouble(param[i++]) * _convertToCM;
            Kf = Convert.ToDouble(param[i++]) * _convertToCM;
            E = Convert.ToDouble(param[i++]) * _convertToCM;
            N = Convert.ToInt32(param[i++]);
            // Получить кНсм
            M = Convert.ToInt32(param[i++]) * _convertToKNCM;
            Q = Convert.ToInt32(param[i++]);
            IndexSteelMark = Convert.ToInt32(param[i++]);
            IndexControlType = Convert.ToInt32(param[i++]);
            IndexTypeWeld = Convert.ToInt32(param[i++]);

            if (T1 < T2) _tMin = T1;
            else _tMin = T2;
        }

        public double CheckForceN()
        {
            double result = 0;

            result = N / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * TotalL*WeldResistance.GetResistanceFilletShear(IndexSteelMark));

            return result;
        }
         
        public double CheckForceM()
        {
            double result = 0;
            result = (6 * M) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * TotalL * TotalL * TotalL * WeldResistance.GetResistanceFilletShear(IndexSteelMark));
            return result;
        }

        public double CheckForceQ()
        {
            double result = 0;
            double normalTension = 0;
            double tangentTension = 0;

            // Проверка нормальных напряжений в шве
            normalTension = (6 * Q * E) / (_tMin * TotalL * TotalL * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));
            if (normalTension >= 1) MessageBox.Show("Нормальные напряжения " + normalTension.ToString() + " > 1.0", "Проверка не прошла!");

            // Проверка касательных напряжений в шве
            tangentTension = (3 * Q) / (2 * _tMin * TotalL * WeldResistance.GetResistanceButtShear(IndexSteelMark));
            if (tangentTension >= 1) MessageBox.Show("Касательные напряжения " + tangentTension.ToString() + " > 1.0", "Проверка не прошла!");

            result = (Math.Sqrt(
            Math.Pow((6 * Q * E) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * TotalL * TotalL), 2) + 
            Math.Pow((Q / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * TotalL)), 2)
            )) 
            / 
            WeldResistance.GetResistanceFilletShear(IndexSteelMark);

            return result;
        }

        public double CheckForceMN()
        {
            double result = 0;

            result = ((N / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * TotalL)) + 
                ((6 * Q * E) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * TotalL * TotalL)))
                / WeldResistance.GetResistanceFilletShear(IndexSteelMark);

            return result;
        }

        public double CheckForceMQ()
        {
            throw new NotImplementedException();
        }
    }
}
