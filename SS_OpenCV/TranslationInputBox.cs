using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class TranslationInputBox : Form
    {
        public TranslationInputBox()
        {
            InitializeComponent();
        }
        public TranslationInputBox(string _title)
        {
            InitializeComponent();

            this.Text = _title;

        }

        public static Tuple<int, int> GetValue(string title)
        {
            TranslationInputBox form = new TranslationInputBox();
            form.Text = title;

            form.button1.Click += form.button1_Click;

            if (form.ShowDialog() == DialogResult.OK)
                return new Tuple<int, int>(Convert.ToInt32(form.translationX.Text), Convert.ToInt32(form.translationY.Text));
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TranslationInputBox_Load(object sender, EventArgs e)
        {

        }
    }
}