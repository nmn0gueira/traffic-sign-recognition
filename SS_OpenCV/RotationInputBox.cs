using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class RotationInputBox : Form
    {
        public RotationInputBox()
        {
            InitializeComponent();
        }
        public RotationInputBox(string _title)
        {
            InitializeComponent();

            this.Text = _title;

        }

        public static int GetIntValue(string title)
        {
            RotationInputBox form = new RotationInputBox();
            form.Text = title;

            form.button1.Click += form.button1_Click;

            if (form.ShowDialog() == DialogResult.OK)
                return Convert.ToInt32(form.angle.Text);
            return -1;
        }


        public static string GetValue(string title)
        {
            RotationInputBox form = new RotationInputBox();
            form.Text=title;
            if (form.ShowDialog() == DialogResult.OK)
               return form.angle.Text;
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
            RotationInputBox form = new RotationInputBox();
            form.Text = title;
            form.angle.Text = value;

            if (form.ShowDialog() == DialogResult.OK)
                return form.angle.Text;
            return null;
        }
    }
}