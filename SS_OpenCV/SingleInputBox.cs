using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class SingleInputBox : Form
    {
        public SingleInputBox()
        {
            InitializeComponent();
        }
        public SingleInputBox(string _title)
        {
            InitializeComponent();

            this.Text = _title;
        }

        public SingleInputBox(string _title, string _label)
        {
            InitializeComponent();

            this.Text = _title;
            this.label1.Text = _label;
        }

        public static string GetValue()
        {
            return GetValue("");
        }

        public static int GetIntValue(string title)
        {
            SingleInputBox form = new SingleInputBox();
            form.Text = title;

            form.button1.Click += form.button1_Click;

            if (form.ShowDialog() == DialogResult.OK)
                return Convert.ToInt32(form.ValueTextBox.Text);
            return -1;
        }


        public static string GetValue(string title)
        {
            SingleInputBox form = new SingleInputBox();
            form.Text=title;
            if (form.ShowDialog() == DialogResult.OK)
               return form.ValueTextBox.Text;
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
            SingleInputBox form = new SingleInputBox();
            form.Text = title;
            form.ValueTextBox.Text = value;

            if (form.ShowDialog() == DialogResult.OK)
                return form.ValueTextBox.Text;
            return null;
        }
    }
}