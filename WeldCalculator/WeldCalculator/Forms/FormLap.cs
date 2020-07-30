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
    public partial class FormLap : Form
    {
        public FormLap()
        {
            InitializeComponent();

            //Инициализируем comboBox_FormButt сразу индексами <0>
            comboBox_FormLap_Steel.SelectedIndex = 0;
            comboBox_FormLap_ControlType.SelectedIndex = 0;
            comboBox_FormLap_WeldType.SelectedIndex = 0;

            timer_FormLap_Checked.Start();
            timer_FormLap_Checked.Interval = 500;

        }

        #region Синхронизация полей и значений на картинке в зависимости от активного типа расчета
        // Timer который синхронизирует введенные данные на кртинку
        private void timer_FormLap_Tick(object sender, EventArgs e)
        {
            // Обновление, если активное окно (Осевое усилие N)
            if (radioButton_FormLap_N.Checked) 
            {
                if (textBox_FormLap_L1.Text == String.Empty || textBox_FormLap_L1.Text == " ")
                    label_FormFillet_L1_ViewN.Text = "0";
                else label_FormFillet_L1_ViewN.Text = textBox_FormLap_L1.Text;

                if (textBox_FormLap_L2.Text == String.Empty || textBox_FormLap_L2.Text == " ")
                    label_FormFillet_L2_ViewN.Text = "0";
                else label_FormFillet_L2_ViewN.Text = textBox_FormLap_L2.Text;

                if (textBox_FormLap_L3.Text == String.Empty || textBox_FormLap_L3.Text == " ")
                    label_FormFillet_L3_ViewN.Text = "0";
                else label_FormFillet_L3_ViewN.Text = textBox_FormLap_L3.Text;

                if (textBox_FormLap_L4.Text == String.Empty || textBox_FormLap_L4.Text == " ")
                    label_FormFillet_L4_ViewN.Text = "0";
                else label_FormFillet_L4_ViewN.Text = textBox_FormLap_L4.Text;

                if (textBox_FormLap_t1.Text == String.Empty || textBox_FormLap_t1.Text == " ")
                    label_FormFillet_t1_ViewN.Text = "0";
                else label_FormFillet_t1_ViewN.Text = textBox_FormLap_t1.Text;

                if (textBox_FormLap_t2.Text == String.Empty || textBox_FormLap_t2.Text == " ")
                    label_FormFillet_t2_ViewN.Text = "0";
                else label_FormFillet_t2_ViewN.Text = textBox_FormLap_t2.Text;

                if (textBox_FormLap_NForce.Text == String.Empty || textBox_FormLap_NForce.Text == " ")
                    label_FormFillet_N_ViewN.Text = "0";
                else label_FormFillet_N_ViewN.Text = textBox_FormLap_NForce.Text;
            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewN.Text = "0";
                label_FormFillet_L2_ViewN.Text = "0";
                label_FormFillet_L3_ViewN.Text = "0";
                label_FormFillet_L4_ViewN.Text = "0";
                label_FormFillet_t1_ViewN.Text = "0";
                label_FormFillet_t2_ViewN.Text = "0";
                label_FormFillet_N_ViewN.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M)
            if (radioButton_FormLap_M.Checked)
            {
                if (textBox_FormLap_L1.Text == String.Empty || textBox_FormLap_L1.Text == " ")
                    label_FormFillet_L1_ViewM.Text = "0";
                else label_FormFillet_L1_ViewM.Text = textBox_FormLap_L1.Text;

                if (textBox_FormLap_L2.Text == String.Empty || textBox_FormLap_L2.Text == " ")
                    label_FormFillet_L2_ViewM.Text = "0";
                else label_FormFillet_L2_ViewM.Text = textBox_FormLap_L2.Text;

                if (textBox_FormLap_L3.Text == String.Empty || textBox_FormLap_L3.Text == " ")
                    label_FormFillet_L3_ViewM.Text = "0";
                else label_FormFillet_L3_ViewM.Text = textBox_FormLap_L3.Text;

                if (textBox_FormLap_L4.Text == String.Empty || textBox_FormLap_L4.Text == " ")
                    label_FormFillet_L4_ViewM.Text = "0";
                else label_FormFillet_L4_ViewM.Text = textBox_FormLap_L4.Text;

                if (textBox_FormLap_t1.Text == String.Empty || textBox_FormLap_t1.Text == " ")
                    label_FormFillet_t1_ViewM.Text = "0";
                else label_FormFillet_t1_ViewM.Text = textBox_FormLap_t1.Text;

                if (textBox_FormLap_t2.Text == String.Empty || textBox_FormLap_t2.Text == " ")
                    label_FormFillet_t2_ViewM.Text = "0";
                else label_FormFillet_t2_ViewM.Text = textBox_FormLap_t2.Text;

                if (textBox_FormLap_MForce.Text == String.Empty || textBox_FormLap_MForce.Text == " ")
                    label_FormFillet_M_ViewM.Text = "0";
                else label_FormFillet_M_ViewM.Text = textBox_FormLap_MForce.Text;

            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewM.Text = "0";
                label_FormFillet_L2_ViewM.Text = "0";
                label_FormFillet_L3_ViewM.Text = "0";
                label_FormFillet_L4_ViewM.Text = "0";
                label_FormFillet_t1_ViewM.Text = "0";
                label_FormFillet_t2_ViewM.Text = "0";
                label_FormFillet_M_ViewM.Text = "0";
            }

            // Обновление, если активное окно (перерезающие усилие Q с ексцентриситетом e)
            if (radioButton_FormLap_Q.Checked)
            {
                if (textBox_FormLap_L1.Text == String.Empty || textBox_FormLap_L1.Text == " ")
                    label_FormFillet_L1_ViewQ.Text = "0";
                else label_FormFillet_L1_ViewQ.Text = textBox_FormLap_L1.Text;

                if (textBox_FormLap_L2.Text == String.Empty || textBox_FormLap_L2.Text == " ")
                    label_FormFillet_L2_ViewQ.Text = "0";
                else label_FormFillet_L2_ViewQ.Text = textBox_FormLap_L2.Text;

                if (textBox_FormLap_L3.Text == String.Empty || textBox_FormLap_L3.Text == " ")
                    label_FormFillet_L3_ViewQ.Text = "0";
                else label_FormFillet_L3_ViewQ.Text = textBox_FormLap_L3.Text;

                if (textBox_FormLap_L4.Text == String.Empty || textBox_FormLap_L4.Text == " ")
                    label_FormFillet_L4_ViewQ.Text = "0";
                else label_FormFillet_L4_ViewQ.Text = textBox_FormLap_L4.Text;

                if (textBox_FormLap_t1.Text == String.Empty || textBox_FormLap_t1.Text == " ")
                    label_FormFillet_t1_ViewQ.Text = "0";
                else label_FormFillet_t1_ViewQ.Text = textBox_FormLap_t1.Text;

                if (textBox_FormLap_t2.Text == String.Empty || textBox_FormLap_t2.Text == " ")
                    label_FormFillet_t2_ViewQ.Text = "0";
                else label_FormFillet_t2_ViewQ.Text = textBox_FormLap_t2.Text;

                if (textBox_FormLap_QForce.Text == String.Empty || textBox_FormLap_QForce.Text == " ")
                    label_FormFillet_Q_ViewQ.Text = "0";
                else label_FormFillet_Q_ViewQ.Text = textBox_FormLap_QForce.Text;

                if (textBox_FormLap_e.Text == String.Empty || textBox_FormLap_e.Text == " ")
                    label_FormFillet_e_ViewQ.Text = "0";
                else label_FormFillet_e_ViewQ.Text = textBox_FormLap_e.Text;

            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewQ.Text = "0";
                label_FormFillet_L2_ViewQ.Text = "0";
                label_FormFillet_L3_ViewQ.Text = "0";
                label_FormFillet_L4_ViewQ.Text = "0";
                label_FormFillet_t1_ViewQ.Text = "0";
                label_FormFillet_t2_ViewQ.Text = "0";
                label_FormFillet_Q_ViewQ.Text = "0";
                label_FormFillet_e_ViewQ.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M и Осевое усилие N)
            if (radioButton_FormLap_MN.Checked)
            {
                if (textBox_FormLap_L1.Text == String.Empty || textBox_FormLap_L1.Text == " ")
                    label_FormFillet_L1_ViewMN.Text = "0";
                else label_FormFillet_L1_ViewMN.Text = textBox_FormLap_L1.Text;

                if (textBox_FormLap_L2.Text == String.Empty || textBox_FormLap_L2.Text == " ")
                    label_FormFillet_L2_ViewMN.Text = "0";
                else label_FormFillet_L2_ViewMN.Text = textBox_FormLap_L2.Text;

                if (textBox_FormLap_L3.Text == String.Empty || textBox_FormLap_L3.Text == " ")
                    label_FormFillet_L3_ViewMN.Text = "0";
                else label_FormFillet_L3_ViewMN.Text = textBox_FormLap_L3.Text;

                if (textBox_FormLap_L4.Text == String.Empty || textBox_FormLap_L4.Text == " ")
                    label_FormFillet_L4_ViewMN.Text = "0";
                else label_FormFillet_L4_ViewMN.Text = textBox_FormLap_L4.Text;

                if (textBox_FormLap_t1.Text == String.Empty || textBox_FormLap_t1.Text == " ")
                    label_FormFillet_t1_ViewMN.Text = "0";
                else label_FormFillet_t1_ViewMN.Text = textBox_FormLap_t1.Text;

                if (textBox_FormLap_t2.Text == String.Empty || textBox_FormLap_t2.Text == " ")
                    label_FormFillet_t2_ViewMN.Text = "0";
                else label_FormFillet_t2_ViewMN.Text = textBox_FormLap_t2.Text;

                if (textBox_FormLap_MForce.Text == String.Empty || textBox_FormLap_MForce.Text == " ")
                    label_FormFillet_M_ViewMN.Text = "0";
                else label_FormFillet_M_ViewMN.Text = textBox_FormLap_MForce.Text;

                if (textBox_FormLap_NForce.Text == String.Empty || textBox_FormLap_NForce.Text == " ")
                    label_FormFillet_N_ViewMN.Text = "0";
                else label_FormFillet_N_ViewMN.Text = textBox_FormLap_NForce.Text;

            }
            else
            {
                label_FormFillet_L1_ViewMN.Text = "0";
                label_FormFillet_L2_ViewMN.Text = "0";
                label_FormFillet_L3_ViewMN.Text = "0";
                label_FormFillet_L4_ViewMN.Text = "0";
                label_FormFillet_t1_ViewMN.Text = "0";
                label_FormFillet_t2_ViewMN.Text = "0";
                label_FormFillet_N_ViewMN.Text = "0";
                label_FormFillet_M_ViewMN.Text = "0";
            }

        }
        #endregion

        private void timer_FormLap_Checked_Tick(object sender, EventArgs e)
        {
            if (radioButton_FormLap_N.Checked == true ||
                radioButton_FormLap_M.Checked == true ||
                radioButton_FormLap_Q.Checked == true ||
                radioButton_FormLap_MN.Checked == true)
            {
                timer_FormLap.Start();
                timer_FormLap.Interval = 100;
            }
        }

        private void button_FormLap_Checking_Click(object sender, EventArgs e)
        {
            #region Параметры для стратегии
            // Передать параметры для стратегии расчета
            IStrategyCheckingByForcesType calculateStrategyWeld = new CalculateLapWeld();
            calculateStrategyWeld.AddParam(
                textBox_FormLap_L1.Text,
                textBox_FormLap_L2.Text,
                textBox_FormLap_L3.Text,
                textBox_FormLap_L4.Text,
                textBox_FormLap_t1.Text,
                textBox_FormLap_t2.Text,
                textBox_FormLap_Kf.Text,
                textBox_FormLap_e.Text,
                textBox_FormLap_NForce.Text,
                textBox_FormLap_MForce.Text,
                textBox_FormLap_QForce.Text,
                comboBox_FormLap_Steel.SelectedIndex.ToString(),
                comboBox_FormLap_ControlType.SelectedIndex.ToString(),
                comboBox_FormLap_WeldType.SelectedIndex.ToString()
                );
            #endregion

            // Создание объекта сбора ошибок
            DataError dataError = new DataError();

            #region Проверка активного типа расчета
            if (radioButton_FormLap_N.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L2.Text)) dataError.AddError("Поле L2 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L3.Text)) dataError.AddError("Поле L3 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L4.Text)) dataError.AddError("Поле L4 не число!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormLap_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                //textBox_FormFillet_NForce.Text = "0";
                textBox_FormLap_MForce.Text = "0";
                textBox_FormLap_QForce.Text = "0";
                textBox_FormLap_e.Text = "0";
            }
            if (radioButton_FormLap_M.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L2.Text)) dataError.AddError("Поле L2 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L3.Text)) dataError.AddError("Поле L3 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L4.Text)) dataError.AddError("Поле L4 не число!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                textBox_FormLap_NForce.Text = "0";
                //textBox_FormLap_MForce.Text = "0";
                textBox_FormLap_QForce.Text = "0";
                textBox_FormLap_e.Text = "0";
            }
            if (radioButton_FormLap_Q.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L2.Text)) dataError.AddError("Поле L2 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L3.Text)) dataError.AddError("Поле L3 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L4.Text)) dataError.AddError("Поле L4 не число!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                textBox_FormLap_NForce.Text = "0";
                textBox_FormLap_MForce.Text = "0";
                //textBox_FormLap_QForce.Text = "0";
                //textBox_FormLap_e.Text = "0";
            }
            if (radioButton_FormLap_MN.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L2.Text)) dataError.AddError("Поле L2 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L3.Text)) dataError.AddError("Поле L3 не число!");
                if (!CheckFieldByInt.CheckIntWithoutNull(textBox_FormLap_L4.Text)) dataError.AddError("Поле L4 не число!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_t2.Text)) dataError.AddError("Поле t2 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormLap_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormLap_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormLap_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                //textBox_FormLap_NForce.Text = "0";
                //textBox_FormLap_MForce.Text = "0";
                textBox_FormLap_QForce.Text = "0";
                textBox_FormLap_e.Text = "0";
            }
            #endregion

            #region Расчет конкретного типа стыкового шва
            // Если ошибок нет, то продолжить
            if (dataError.Count() <= 0 && CheckingWeldSize.CheckSize(textBox_FormLap_t1.Text, textBox_FormLap_t2.Text, textBox_FormLap_Kf.Text))
            {
                // Для нахлесточного шва N
                if (radioButton_FormLap_N.Checked)
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

                // Для нахлесточного шва M
                if (radioButton_FormLap_M.Checked)
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

                // Для нахлесточного шва Q
                if (radioButton_FormLap_Q.Checked)
                {
                    // Результирующие нормальные напряжения
                    double resultNormalTension = Math.Abs(calculateStrategyWeld.CheckForceQ());
                    resultNormalTension = Math.Round(resultNormalTension, 2);

                    if (resultNormalTension < 1) MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " < 1.0", "Проверка прошла успешно!");
                    else
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Коэффициент использования = " + resultNormalTension.ToString() + " > 1.0", "Проверка не прошла!");
                    }
                }

                // Для нахлесточного шва MN
                if (radioButton_FormLap_Q.Checked)
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

            }

            #endregion
        }

        private void button_FormFillet_Calculate_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            MessageBox.Show("Недоступно для бесплатной версии", "Ошибка");
        }
    }
}
