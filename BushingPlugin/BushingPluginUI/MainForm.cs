using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BushingPlugin;

namespace BushingPluginUI
{
    /// <summary>
    /// Главная форма программы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объект связи с Компас-3D
        /// </summary>
        private KompasWrapper _kompasWrapper;

        private Dictionary<ParametersType, TextBox> _dic;

        /// <summary>
        /// Главная форма
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _dic = new Dictionary<ParametersType, TextBox>();
            _dic.Clear();

            _dic.Add(ParametersType.TopLength, TopLengthTextBox);
            _dic.Add(ParametersType.TopDiametr, TopDiametrTextBox);
            _dic.Add(ParametersType.HolesDiametr, HolesDiametrTextBox);
            _dic.Add(ParametersType.TotalLength, TotalLengthTextBox);


            _kompasWrapper = new KompasWrapper();
            TotalLengthTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            TopLengthTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            TopDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            OuterDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            InnerDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            NumberHolesTextBox.KeyPress += new KeyPressEventHandler(IsNumberPressed);
            HolesDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            LocationDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
        }        

        private void MethodShowError(Dictionary<ParametersType,string> listError)
        {
            foreach (KeyValuePair<ParametersType, string> keyValue in listError)
            {
                if (_dic.TryGetValue(keyValue.Key, out TextBox textBox)){
                    ShowErrorMessage(textBox, keyValue.Value);
                }                
            }
        }
        /// <summary>
        /// Обработчик кнопки "Построить деталь"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            Bushing bushing = null;

                double newTotalLength = double.Parse(TotalLengthTextBox.Text);
                double newTopLength = double.Parse(TopLengthTextBox.Text);
                double newTopDiametr = double.Parse(TopDiametrTextBox.Text);
                double newOuterDiametr = double.Parse(OuterDiametrTextBox.Text);
                double newInnerDiametr = double.Parse(InnerDiametrTextBox.Text);
                int newNumberHoles = int.Parse(NumberHolesTextBox.Text);
                double newHolesDiametr = double.Parse(HolesDiametrTextBox.Text);
                double newLocationDiametr = double.Parse(LocationDiametrTextBox.Text);

                bushing = new Bushing(newTotalLength, newTopLength, newTopDiametr, newOuterDiametr, newInnerDiametr, newNumberHoles,
                    newHolesDiametr, newLocationDiametr);
            //catch (TopDiametrException exception)
            //{
            //    ShowErrorMessage(TopDiametrTextBox, exception.Message);
            //}
            //catch (OuterDiametrException exception)
            //{
            //    ShowErrorMessage(OuterDiametrTextBox, exception.Message);
            //}
            //catch (InnerDiametrException exception)
            //{
            //    ShowErrorMessage(InnerDiametrTextBox, exception.Message);
            //}
            //catch (NumberHolesException exception)
            //{
            //    ShowErrorMessage(NumberHolesTextBox, exception.Message);
            //}
            //catch (HolesDiametrException exception)
            //{
            //    ShowErrorMessage(HolesDiametrTextBox, exception.Message);
            //}
            //catch (LocationDiametrException exception)
            //{
            //    ShowErrorMessage(LocationDiametrTextBox, exception.Message);
            //}
            if (bushing._listError.Count > 0)
            {
                MethodShowError(bushing._listError);
            }
            else
            {
                if (bushing != null)
                {
                    _kompasWrapper.StartKompas();
                    _kompasWrapper.BuildBushing(bushing);
                }
            }
        }

        /// <summary>
        /// Проверка textbox на недопустимые значения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextboxValidation(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text == ""
                || textbox.Text == ".")
            {
                textbox.BackColor = Color.LightSalmon;
                ShowErrorMessage(textbox, "Ошибка в значении параметра!");
                textbox.Focus();
            }
        }

        /// <summary>
        /// Событие, проверяющее, чтобы textbox содержал только цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IsNumberPressed(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsControl(e.KeyChar))
                && !(Char.IsDigit(e.KeyChar))
            )
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Событие, проверяющее, чтобы textbox содержал только цифры и максимум
        /// один знак разделения (точку)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IsNumberOrDotPressed(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsControl(e.KeyChar))
                && !(Char.IsDigit(e.KeyChar))
                && !((e.KeyChar == '.')
                && (((TextBox)sender).Text.IndexOf(".") == -1)
            ))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Вывод сообщения об ошибке и подсветка соответствующего textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="message"></param>
        private void ShowErrorMessage(TextBox textBox, string message)
        {
            textBox.BackColor = Color.LightSalmon;
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Возврат исходного цвета textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBackColor(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BackColor = Color.White;
        }
    }
}