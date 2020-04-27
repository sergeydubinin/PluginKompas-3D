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
using BushingParametrs;

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

        /// <summary>
        /// Словарь, cвязывающий параметр втулки и соотвествующий ему textbox
        /// </summary>
        private Dictionary<ParametersType, TextBox> _bindTextBoxToParametr;

        /// <summary>
        /// Главная форма
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _kompasWrapper = new KompasWrapper();

            _bindTextBoxToParametr = new Dictionary<ParametersType, TextBox>();
            _bindTextBoxToParametr.Clear();
            _bindTextBoxToParametr.Add(ParametersType.TotalLength, TotalLengthTextBox);
            _bindTextBoxToParametr.Add(ParametersType.TopLength, TopLengthTextBox);
            _bindTextBoxToParametr.Add(ParametersType.TopDiametr, TopDiametrTextBox);
            _bindTextBoxToParametr.Add(ParametersType.OuterDiametr, OuterDiametrTextBox);
            _bindTextBoxToParametr.Add(ParametersType.InnerDiametr, InnerDiametrTextBox);
            _bindTextBoxToParametr.Add(ParametersType.NumberHoles, NumberHolesTextBox);
            _bindTextBoxToParametr.Add(ParametersType.HolesDiametr, HolesDiametrTextBox);
            _bindTextBoxToParametr.Add(ParametersType.LocationDiametr, LocationDiametrTextBox);

            TotalLengthTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            TopLengthTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            TopDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            OuterDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            InnerDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            NumberHolesTextBox.KeyPress += new KeyPressEventHandler(IsNumberPressed);
            HolesDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
            LocationDiametrTextBox.KeyPress += new KeyPressEventHandler(IsNumberOrDotPressed);
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

            if (bushing._listError.Count > 0)
            {
                ShowError(bushing._listError);
            }
            else
            {               
                _kompasWrapper.StartKompas();
                _kompasWrapper.BuildBushing(bushing);                
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
                MessageBox.Show("Ошибка в значении параметра!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Возврат исходного цвета textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBackColor(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BackColor = Color.White;
        }

        /// <summary>
        /// Метод, выводящий ошибку на экран и подсвечивающий textbox, связанный
        /// с данной ошибкой
        /// </summary>
        /// <param name="listError"></param>
        private void ShowError(Dictionary<ParametersType, string> listError)
        {
            string message = "";
            foreach (KeyValuePair<ParametersType, string> keyValue in listError)
            {

                if (_bindTextBoxToParametr.TryGetValue(keyValue.Key, out TextBox textBox))
                {
                    textBox.BackColor = Color.LightSalmon;
                    message += "*" + keyValue.Value + "\n" + "\n";
                }

            }
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
