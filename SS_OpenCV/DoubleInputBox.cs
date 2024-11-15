using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class DoubleInputBox : Form
    {
        public DoubleInputBox()
        {
            InitializeComponent();
        }
        public DoubleInputBox(string _title)
        {
            InitializeComponent();

            this.Text = _title;
        }

        public DoubleInputBox(string _title, string _label1, string _label2)
        {
            InitializeComponent();

            this.Text = _title;
            this.label1.Text = _label1;
            this.label2.Text = _label2;
        }

        public static string GetValue()
        {
            return GetValue("");
        }

        public static int GetIntValue(string title)
        {
            DoubleInputBox form = new DoubleInputBox();
            form.Text = title;

            form.button1.Click += form.button1_Click;

            if (form.ShowDialog() == DialogResult.OK)
                return Convert.ToInt32(form.ValueTextBox1.Text);
            return -1;
        }


        public static string GetValue(string title)
        {
            DoubleInputBox form = new DoubleInputBox();
            form.Text=title;
            if (form.ShowDialog() == DialogResult.OK)
               return form.ValueTextBox1.Text;
           return null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try {
               // Convert.ToInt32(ValueTextBox.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            } catch(Exception ex )
            {
                MessageBox.Show("Invalid number format");
            }
        }

        public static string GetValue(string title, string value)
        {
            DoubleInputBox form = new DoubleInputBox();
            form.Text = title;
            form.ValueTextBox1.Text = value;

            if (form.ShowDialog() == DialogResult.OK)
                return form.ValueTextBox1.Text;
            return null;
        }
    }
}