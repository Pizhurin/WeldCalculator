using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeldCalculator
{
    public partial class Form_Start : Form
    {
        public Form_Start()
        {
            InitializeComponent();            
        }

        private void button_FormStart_Calculate_Click(object sender, EventArgs e)
        {
            // Выбор вызова формы в зависимости от выбранного типа сварного шва
            if (radioButton_FormStart_ButtWeld.Checked == true)
            {
                this.Visible = false;
                FormButt formButt = new FormButt();
                formButt.ShowDialog();
                this.Visible = true;
            }
            else if (radioButton_FormStart_FilletWeld.Checked == true)
            {
                this.Visible = false;
                FormFillet formLap = new FormFillet();
                formLap.ShowDialog();
                this.Visible = true;
            }
            else if (radioButton_FormStart_LapWeld.Checked == true)
            {
                this.Visible = false;
                FormLap formFillet = new FormLap();
                formFillet.ShowDialog();
                this.Visible = true;
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не выбран тип сварного шва!", "Error");
            }

        }
    }
}
