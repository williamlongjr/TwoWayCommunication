﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apollo
{
    public partial class Form1 : Form
    {

        PACSListener mListener;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mListener = new PACSListener();
            //mListener.Start("http://localhost:8080/ApolloService/");
			mListener.Start("net.tcp://localhost:8080/ApolloService/");


        }

        private void btnCallback_Click(object sender, EventArgs e)
        {
            mListener.MeasurementCallback("The index has changed");
        }
    }
}
