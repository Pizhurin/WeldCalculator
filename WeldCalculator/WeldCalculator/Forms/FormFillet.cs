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
    public partial class FormFillet : Form
    {
        public FormFillet()
        {
            InitializeComponent();
            comboBox_FormFillet_Steel.SelectedIndex = 0;
            comboBox_FormFillet_ControlType.SelectedIndex = 0;
            comboBox_FormFillet_WeldType.SelectedIndex = 0;
            comboBox_FormFillet_WeldMultiline.SelectedIndex = 0;

            timer_FormFilletChecked.Start();
            timer_FormFilletChecked.Interval = 500;
        }

        private void timer_FormFilletChecked_Tick(object sender, EventArgs e)
        {
            if (radioButton_FormFillet_N.Checked == true ||
               radioButton_FormFillet_M.Checked == true ||
               radioButton_FormFillet_Q.Checked == true ||
               radioButton_FormFillet_MN.Checked == true)
            {
                timer_FormFillet.Start();
                timer_FormFillet.Interval = 100;
            }
        }

        #region Синхронизация полей и значений на картинке в зависимости от активного типа расчета
        // Таймер обновления данных на картинке в соответсвии с полями формы
        private void timer_FormFillet_Tick(object sender, EventArgs e)
        {
            // Обновление, если активное окно (Осевое усилие N)
            if (radioButton_FormFillet_N.Checked)
            {
                // Установка картинки в зависимости от выбранного типа шва (односторонний или двусторонний)
                if (comboBox_FormFillet_WeldMultiline.SelectedIndex == 1)
                {
                    pictureBox_FormFillet_N.Image = new Bitmap("Fillet_N1.png");
                    pictureBox_FormFillet_N.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox_FormFillet_N.Image = new Bitmap("Fillet_N.png");
                    pictureBox_FormFillet_N.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (textBox_FormFillet_L1.Text == String.Empty || textBox_FormFillet_L1.Text == " ")
                    label_FormFillet_L1_ViewN.Text = "0";
                else label_FormFillet_L1_ViewN.Text = textBox_FormFillet_L1.Text;

                if (textBox_FormFillet_t1.Text == String.Empty || textBox_FormFillet_t1.Text == " ")
                    label_FormFillet_t1_ViewN.Text = "0";
                else label_FormFillet_t1_ViewN.Text = textBox_FormFillet_t1.Text;

                if (textBox_FormFillet_NForce.Text == String.Empty || textBox_FormFillet_NForce.Text == " ")
                    label_FormFillet_N_ViewN.Text = "0";
                else label_FormFillet_N_ViewN.Text = textBox_FormFillet_NForce.Text;

                if (textBox_FormFillet_Kf.Text == String.Empty || textBox_FormFillet_Kf.Text == " ")
                    label_FormFillet_Kf_ViewN.Text = "Kf = 0";
                else label_FormFillet_Kf_ViewN.Text = "Kf = " + textBox_FormFillet_Kf.Text;
            }
            else
            {
                //Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewN.Text = "0"; 
                label_FormFillet_t1_ViewN.Text = "0";
                label_FormFillet_N_ViewN.Text = "0";
                label_FormFillet_Kf_ViewN.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M)
            if (radioButton_FormFillet_M.Checked)
            {
                // Установка картинки в зависимости от выбранного типа шва (односторонний или двусторонний)
                if (comboBox_FormFillet_WeldMultiline.SelectedIndex == 1)
                {
                    pictureBox_FormFillet_M.Image = new Bitmap("Fillet_M1.png");
                    pictureBox_FormFillet_M.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox_FormFillet_M.Image = new Bitmap("Fillet_M.png");
                    pictureBox_FormFillet_M.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (textBox_FormFillet_L1.Text == String.Empty || textBox_FormFillet_L1.Text == " ")
                    label_FormFillet_L1_ViewM.Text = "0";
                else label_FormFillet_L1_ViewM.Text = textBox_FormFillet_L1.Text;

                if (textBox_FormFillet_t1.Text == String.Empty || textBox_FormFillet_t1.Text == " ")
                    label_FormFillet_t1_ViewM.Text = "0";
                else label_FormFillet_t1_ViewM.Text = textBox_FormFillet_t1.Text;

                if (textBox_FormFillet_MForce.Text == String.Empty || textBox_FormFillet_MForce.Text == " ")
                    label_FormFillet_M_ViewM.Text = "0";
                else label_FormFillet_M_ViewM.Text = textBox_FormFillet_MForce.Text;

                if (textBox_FormFillet_Kf.Text == String.Empty || textBox_FormFillet_Kf.Text == " ")
                    label_FormFillet_Kf_ViewM.Text = "Kf = 0";
                else label_FormFillet_Kf_ViewM.Text = "Kf = " + textBox_FormFillet_Kf.Text;
            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewM.Text = "0";
                label_FormFillet_t1_ViewM.Text = "0";
                //label_FormFillet_N_ViewM.Text = "0";
                label_FormFillet_M_ViewM.Text = "0";
                //label_FormFillet_Q_ViewQ.Text = "0";
                //label_FormFillet_e_ViewQ.Text = "0";
            }

            // Обновление, если активное окно (перерезающие усилие Q с ексцентриситетом e)
            if (radioButton_FormFillet_Q.Checked)
            {
                // Установка картинки в зависимости от выбранного типа шва (односторонний или двусторонний)
                if (comboBox_FormFillet_WeldMultiline.SelectedIndex == 1)
                {
                    pictureBox_FormFillet_Q.Image = new Bitmap("Fillet_Q1.png");
                    pictureBox_FormFillet_Q.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox_FormFillet_Q.Image = new Bitmap("Fillet_Q.png");
                    pictureBox_FormFillet_Q.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (textBox_FormFillet_L1.Text == String.Empty || textBox_FormFillet_L1.Text == " ")
                    label_FormFillet_L1_ViewQ.Text = "0";
                else label_FormFillet_L1_ViewQ.Text = textBox_FormFillet_L1.Text;

                if (textBox_FormFillet_t1.Text == String.Empty || textBox_FormFillet_t1.Text == " ")
                    label_FormFillet_t1_ViewQ.Text = "0";
                else label_FormFillet_t1_ViewQ.Text = textBox_FormFillet_t1.Text;

                if (textBox_FormFillet_QForce.Text == String.Empty || textBox_FormFillet_QForce.Text == " ")
                    label_FormFillet_Q_ViewQ.Text = "0";
                else label_FormFillet_Q_ViewQ.Text = textBox_FormFillet_QForce.Text;

                if (textBox_FormFillet_e.Text == String.Empty || textBox_FormFillet_e.Text == " ")
                    label_FormFillet_e_ViewQ.Text = "0";
                else label_FormFillet_e_ViewQ.Text = textBox_FormFillet_e.Text;

                if (textBox_FormFillet_Kf.Text == String.Empty || textBox_FormFillet_Kf.Text == " ")
                    label_FormFillet_Kf_ViewQ.Text = "Kf = 0";
                else label_FormFillet_Kf_ViewQ.Text = "Kf = " + textBox_FormFillet_Kf.Text;

            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewQ.Text = "0";
                label_FormFillet_t1_ViewQ.Text = "0";
                //label_FormFillet_N_ViewQ.Text = "0";
                //label_FormFillet_M_ViewQ.Text = "0";
                label_FormFillet_Q_ViewQ.Text = "0";
                label_FormFillet_e_ViewQ.Text = "0";
            }

            // Обновление, если активное окно (Изгибающий момент M и Осевое усилие N)
            if (radioButton_FormFillet_MN.Checked)
            {
                // Установка картинки в зависимости от выбранного типа шва (односторонний или двусторонний)
                if (comboBox_FormFillet_WeldMultiline.SelectedIndex == 1)
                {
                    pictureBox_FormFillet_MN.Image = new Bitmap("Fillet_MN1.png");
                    pictureBox_FormFillet_MN.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox_FormFillet_MN.Image = new Bitmap("Fillet_MN.png");
                    pictureBox_FormFillet_MN.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (textBox_FormFillet_L1.Text == String.Empty || textBox_FormFillet_L1.Text == " ")
                    label_FormFillet_L1_ViewMN.Text = "0";
                else label_FormFillet_L1_ViewMN.Text = textBox_FormFillet_L1.Text;

                if (textBox_FormFillet_t1.Text == String.Empty || textBox_FormFillet_t1.Text == " ")
                    label_FormFillet_t1_ViewMN.Text = "0";
                else label_FormFillet_t1_ViewMN.Text = textBox_FormFillet_t1.Text;

                if (textBox_FormFillet_MForce.Text == String.Empty || textBox_FormFillet_MForce.Text == " ")
                    label_FormFillet_M_ViewMN.Text = "0";
                else label_FormFillet_M_ViewMN.Text = textBox_FormFillet_MForce.Text;

                if (textBox_FormFillet_NForce.Text == String.Empty || textBox_FormFillet_NForce.Text == " ")
                    label_FormFillet_N_ViewMN.Text = "0";
                else label_FormFillet_N_ViewMN.Text = textBox_FormFillet_NForce.Text;

                if (textBox_FormFillet_Kf.Text == String.Empty || textBox_FormFillet_Kf.Text == " ")
                    label_FormFillet_Kf_ViewMN.Text = "Kf = 0";
                else label_FormFillet_Kf_ViewMN.Text = "Kf = " + textBox_FormFillet_Kf.Text;
            }
            else
            {
                // Если окно с типами усилий становится не активным, сбросить все значения на <0>
                label_FormFillet_L1_ViewMN.Text = "0";
                label_FormFillet_t1_ViewMN.Text = "0";
                label_FormFillet_N_ViewMN.Text = "0";
                label_FormFillet_M_ViewMN.Text = "0";
                //label_FormFillet_Q_ViewQ.Text = "0";
                //label_FormFillet_e_ViewQ.Text = "0";
            }
        }
        #endregion

        #region Проверка работы сварного шва
        private void button_FormFillet_Checking_Click(object sender, EventArgs e)
        {
            #region Параметры для стратегии
            // Передать параметры для стратегии расчета
            IStrategyCheckingByForcesType calculateStrategyWeld = new CalculateFilletWeld();
            calculateStrategyWeld.AddParam(
                textBox_FormFillet_L1.Text,
                textBox_FormFillet_t1.Text,
                textBox_FormFillet_Kf.Text,
                textBox_FormFillet_e.Text,
                textBox_FormFillet_NForce.Text,
                textBox_FormFillet_MForce.Text,
                textBox_FormFillet_QForce.Text,
                comboBox_FormFillet_Steel.SelectedIndex.ToString(),
                comboBox_FormFillet_ControlType.SelectedIndex.ToString(),
                comboBox_FormFillet_WeldType.SelectedIndex.ToString(),
                comboBox_FormFillet_WeldMultiline.SelectedIndex.ToString()
                );
            #endregion

            // Создание объекта сбора ошибок
            DataError dataError = new DataError();

            #region Проверка активного типа расчета
            if (radioButton_FormFillet_N.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormFillet_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                //textBox_FormFillet_NForce.Text = "0";
                textBox_FormFillet_MForce.Text = "0";
                textBox_FormFillet_QForce.Text = "0";
                textBox_FormFillet_e.Text = "0";
            }
            if (radioButton_FormFillet_M.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormFillet_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                textBox_FormFillet_NForce.Text = "0";
                //textBox_FormFillet_MForce.Text = "0";
                textBox_FormFillet_QForce.Text = "0";
                textBox_FormFillet_e.Text = "0";
            }
            if (radioButton_FormFillet_Q.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                textBox_FormFillet_NForce.Text = "0";
                textBox_FormFillet_MForce.Text = "0";
                //textBox_FormFillet_QForce.Text = "0";
                //textBox_FormFillet_e.Text = "0";
            }
            if (radioButton_FormFillet_MN.Checked)
            {
                // Перед расчетом проведем проверку, что во всех полях числа
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_L1.Text)) dataError.AddError("Поле L1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_t1.Text)) dataError.AddError("Поле t1 не число или равно <0>!");
                if (!CheckFieldByInt.CheckInt(textBox_FormFillet_Kf.Text)) dataError.AddError("Поле Kf не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_NForce.Text)) dataError.AddError("Поле N не число или равно <0>!");
                if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_MForce.Text)) dataError.AddError("Поле M не число или равно <0>!");
                //if (!CheckFieldByDouble.CheckDouble(textBox_FormFillet_QForce.Text)) dataError.AddError("Поле Q не число или равно <0>!");
                //if (!CheckFieldByInt.CheckInt(textBox_FormFillet_e.Text)) dataError.AddError("Поле <e> не число или равно <0>!");
                dataError.ShowErrors();

                // Неактивные поля (N, M, Q) обнулим, чтобы не было ошибки пустого поля
                //textBox_FormFillet_NForce.Text = "0";
                //textBox_FormFillet_MForce.Text = "0";
                textBox_FormFillet_QForce.Text = "0";
                textBox_FormFillet_e.Text = "0";
            }
            #endregion/

            #region Расчет конкретного типа стыкового шва
            // Если ошибок нет, то продолжить
            if (dataError.Count() <= 0 && CheckingWeldSize.CheckSize(textBox_FormFillet_t1.Text, textBox_FormFillet_Kf.Text))
            {
                // Для углового соединения N
                if (radioButton_FormFillet_N.Checked)
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

                // Для углового соединения M
                if (radioButton_FormFillet_M.Checked)
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

                // Для углового соединения Q
                if (radioButton_FormFillet_Q.Checked)
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

                // Для углового соединения MN
                if (radioButton_FormFillet_MN.Checked)
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
        #endregion

        private void button_FormFillet_Calculate_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            MessageBox.Show("Недоступно для бесплатной версии", "Ошибка");
        }
    }
}
