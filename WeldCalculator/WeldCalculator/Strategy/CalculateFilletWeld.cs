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
    class CalculateFilletWeld : IStrategyCheckingByForcesType
    {

        #region Закрыты поля
        // Длина шва L1
        private double l1;        
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
        // Количество сварных швов прикрепляемого элемента (односторонний или двусторонний)
        private int _multilineWeld = -1;
        // Константа перевода в см
        private const double _convertToCM = 0.1;
        // Константа перевода в кНсм
        private const int _convertToKNCM = 100;
        //Константа учитывающая зазор, при условии отсутствия разделки кромок
        // Коэф учитывающий наименьшее сечение углового шва
        private const double _ratioMinSection = 0.7;
        #endregion

        #region Свойства
        public double L1
        {
            get => l1;
            set
            {
                if (value > 0) l1 = value - 1;
                else l1 = 0;
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

        public int IndexMultilineWeld
        {
            get => _multilineWeld;
            set
            {
                if (value >= 0) _multilineWeld = value;
            }
        }

        #endregion

        public CalculateFilletWeld()
        {
        }

        public void AddParam(params string[] param)
        {
            int i = 0;
            // Получить длину шва в см
            L1 = Convert.ToDouble(param[i++]) * _convertToCM;
            // Получить толщины соединяемых элементов в см
            T1 = Convert.ToDouble(param[i++]) * _convertToCM;
            Kf = Convert.ToDouble(param[i++]) * _convertToCM;
            E = Convert.ToDouble(param[i++]) * _convertToCM;
            N = Convert.ToInt32(param[i++]);
            // Получить кНсм
            M = Convert.ToInt32(param[i++]) * _convertToKNCM;
            Q = Convert.ToInt32(param[i++]);
            IndexSteelMark = Convert.ToInt32(param[i++]);
            IndexControlType = Convert.ToInt32(param[i++]);
            IndexTypeWeld = Convert.ToInt32(param[i++]);
            IndexMultilineWeld = Convert.ToInt32(param[i++]);
        }

        public double CheckForceN()
        {
            double result = 0;

            // Проверка шва (односторонний или двусторонний)
            if (IndexMultilineWeld == 0)
                result = N / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * L1 * WeldResistance.GetResistanceFilletShear(IndexSteelMark));
            else result = N / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * (L1 + L1) * WeldResistance.GetResistanceFilletShear(IndexSteelMark));

            return result;            
        }

        public double CheckForceM()
        {
            double result = 0;

            if (IndexMultilineWeld == 0)
                result = (6 * M) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * L1 * L1 * WeldResistance.GetResistanceFilletShear(IndexSteelMark));
            else result = (6 * M) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * (L1 + L1) * (L1 + L1)  * WeldResistance.GetResistanceFilletShear(IndexSteelMark));
            return result;
        }


        public double CheckForceQ()
        {
            double result = 0;
            double normalTension = 0;
            double tangentTension = 0;

            if (IndexMultilineWeld == 0)
            {              
                // Проверка нормальных напряжений в шве
                normalTension = (6 * Q * E) / (Kf * _ratioMinSection * L1 * L1 * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));
                if (normalTension >= 1) MessageBox.Show("Нормальные напряжения " + normalTension.ToString() + " > 1.0", "Проверка не прошла!");

                // Проверка касательных напряжений в шве
                tangentTension = (3 * Q) / (2 * Kf * _ratioMinSection * L1 * WeldResistance.GetResistanceButtShear(IndexSteelMark));
                if (tangentTension >= 1) MessageBox.Show("Касательные напряжения " + tangentTension.ToString() + " > 1.0", "Проверка не прошла!");

                result = (Math.Sqrt(
            Math.Pow((6 * Q * E) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * L1 * L1), 2) +
            Math.Pow((Q / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf - _ratioMinSection * L1)), 2)
            ))
            /
            WeldResistance.GetResistanceFilletShear(IndexSteelMark);
            }
            else
            {
                // Проверка нормальных напряжений в шве
                normalTension = (6 * Q * E) / (Kf * _ratioMinSection * (L1+L1) * (L1 + L1) * WeldResistance.GetResistanceButtStretching(IndexSteelMark, IndexControlType));
                if (normalTension >= 1) MessageBox.Show("Нормальные напряжения " + normalTension.ToString() + " > 1.0", "Проверка не прошла!");

                // Проверка касательных напряжений в шве
                tangentTension = (3 * Q) / (2 * Kf * _ratioMinSection * (L1 + L1) * WeldResistance.GetResistanceButtShear(IndexSteelMark));
                if (tangentTension >= 1) MessageBox.Show("Касательные напряжения " + tangentTension.ToString() + " > 1.0", "Проверка не прошла!");

                result = (Math.Sqrt(
            Math.Pow((6 * Q * E) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * (L1+L1) * (L1 + L1)), 2) +
            Math.Pow((Q / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf - _ratioMinSection * (L1 + L1))), 2)
            ))
            /
            WeldResistance.GetResistanceFilletShear(IndexSteelMark);
            }

            return result;
        }

        public double CheckForceMN()
        {
            double result = 0;

            if (IndexMultilineWeld == 0)
            {
                result = ((N / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * L1)) +
                ((6 * Q * E) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * L1 * L1)))
                / WeldResistance.GetResistanceFilletShear(IndexSteelMark);
            }
            else
            {
                result = ((N / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * (L1+L1))) +
                ((6 * Q * E) / (DepthWeldRatio.GetRatio(IndexTypeWeld) * Kf * _ratioMinSection * (L1 + L1) * (L1 + L1))))
                / WeldResistance.GetResistanceFilletShear(IndexSteelMark);
            }

            return result;
        }

        public double CheckForceMQ()
        {
            throw new NotImplementedException();
        }

    }
}
