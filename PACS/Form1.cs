using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PACS
{
    public partial class Form1 : Form
    {
        Communication mCommunication;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mCommunication = new Communication();
        }

        private void btnGetMeasurement_Click(object sender, EventArgs e)
        {
            string newValue;

            newValue = mCommunication.GetMeasurementRecords("a test UID");
            listBox1.Items.Add(newValue);
        }
    }
}
