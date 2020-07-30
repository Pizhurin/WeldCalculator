using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeldCalculator.Checking
{
    static class CheckingWeldSize
    {
        public static bool CheckSize(string _t1, string _t2, string _kf)
        {
            int t1;
            int t2;
            int tMin = 0;
            int kf;

            //Парсинг строки в int
            int.TryParse(_t1, out t1);
            int.TryParse(_t2, out t2);
            int.TryParse(_kf, out kf);
            
            // Определение минимальной толщины пластины
            if (t1 < t2) tMin = t1;
            else tMin = t2;

            //if (t1 > (t2 + 4) || t2 > (t1 + 4))
            //{
            //    MessageBox.Show("Для выравнивания толщин листов\nтребуется скос по толщине с отношением 1:5\n", "Недупустимая разница толщин пластин");                
            //}       

            if(kf > (tMin * 1.2))
            {
                MessageBox.Show("Катет сварного шва больше допустимого ", "Ошибка");
                return false;
            }

            return true;
        }

        public static bool CheckSize(string _t1, string _kf)
        {
            int t1;          
            int kf;

            //Парсинг строки в int
            int.TryParse(_t1, out t1);
            int.TryParse(_kf, out kf);

            if (kf > (t1 * 1.2))
            {
                MessageBox.Show("Катет сварного шва больше допустимого ", "Ошибка");
                return false;
            }

            return true;
        }
    }
}
