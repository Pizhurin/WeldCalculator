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
using WeldCalculator.Butt;
using WeldCalculator.Checking;
using WeldCalculator.Strategy;

namespace WeldCalculator
{
    public partial class FormButt : Form
    {
        public FormButt()
        {
            InitializeComponent();
            timer_FormButt_Checked.Start();
            timer_FormButt_Checked.Interval = 500;

            //Инициализируем comboBox_FormButt сразу индексами <0>
            comboBox_FormButt_Steel.SelectedIndex = 0;
            comboBox_FormButt_ControlType.SelectedIndex = 0;
            comboBox_FormButt_WeldType.SelectedIndex = 0;
        }

        #region Синхронизация полей и значений на картинке в зависимости от активного типа расчета
        // Timer который синхронизирует введенные данные на кртинку
        private void timer_FormButt_Tick(object sender, EventArgs e)
        {
            // Обновление, если активное окно (Осевое усилие N)
            if (radioButton_FormButt_N.Checked)
            {
                if (textBox_FormButt_L1.Text == String.Empty || textBox_FormButt_L1.Text == " ")
                    label_FormButt_L1_ViewN.Text = "0";
                else label_FormButt_L1_ViewN.Text = textBox_FormButt_L1.Text;
              
                if (textBox_FormButt_t1.Text == String.Empty || textBox_FormButt_t1.Text == " ")
                    label_FormButt_t1_ViewN.Text = "0";
                else label_FormButt_t1_ViewN.Text = textBox_FormButt_t1.Text;

                if (textBox_FormButt_t2.Text == String.Empty || textBox_FormButt_t2.Text == " ")
                    label_FormButt_t2_ViewN.Text = "0";
                else label_FormButt_t2_ViewN.Text = textBox_FormButt_t2.Text;

                if (textBox_FormButt_NForce.Text == String.Empty || textBox_FormButt_NForce.Text == " ")
                    label_FormButt_N_ViewN.Text = "0";
                else label_FormButt_N_ViewN.Text = textBox_FormButt_NForce.Text;

            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormButt_L1_ViewN.Text = "0";
                label_FormButt_t1_ViewN.Text = "0";
                label_FormButt_t2_ViewN.Text = "0";
                label_FormButt_N_ViewN.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M)
            if (radioButton_FormButt_M.Checked)
            {
                if (textBox_FormButt_L1.Text == String.Empty || textBox_FormButt_L1.Text == " ")
                    label_FormButt_L1_ViewM.Text = "0";
                else label_FormButt_L1_ViewM.Text = textBox_FormButt_L1.Text;

                if (textBox_FormButt_t1.Text == String.Empty || textBox_FormButt_t1.Text == " ")
                    label_FormButt_t1_ViewM.Text = "0";
                else label_FormButt_t1_ViewM.Text = textBox_FormButt_t1.Text;

                if (textBox_FormButt_t2.Text == String.Empty || textBox_FormButt_t2.Text == " ")
                    label_FormButt_t2_ViewM.Text = "0";
                else label_FormButt_t2_ViewM.Text = textBox_FormButt_t2.Text;

                if (textBox_FormButt_MForce.Text == String.Empty || textBox_FormButt_MForce.Text == " ")
                    label_FormButt_M_ViewM.Text = "0";
                else label_FormButt_M_ViewM.Text = textBox_FormButt_MForce.Text;
            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormButt_L1_ViewM.Text = "0";
                label_FormButt_t1_ViewM.Text = "0";
                label_FormButt_t2_ViewM.Text = "0";
                label_FormButt_M_ViewM.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M и перерезающие усилия Q)
            if (radioButton_FormButt_MQ.Checked)
            {
                if (textBox_FormButt_L1.Text == String.Empty || textBox_FormButt_L1.Text == " ")
                    label_FormButt_L1_ViewMQ.Text = "0";
                else label_FormButt_L1_ViewMQ.Text = textBox_FormButt_L1.Text;

                if (textBox_FormButt_t1.Text == String.Empty || textBox_FormButt_t1.Text == " ")
                    label_FormButt_t1_ViewMQ.Text = "0";
                else label_FormButt_t1_ViewMQ.Text = textBox_FormButt_t1.Text;

                if (textBox_FormButt_t2.Text == String.Empty || textBox_FormButt_t2.Text == " ")
                    label_FormButt_t2_ViewMQ.Text = "0";
                else label_FormButt_t2_ViewMQ.Text = textBox_FormButt_t2.Text;

                if (textBox_FormButt_MForce.Text == String.Empty || textBox_FormButt_MForce.Text == " ")
                    label_FormButt_M_ViewMQ.Text = "0";
                else label_FormButt_M_ViewMQ.Text = textBox_FormButt_MForce.Text;

                if (textBox_FormButt_QForce.Text == String.Empty || textBox_FormButt_QForce.Text == " ")
                    label_FormButt_Q_ViewMQ.Text = "0";
                else label_FormButt_Q_ViewMQ.Text = textBox_FormButt_QForce.Text;
            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormButt_L1_ViewMQ.Text = "0";
                label_FormButt_t1_ViewMQ.Text = "0";
                label_FormButt_t2_ViewMQ.Text = "0";
                label_FormButt_M_ViewMQ.Text = "0";
                label_FormButt_Q_ViewMQ.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M и Осевое усилие N)
            if (radioButton_FormButt_MN.Checked)
            {
                if (textBox_FormButt_L1.Text == String.Empty || textBox_FormButt_L1.Text == " ")
                    label_FormButt_L1_ViewMN.Text = "0";
                else label_FormButt_L1_ViewMN.Text = textBox_FormButt_L1.Text;

                if (textBox_FormButt_t1.Text == String.Empty || textBox_FormButt_t1.Text == " ")
                    label_FormButt_t1_ViewMN.Text = "0";
                else label_FormButt_t1_ViewMN.Text = textBox_FormButt_t1.Text;

                if (textBox_FormButt_t2.Text == String.Empty || textBox_FormButt_t2.Text == " ")
                    label_FormButt_t2_ViewMN.Text = "0";
                else label_FormButt_t2_ViewMN.Text = textBox_FormButt_t2.Text;

                if (textBox_FormButt_MForce.Text == String.Empty || textBox_FormButt_MForce.Text == " ")
                    label_FormButt_M_ViewMN.Text = "0";
                else label_FormButt_M_ViewMN.Text = textBox_FormButt_MForce.Text;

                if (textBox_FormButt_NForce.Text == String.Empty || textBox_FormButt_QForce.Text == " ")
                    label_FormButt_N_ViewMN.Text = "0";
                else label_FormButt_N_ViewMN.Text = textBox_FormButt_NForce.Text;
            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormButt_L1_ViewMN.Text = "0";
                label_FormButt_t1_ViewMN.Text = "0";
                label_FormButt_t2_ViewMN.Text = "0";
                label_FormButt_M_ViewMN.Text = "0";
                label_FormButt_N_ViewMN.Text = "0";
            }

        }
        #endregion

        // Timer который отслеживает, что выбран один из типов расчета, тогда он запускает Timer для синхронизации введенных значений на картинку
        private void timer_FormButt_Checked_Tick(object sender, EventArgs e)
        {
            if (radioButton_FormButt_N.Checked == true ||
                radioButton_FormButt_M.Checked == true ||
                radioButton_FormButt_MQ.Checked == true ||
                radioButton_FormButt_MN.Checked == true)
            {
                timer_FormButt.Start();
                timer_FormButt.Interval = 100;
            }
        }

        // Непосредственно РАСЧЕТ
        private void button_FormButt_Checking_Click(object sender, EventArgs e)
        {

            #region Параметры для стратегии
            // Передать параметры для стратегии расчета
            IStrategyCheckingByForcesType calculateStrategyWeld = new CalculateButtWeld();
            calculateStrategyWeld.AddParam(
                textBox_FormButt_L1.Text,
                textBox_FormButt_t1.Text,
                textBox_FormButt_t2.Text,
                textBox_FormButt_NForce.Text,
                textBox_FormButt_MForce.Text,
                textBox_FormButt_QForce.Text,
                comboBox_FormButt_Steel.SelectedIndex.ToString(),
                comboBox_FormButt_ControlType.SelectedIndex.ToString()
                );
            #endregion

            // Создание объекта сбора ошибок
            DataError dataError = new DataError();

            #region Проверка активного типа расчета
            if (radioButton_FormButt_N.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormButt_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                //textBox_FormButt_NForce.Text = "0";
                textBox_FormButt_MForce.Text = "0";
                textBox_FormButt_QForce.Text = "0";
            }
            if (radioButton_FormButt_M.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormButt_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                textBox_FormButt_NForce.Text = "0";
                //textBox_FormButt_MForce.Text = "0";
                textBox_FormButt_QForce.Text = "0";
            }
            if (radioButton_FormButt_MQ.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormButt_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                textBox_FormButt_NForce.Text = "0";
                //textBox_FormButt_MForce.Text = "0";
                //textBox_FormButt_QForce.Text = "0";
            }
            if (radioButton_FormButt_MN.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormButt_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormButt_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormButt_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                //textBox_FormButt_NForce.Text = "0";
                //textBox_FormButt_MForce.Text = "0";
                textBox_FormButt_QForce.Text = "0";
            }
            #endregion

            #region Расчет конкретного типа стыкового шва
            // Если ошибок нет, то продолжить            
            if (dataError.Count() <= 0 && CheckingWeldSize.CheckSize(textBox_FormButt_t1.Text, textBox_FormButt_t2.Text, textBox_FormButt_Kf.Text))
            {
                // Для cтыкового шва N
                if (radioButton_FormButt_N.Checked)
                {
                    // Результирующие нормальные напряжения
                    double resultNormalTension = Math.Abs(calculateStrategyWeld.CheckForceN());
                    resultNormalTension = Math.Round(resultNormalTension, 2);

                    if (resultNormalTension < 1) MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " < 1.0", "Проверка прошла успешно!");
                    else
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " > 1.0", "Проверка не прошла!");                        
                    }
                }

                // Для cтыкового шва M
                if (radioButton_FormButt_M.Checked)
                {
                    // Результирующие нормальные напряжения
                    double resultNormalTension = Math.Abs(calculateStrategyWeld.CheckForceM());
                    resultNormalTension = Math.Round(resultNormalTension, 2);

                    if (resultNormalTension < 1) MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " < 1.0", "Проверка прошла успешно!");
                    else
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " > 1.0", "Проверка не прошла!");
                    }
                }

                // Для cтыкового шва MQ
                if (radioButton_FormButt_MQ.Checked)
                {
                    // Результирующие нормальные напряжения
                    double resultNormalTension = Math.Abs(calculateStrategyWeld.CheckForceMQ());
                    resultNormalTension = Math.Round(resultNormalTension, 2);

                    if (resultNormalTension < 1) MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " < 1.0", "Проверка прошла успешно!");
                    else
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " > 1.0", "Проверка не прошла!");
                    }
                }

                if (radioButton_FormButt_MN.Checked)
                {
                    // Результирующие нормальные напряжения
                    double resultNormalTension = Math.Abs(calculateStrategyWeld.CheckForceMN());
                    resultNormalTension = Math.Round(resultNormalTension, 2);

                    if (resultNormalTension < 1) MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " < 1.0", "Проверка прошла успешно!");
                    else
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " > 1.0", "Проверка не прошла!");
                    }

                }

                #endregion

            }
        }

        private void button_FormButt_Calculate_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            MessageBox.Show("Недоступно для бесплатной версии", "Ошибка");
        }
    }
}
