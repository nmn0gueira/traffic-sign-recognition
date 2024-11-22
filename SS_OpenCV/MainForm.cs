using System;
using System.Collections.Generic;
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
        int mouseX, mouseY;
        bool mouseFlag = false;

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

        private void manualThresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            SingleInputBox form = new SingleInputBox("Binarization", "Threshold:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                int threshold = Convert.ToInt32(form.ValueTextBox.Text);

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.ConvertToBW(img, threshold);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void otsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToBW_Otsu(img);

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

        private void hSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            //img.Convert<Hsv, byte>().CopyTo(img);
            ImageClass.ImageRGBtoHSV(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            

            DoubleInputBox form = new DoubleInputBox("Translation", "X-Axis:", "Y-Axis:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                int x = Convert.ToInt32(form.ValueTextBox1.Text);
                int y = Convert.ToInt32(form.ValueTextBox2.Text);

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Translation(img, imgUndo, x, y);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void nearestNeighborToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
           
            SingleInputBox form = new SingleInputBox("Rotation", "Radians:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float angle = Convert.ToSingle(form.ValueTextBox.Text);

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Rotation(img, imgUndo, angle);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void bilinearInterpolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            SingleInputBox form = new SingleInputBox("Rotation", "Radians:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float angle = Convert.ToSingle(form.ValueTextBox.Text);

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Rotation_Bilinear(img, imgUndo, angle);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void nearestNeighborToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;        

            SingleInputBox form = new SingleInputBox("Scale", "Factor:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float factor = Convert.ToSingle(form.ValueTextBox.Text);

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Scale(img, imgUndo, factor);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void bilinearInterpolationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            SingleInputBox form = new SingleInputBox("Scale", "Factor:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float factor = Convert.ToSingle(form.ValueTextBox.Text);

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Scale_Bilinear(img, imgUndo, factor);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void nearestNeighborToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            SingleInputBox form = new SingleInputBox("Zoom", "Factor:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float factor = Convert.ToSingle(form.ValueTextBox.Text);                

                // get mouse coordinates using mouseclick event
                mouseFlag = true;
                while (mouseFlag) // wait for mouse click
                {
                    Application.DoEvents();
                }

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Scale_point_xy(img, imgUndo, factor, mouseX, mouseY);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void bilinearInterpolationToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            

            SingleInputBox form = new SingleInputBox("Zoom", "Factor:");
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                float factor = Convert.ToSingle(form.ValueTextBox.Text);          

                // get mouse coordinates using mouseclick event
                mouseFlag = true;
                while (mouseFlag) // wait for mouse click
                {
                    Application.DoEvents();
                }

                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.Scale_point_xy_Bilinear(img, imgUndo, factor, mouseX, mouseY);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
            }

            Cursor = Cursors.Default; // normal cursor
        }

        private void quadraticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.MeanQuadratic(img, imgUndo, 3);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void linearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.MeanLinear(img, 3);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void constantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.MeanConstant(img, 7);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            FilterForm ff = new FilterForm();

            if (ff.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor; // clock cursor 

                //copy Undo Image
                imgUndo = img.Copy();

                ImageClass.NonUniform(img, imgUndo, ff.matrix3, ff.weight, ff.offset);

                ImageViewer.Image = img;
                ImageViewer.Refresh(); // refresh image on the screen
                
            }
            Cursor = Cursors.Default; // normal cursor
        }

        private void sobrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Sobel(img, imgUndo);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void differentiationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Diferentiation(img, imgUndo);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Roberts(img, imgUndo);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.MedianBlur(img, imgUndo, 3);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.MedianBlur3D(img, imgUndo, 3);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void grayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            int[] histogram = ImageClass.Histogram_Gray(img);

            // Prepare the histograms for display
            var histograms = new Dictionary<string, (int[] Histogram, Color Color)>
            {
                { "Gray", (histogram, Color.Gray) }
            };

            // Show the tabbed histogram form
            HistogramForm hf = new HistogramForm(histograms);
            hf.ShowDialog();

            Cursor = Cursors.Default; // normal cursor
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            int[,] histogram = ImageClass.Histogram_RGB(img);
            int[] redHistogram = new int[256];
            int[] greenHistogram = new int[256];
            int[] blueHistogram = new int[256];

            for (int i = 0; i < 256; i++)
            {
                redHistogram[i] = histogram[2, i];
                greenHistogram[i] = histogram[1, i];
                blueHistogram[i] = histogram[0, i];
            }

            // Prepare the histograms for display
            var histograms = new Dictionary<string, (int[] Histogram, Color Color)>
            {
                { "Red", (redHistogram, Color.Red) },
                { "Green", (greenHistogram, Color.Green) },
                { "Blue", (blueHistogram, Color.Blue) }
            };

            // Show the tabbed histogram form
            HistogramForm hf = new HistogramForm(histograms);
            hf.ShowDialog();

            Cursor = Cursors.Default; // Reset cursor
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            int[,] histogram = ImageClass.Histogram_All(img);
            int[] grayHistogram = new int[256];
            int[] redHistogram = new int[256];
            int[] greenHistogram = new int[256];
            int[] blueHistogram = new int[256];

            for (int i = 0; i < 256; i++)
            {
                grayHistogram[i] = histogram[0, i];
                redHistogram[i] = histogram[3, i];
                greenHistogram[i] = histogram[2, i];
                blueHistogram[i] = histogram[1, i];
            }

            // Prepare the histograms for display
            var histograms = new Dictionary<string, (int[] Histogram, Color Color)>
            {
                { "Gray", (grayHistogram, Color.Gray) },
                { "Red", (redHistogram, Color.Red) },
                { "Green", (greenHistogram, Color.Green) },
                { "Blue", (blueHistogram, Color.Blue) }
            };

            // Show the tabbed histogram form
            HistogramForm hf = new HistogramForm(histograms);
            hf.ShowDialog();

            Cursor = Cursors.Default; // Reset cursor
        }

        private void equalizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Equalization(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            bool[,] mask = new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } };

            ImageClass.Dilation(img, mask);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            bool[,] mask = new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } };

            ImageClass.Erosion(img, mask);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            bool[,] mask = new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } };

            ImageClass.Opening(img, mask);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void closureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            bool[,] mask = new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } };

            ImageClass.Closing(img, mask);

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

        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseFlag)
            {
                mouseX = e.X;
                mouseY = e.Y;

                mouseFlag = false;
            }
        }

        private void debugBinarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BinarizeOnRed(img);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void connectedCompDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            Image<Bgr, byte> imgCopy1 = img.Copy();
            Image<Bgr, byte> imgCopy2 = img.Copy();
            ImageClass.BinarizeOnRed(img);
            //CvInvoke.ConnectedComponentsWithStats(img.Convert<B, byte>(), imgUndo, imgCopy1, imgCopy2);

            ImageViewer.Image = img;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
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
    }
}