﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SS_OpenCV
{ 
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        /// <summary>
        /// Change visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        /// <summary>
        /// Call image convertion to gray scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void brightnessAndContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            DoubleInputBox form = new DoubleInputBox("Brightness and Contrast", "Brightness:", "Contrast:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                int brightness = Convert.ToInt32(form.ValueTextBox1.Text);
                int contrast = Convert.ToInt32(form.ValueTextBox2.Text);

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.BrightContrast(img, brightness, contrast);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void redChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }


        private void greenChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.GreenChannel(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void blueChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BlueChannel(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            DoubleInputBox form = new DoubleInputBox("Translation", "X-Axis:", "Y-Axis:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                int x = Convert.ToInt32(form.ValueTextBox1.Text);
                int y = Convert.ToInt32(form.ValueTextBox2.Text);

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Translation(img, imgUndo, x, y);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            SingleInputBox form = new SingleInputBox("Rotation", "Radians:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float angle = Convert.ToSingle(form.ValueTextBox.Text);

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Rotation_Bilinear(img, imgUndo, angle);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            //

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }



        /// <summary>
        /// Call automated image processing check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();

        }

        private void ImageViewer_MouseMove(object sender, MouseEventArgs e)
        {
            int aux_x = 0;
            int aux_y = 0;
            if (ImageViewer.SizeMode == PictureBoxSizeMode.Zoom)
            {
                aux_x = (int)(e.X / ImageViewer.ZoomScale + ImageViewer.HorizontalScrollBar.Value * ImageViewer.ZoomScale);
                aux_y = (int)(e.Y / ImageViewer.ZoomScale + ImageViewer.VerticalScrollBar.Value * ImageViewer.ZoomScale);

            }
            else
            {
                aux_x = (int)(e.X / ImageViewer.ZoomScale + ImageViewer.HorizontalScrollBar.Value * ImageViewer.ZoomScale);
                aux_y = (int)(e.Y / ImageViewer.ZoomScale + ImageViewer.VerticalScrollBar.Value * ImageViewer.ZoomScale);
            }


            if (img != null && aux_y < img.Height && aux_x < img.Width)
                statusLabel.Text = "X:" + aux_x + "  Y:" + aux_y + " - BGR = (" + img.Data[aux_y, aux_x, 0] + "," + img.Data[aux_y, aux_x, 1] + "," + img.Data[aux_y, aux_x, 2] + ")";

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterForm ff = new FilterForm();

            if (ff.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                //ImageClass.NonUniformFilter(img, ff.Mask);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen

                Cursor = Cursors.Default; // normal cursor
            }
        }

        private void hSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            img.Convert<Hsv, byte>().CopyTo(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void yCrCbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ImageRGBtoYCrCb(img.MIplImage);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void yccTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ImageRGBtoYCrCb(img.MIplImage);
            ImageClass.ImageYCrCbtoRGB(img.MIplImage);

            /*
            img.Convert<Ycc, byte>().CopyTo(img);
            img.Convert<Bgr, byte>().CopyTo(img);*/

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }
    }
}