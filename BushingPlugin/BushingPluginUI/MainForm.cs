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
    public partial class MainForm : Form
    {
        private KompasWrapper _kompasWrapper;

        public MainForm()
        {
            InitializeComponent();
            _kompasWrapper = new KompasWrapper();
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            Bushing bushing = null;
            double newTotalLength = double.Parse(TotalLengthTextBox.Text);
            double newTopLength = double.Parse(TopLengthTextBox.Text);
            double newTopDiametr = double.Parse(TopDiametrTextBox.Text);
            double newOuterDiametr = double.Parse(OuterDiametrTextBox.Text);
            bushing = new Bushing(newTotalLength, newTopLength, newTopDiametr, newOuterDiametr);
            _kompasWrapper.StartKompas();
            _kompasWrapper.BuildBushing(bushing);
        }
    }
}
