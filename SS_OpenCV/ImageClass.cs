using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.Management.Instrumentation;
using System.Windows.Forms;
using Emgu.CV.ImgHash;
using System.Linq;
using Emgu.CV.XFeatures2D;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Contracts;
using Emgu.CV.Cuda;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void NegativeSlow(Image<Bgr, byte> img)
        {
            int x, y;

            Bgr aux;
            for (y = 0; y < img.Height; y++)
            {
                for (x = 0; x < img.Width; x++)
                {
                    // acesso pela biblioteca : mais lento 
                    aux = img[y, x];
                    img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                }
            }
        }

        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            dataPtr[0] = (byte)(255 - dataPtr[0]);
                            dataPtr[1] = (byte)(255 - dataPtr[1]);
                            dataPtr[2] = (byte)(255 - dataPtr[2]);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                byte gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // convert to gray
                            gray = (byte)Math.Round(((int)dataPtr[0] + dataPtr[1] + dataPtr[2]) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            dataPtr[0] = dataPtr[2];
                            dataPtr[1] = dataPtr[2];

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void GreenChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            dataPtr[0] = dataPtr[1];
                            dataPtr[2] = dataPtr[1];

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BlueChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            dataPtr[1] = dataPtr[0];
                            dataPtr[2] = dataPtr[0];

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)

                // Lookup table for brightness and contrast adjustment
                byte[] lookupTable = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    int newValue = (int)Math.Round(i * contrast + bright);
                    lookupTable[i] = (byte)Clamp(newValue, 0, 255);
                }

                if (nChan == 3) // image in RGB
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // Apply the lookup table for each channel
                            dataPtr[0] = lookupTable[dataPtr[0]];
                            dataPtr[1] = lookupTable[dataPtr[1]];
                            dataPtr[2] = lookupTable[dataPtr[2]];

                            // Advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        // Advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the destination image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the source image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alignment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;

                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        yOrigin = yDestin - dy;
                        bool yOriginValid = yOrigin >= 0 && yOrigin < height;
                        byte* rowPtrOrigin = dataPtrCopy + yOrigin * widthStep;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = xDestin - dx;

                            if (!yOriginValid || xOrigin < 0 || xOrigin >= width)
                            {
                                // Out of bounds, set pixel to black
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }
                            else
                            {
                                byte* dataPtrAux = rowPtrOrigin + xOrigin * nChan;

                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }


        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;

                float halfHeight = height / 2.0f;
                float halfWidth = width / 2.0f;

                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);

                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        double aux1 = (halfHeight - yDestin) * sin - halfWidth;
                        double aux2 = (halfHeight - yDestin) * cos;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = (int)Math.Round((xDestin - halfWidth) * cos - aux1);
                            yOrigin = (int)Math.Round(halfHeight - (xDestin - halfWidth) * sin - aux2);

                            if (xOrigin < 0 || xOrigin >= width || yOrigin < 0 || yOrigin >= height)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }

                            else
                            {
                                byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;

                float inverseScaleFactor = 1 / scaleFactor;


                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        yOrigin = (int)Math.Round(yDestin * inverseScaleFactor);
                        bool yOriginValid = yOrigin >= 0 && yOrigin < height;
                        byte* rowPtrOrigin = dataPtrCopy + yOrigin * widthStep;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = (int)Math.Round(xDestin * inverseScaleFactor);

                            if (!yOriginValid || xOrigin < 0 || xOrigin >= width)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {
                                byte* dataPtrAux = rowPtrOrigin + xOrigin * nChan;
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }

                            dataPtr += nChan;

                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;


                float xOffset = centerX - (width - 1) / 2.0f / scaleFactor;
                float yOffset = centerY - (height - 1) / 2.0f / scaleFactor;

                float inverseScaleFactor = 1 / scaleFactor;


                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        yOrigin = (int)Math.Round(yDestin * inverseScaleFactor + yOffset);
                        bool yOriginValid = yOrigin >= 0 && yOrigin < height;
                        byte* rowPtrOrigin = dataPtrCopy + yOrigin * widthStep;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = (int)Math.Round(xDestin * inverseScaleFactor + xOffset);


                            if (!yOriginValid || xOrigin < 0 || xOrigin >= width)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {
                                byte* dataPtrAux = rowPtrOrigin + xOrigin * nChan;
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }

                            dataPtr += nChan;

                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;

                float halfHeight = height / 2.0f;
                float halfWidth = width / 2.0f;

                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);

                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        double aux1 = (halfHeight - yDestin) * sin - halfWidth;
                        double aux2 = (halfHeight - yDestin) * cos;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = (int)Math.Round((xDestin - halfWidth) * cos - aux1);
                            yOrigin = (int)Math.Round(halfHeight - (xDestin - halfWidth) * sin - aux2);

                            if (xOrigin < 0 || xOrigin >= width || yOrigin < 0 || yOrigin >= height)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }

                            else
                            {
                                byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Scale_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;

                float inverseScaleFactor = 1 / scaleFactor;


                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        yOrigin = (int)Math.Round(yDestin * inverseScaleFactor);
                        bool yOriginValid = yOrigin >= 0 && yOrigin < height;
                        byte* rowPtrOrigin = dataPtrCopy + yOrigin * widthStep;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = (int)Math.Round(xDestin * inverseScaleFactor);

                            if (!yOriginValid || xOrigin < 0 || xOrigin >= width)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {
                                byte* dataPtrAux = rowPtrOrigin + xOrigin * nChan;
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }

                            dataPtr += nChan;

                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Scale_point_xy_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;


                float xOffset = centerX - (width - 1) / 2.0f / scaleFactor;
                float yOffset = centerY - (height - 1) / 2.0f / scaleFactor;

                float inverseScaleFactor = 1 / scaleFactor;


                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        yOrigin = (int)Math.Round(yDestin * inverseScaleFactor + yOffset);
                        bool yOriginValid = yOrigin >= 0 && yOrigin < height;
                        byte* rowPtrOrigin = dataPtrCopy + yOrigin * widthStep;

                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            xOrigin = (int)Math.Round(xDestin * inverseScaleFactor + xOffset);


                            if (!yOriginValid || xOrigin < 0 || xOrigin >= width)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {
                                byte* dataPtrAux = rowPtrOrigin + xOrigin * nChan;
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }

                            dataPtr += nChan;

                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            Mean_solutionA(img, imgCopy, 3);
            //MeanSolutionAPadding(img, 3);
        }

        /// <summary>
        /// Applies a mean filter to an image with special treatment of the borders
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        /// <param name="dim"></param>
        public static void Mean_solutionA(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;
                int sumR, sumG, sumB;

                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                float count = dim * dim;
                int filterLineAdvance = widthStep - dim * nChan;

                if (nChan == 3)
                {
                    // Treat top border
                    for (yDestin = 0; yDestin < halfDim; yDestin++)
                    {
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;

                                    if (xOrigin < 0 || xOrigin >= width)
                                    {
                                        xOrigin = xDestin;
                                    }

                                    if (yOrigin < 0)
                                        yOrigin = 0;

                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }

                    // Treat left border
                    for (yDestin = halfDim; yDestin < height; yDestin++)
                    {
                        for (xDestin = 0; xDestin < halfDim; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;

                                    if (xOrigin < 0)
                                        xOrigin = 0;


                                    if (yOrigin >= height)
                                        yOrigin = yDestin;

                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }

                        // Advance the pointer to the next line
                        dataPtr += (width - halfDim) * nChan + padding;
                    }

                    dataPtr -= widthStep * halfDim;
                    dataPtr += nChan * halfDim;

                    // Treat bottom border
                    for (yDestin = height - halfDim; yDestin < height; yDestin++)
                    {
                        for (xDestin = halfDim; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;


                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;


                                    if (xOrigin >= width)
                                        xOrigin = xDestin;

                                    if (yOrigin >= height)
                                        yOrigin = height - 1;


                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }
                        dataPtr += padding + halfDim * nChan;
                    }
                    dataPtr = (byte*)m.ImageData.ToPointer() + widthStep * halfDim + (width - halfDim) * nChan;

                    // Treat right border
                    for (yDestin = halfDim; yDestin < height - halfDim; yDestin++)
                    {
                        
                        for (xDestin = width - halfDim; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;

                                    if (xOrigin >= width)
                                        xOrigin = width - 1;


                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }

                        dataPtr += padding + (width - halfDim) * nChan;

                    }
                    // Put the pointer in the first pixel of the first line of the core
                    dataPtr = (byte*)m.ImageData.ToPointer() + widthStep * halfDim + halfDim * nChan;

                    // Treat core
                    for (yDestin = halfDim; yDestin < height - halfDim; yDestin++)
                    {
                        byte* rowDataPtr = dataPtrCopy + (yDestin - halfDim) * widthStep;

                        for (xDestin = halfDim; xDestin < width - halfDim; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            byte* dataPtrAux = rowDataPtr + (xDestin - halfDim) * nChan;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {

                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }

                                dataPtrAux += filterLineAdvance;
                            }
                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }
                        dataPtr += padding + 2 * halfDim * nChan;
                    }
                }
            }
        }

        public static void Mean_solutionB(Image<Bgr, byte> img, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                float count = dim * dim;

                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, Byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
                // Pad the copy image using OpenCV
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, halfDim, halfDim, halfDim, halfDim, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int filterLineAdvance = widthStep - dim * nChan;
                int x, y;
                int sumR, sumG, sumB;

                if (nChan == 3) // image in RGB
                {
                    for (y = halfDim; y < height - halfDim; y++)
                    {
                        for (x = halfDim; x < width - halfDim; x++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (y - halfDim) * widthStep + (x - halfDim) * nChan;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }

                                dataPtrAux += filterLineAdvance;
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Mean_solutionC(Image<Bgr, byte> img, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                float count = dim * dim;

                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, Byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
                // Pad the copy image using OpenCV
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, halfDim, halfDim, halfDim, halfDim, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int filterLineAdvance = widthStep - dim * nChan;
                int x, y;
                int sumR, sumG, sumB;

                if (nChan == 3) // image in RGB
                {
                    for (y = halfDim; y < height - halfDim; y++)
                    {
                        for (x = halfDim; x < width - halfDim; x++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (y - halfDim) * widthStep + (x - halfDim) * nChan;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }

                                dataPtrAux += filterLineAdvance;
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }


        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight, float offset)
        {
            //float[,] testMatrix = new float[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
            //float testMatrixWeight = 9;
            if (IsMatrixSeparable(matrix, out float[] u, out float[] v))
                NonUniformSeparable(img, u, v, matrixWeight, offset);

            else
                NonUniformNoPad(img, imgCopy, matrix, matrixWeight, offset);
        }

        private static void NonUniformNoPad(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight, float offset)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int xDestin, yDestin, xOrigin, yOrigin;
                float sumR, sumG, sumB;

                int dim = matrix.GetLength(0);
                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                int filterLineAdvance = widthStep - dim * nChan;

                if (nChan == 3) // image in RGB
                {
                    // Treat top border
                    for (yDestin = 0; yDestin < halfDim; yDestin++)
                    {
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;


                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {
                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;

                                    if (xOrigin < 0 || xOrigin >= width)
                                    {
                                        xOrigin = xDestin;
                                    }

                                    if (yOrigin < 0)
                                        yOrigin = 0;

                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0];
                                    sumG += matrix[matrixRow, matrixCol] * dataPtrAux[1];
                                    sumR += matrix[matrixRow, matrixCol] * dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }

                    // Treat left border
                    for (yDestin = halfDim; yDestin < height; yDestin++)
                    {
                        for (xDestin = 0; xDestin < halfDim; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {

                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;

                                    if (xOrigin < 0)
                                        xOrigin = 0;


                                    if (yOrigin >= height)
                                        yOrigin = yDestin;

                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0];
                                    sumG += matrix[matrixRow, matrixCol] * dataPtrAux[1];
                                    sumR += matrix[matrixRow, matrixCol] * dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);

                            dataPtr += nChan;
                        }

                        // Advance the pointer to the next line
                        dataPtr += (width - halfDim) * nChan + padding;
                    }

                    dataPtr -= widthStep * halfDim;
                    dataPtr += nChan * halfDim;

                    // Treat bottom border
                    for (yDestin = height - halfDim; yDestin < height; yDestin++)
                    {
                        for (xDestin = halfDim; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {

                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;


                                    if (xOrigin >= width)
                                        xOrigin = xDestin;

                                    if (yOrigin >= height)
                                        yOrigin = height - 1;


                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0];
                                    sumG += matrix[matrixRow, matrixCol] * dataPtrAux[1];
                                    sumR += matrix[matrixRow, matrixCol] * dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);

                            dataPtr += nChan;
                        }
                        dataPtr += padding + halfDim * nChan;
                    }

                    dataPtr = (byte*)m.ImageData.ToPointer() + widthStep * halfDim + (width - halfDim) * nChan;

                    // Treat right border
                    for (yDestin = halfDim; yDestin < height - halfDim; yDestin++)
                    {
                        for (xDestin = width - halfDim; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {

                                    xOrigin = xDestin + j;
                                    yOrigin = yDestin + i;

                                    if (xOrigin >= width)
                                        xOrigin = width - 1;


                                    byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xOrigin * nChan;
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0];
                                    sumG += matrix[matrixRow, matrixCol] * dataPtrAux[1];
                                    sumR += matrix[matrixRow, matrixCol] * dataPtrAux[2];
                                }
                            }

                            dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);

                            dataPtr += nChan;
                        }
                        dataPtr += padding + (width - halfDim) * nChan;
                    }

                    // Put the pointer in the first pixel of the first line of the core
                    dataPtr = (byte*)m.ImageData.ToPointer() + widthStep * halfDim + halfDim * nChan;

                    // Treat core
                    for (yDestin = halfDim; yDestin < height - halfDim; yDestin++)
                    {
                        byte* rowDataPtr = dataPtrCopy + (yDestin - halfDim) * widthStep;
                        for (xDestin = halfDim; xDestin < width - halfDim; xDestin++)
                        {

                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = rowDataPtr + (xDestin - halfDim) * nChan;

                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0];
                                    sumG += matrix[matrixRow, matrixCol] * dataPtrAux[1];
                                    sumR += matrix[matrixRow, matrixCol] * dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }
                                dataPtrAux += filterLineAdvance;
                            }

                            dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);

                            dataPtr += nChan;

                        }
                        dataPtr += padding + 2 * halfDim * nChan;
                    }
                }
            }
        }

        private static void NonUniformSeparable(Image<Bgr, byte> img, float[] u, float[] v, float matrixWeight, float offset)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image
                int dim = u.Length;
                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, Byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
                // Pad the copy image using OpenCV
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, halfDim, halfDim, halfDim, halfDim, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image
                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int x, y;
                float sumR, sumG, sumB;
                int intermediateWidthStep = width * nChan; // Intermediate buffer does not have padding
                // Create a buffer for the intermediate results (pointer)
                float[] intermediateBuffer = new float[imgCopyPadded.Width * img.Height * nChan];
                fixed (float* p = intermediateBuffer)
                {
                    float* intermediateBufferPtr = p;
                    if (nChan == 3) // image in RGB
                    {
                        for (y = halfDim; y < height - halfDim; y++)
                        {
                            byte* rowDataPtr = dataPtrCopy + (y - halfDim) * widthStep;

                            for (x = 0; x < width; x++)
                            {
                                sumB = 0;
                                sumG = 0;
                                sumR = 0;
                                byte* dataPtrAux = rowDataPtr + x * nChan;

                                for (int i = minusHalfDim, row = 0; i <= halfDim; i++, row++)
                                {
                                    sumB += u[row] * dataPtrAux[0];
                                    sumG += u[row] * dataPtrAux[1];
                                    sumR += u[row] * dataPtrAux[2];
                                    dataPtrAux += widthStep;
                                }

                                intermediateBufferPtr[0] = sumB;
                                intermediateBufferPtr[1] = sumG;
                                intermediateBufferPtr[2] = sumR;
                                intermediateBufferPtr += nChan;
                            }
                        }

                        for (y = halfDim; y < height - halfDim; y++)
                        {
                            float* rowDataPtr = p + (y - halfDim) * intermediateWidthStep;

                            for (x = halfDim; x < width - halfDim; x++)
                            {
                                sumB = 0;
                                sumG = 0;
                                sumR = 0;

                                float* dataPtrAux = rowDataPtr + (x - halfDim) * nChan;
                                for (int i = minusHalfDim, col = 0; i <= halfDim; i++, col++)
                                {
                                    sumB += v[col] * dataPtrAux[0];
                                    sumG += v[col] * dataPtrAux[1];
                                    sumR += v[col] * dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }
                                dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                                dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                                dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);
                                dataPtr += nChan;
                            }
                            dataPtr += padding;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies a 3x3 Sobel filter to an image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the original image

                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, byte>(img.Width + 2, img.Height + 2);
                // Pad the copy image using OpenCV
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, 1, 1, 1, 1, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the padded image

                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // Number of channels - 3 for RGB
                int padding = m.WidthStep - m.NChannels * m.Width; // Alignment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int filterLineAdvance = widthStep - 2 * nChan; // Step for moving one line down during filtering
                int x, y;
                int verticalSumR, verticalSumG, verticalSumB;
                int horizontalSumR, horizontalSumG, horizontalSumB;

                if (nChan == 3) // Image is in RGB
                {
                    for (y = 1; y < height - 1; y++)
                    {
                        byte* rowDataPtr = dataPtrCopy + (y - 1) * widthStep;

                        for (x = 1; x < width - 1; x++)
                        {
                            // Precompute the pointers to the neighboring pixels
                            byte* pA = rowDataPtr + (x - 1) * nChan;  // a
                            byte* pB = pA + nChan;                    // b
                            byte* pC = pB + nChan;                    // c
                            byte* pD = pA + widthStep;                // d
                            byte* pF = pC + widthStep;                // f
                            byte* pG = pD + widthStep;                // g
                            byte* pH = pG + nChan;                    // h
                            byte* pI = pH + nChan;                    // i

                            // Vertical Sobel filter: -1a - 2d - 1g + 1c + 2f + 1i
                            // Horizontal Sobel filter: -1a - 2b - 1c + 1g + 2h + 1i

                            // Start computing vertical and horizontal gradients
                            verticalSumB = horizontalSumB = -pA[0];  // -1a
                            verticalSumG = horizontalSumG = -pA[1];
                            verticalSumR = horizontalSumR = -pA[2];

                            horizontalSumB += -2 * pB[0];             // -2b
                            horizontalSumG += -2 * pB[1];
                            horizontalSumR += -2 * pB[2];

                            horizontalSumB += -pC[0];                 // -1c
                            horizontalSumG += -pC[1];
                            horizontalSumR += -pC[2];

                            verticalSumB += pC[0];                    // +1c
                            verticalSumG += pC[1];
                            verticalSumR += pC[2];

                            verticalSumB += -2 * pD[0];               // -2d
                            verticalSumG += -2 * pD[1];
                            verticalSumR += -2 * pD[2];

                            verticalSumB += 2 * pF[0];                // +2f
                            verticalSumG += 2 * pF[1];
                            verticalSumR += 2 * pF[2];

                            horizontalSumB += pG[0];                  // +1g
                            horizontalSumG += pG[1];
                            horizontalSumR += pG[2];

                            verticalSumB += -pG[0];                   // -1g
                            verticalSumG += -pG[1];
                            verticalSumR += -pG[2];

                            horizontalSumB += 2 * pH[0];              // +2h
                            horizontalSumG += 2 * pH[1];
                            horizontalSumR += 2 * pH[2];

                            horizontalSumB += pI[0];                  // +1i
                            horizontalSumG += pI[1];
                            horizontalSumR += pI[2];

                            verticalSumB += pI[0];                    // +1i
                            verticalSumG += pI[1];
                            verticalSumR += pI[2];

                            // Compute the final Sobel magnitude for each channel and clamp to [0, 255]
                            dataPtr[0] = (byte)Clamp(Math.Abs(horizontalSumB) + Math.Abs(verticalSumB), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Abs(horizontalSumG) + Math.Abs(verticalSumG), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Abs(horizontalSumR) + Math.Abs(verticalSumR), 0, 255);

                            // Move to the next pixel in the row
                            dataPtr += nChan;
                        }

                        // Skip any padding between rows in the original image
                        dataPtr += padding;
                    }
                }
            }
        }



        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int x, y;

                int greenChannelW = widthStep + 1;
                int redChannelW = widthStep + 2;
                int greenChannelC = nChan + 1;
                int redChannelC = nChan + 2;

                if (nChan == 3)
                {
                    // Treat right border
                    dataPtr += widthStep - nChan - padding;
                    dataPtrCopy += widthStep - nChan - padding;             
                    for (y = 0; y < height - 1; y++)
                    {
                        dataPtr[0] = (byte)Math.Abs(dataPtr[0] - dataPtrCopy[widthStep]);
                        dataPtr[1] = (byte)Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelW]);
                        dataPtr[2] = (byte)Math.Abs(dataPtr[2] - dataPtrCopy[redChannelW]);

                        dataPtr += widthStep;
                        dataPtrCopy += widthStep;
                    }

                    // Treat bottom border
                    dataPtr = dataPtr - widthStep + nChan + padding;
                    dataPtrCopy = dataPtrCopy - widthStep + nChan + padding;                  
                    for (x = 0; x < width - 1; x++)
                    {
                        dataPtr[0] = (byte)Math.Abs(dataPtr[0] - dataPtrCopy[nChan]);
                        dataPtr[1] = (byte)Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelC]);
                        dataPtr[2] = (byte)Math.Abs(dataPtr[2] - dataPtrCopy[redChannelC]);

                        dataPtr += nChan;
                        dataPtrCopy += nChan;
                    }

                    // Last pixel is 0
                    dataPtr[0] = 0;
                    dataPtr[1] = 0;
                    dataPtr[2] = 0;


                    // Treat core
                    dataPtr = (byte*)m.ImageData.ToPointer();
                    dataPtrCopy = (byte*)mCopy.ImageData.ToPointer();
                    for (y = 0; y < height - 1; y++)
                    {
                        for (x = 0; x < width - 1; x++)
                        {
                            dataPtr[0] = (byte)Clamp(Math.Abs(dataPtr[0] - dataPtrCopy[nChan]) + Math.Abs(dataPtr[0] - dataPtrCopy[widthStep]), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelC]) + Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelW]), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Abs(dataPtr[2] - dataPtrCopy[redChannelC]) + Math.Abs(dataPtr[2] - dataPtrCopy[redChannelW]), 0, 255);

                            dataPtr += nChan;
                            dataPtrCopy += nChan;
                        }
                        // Skip the last pixel
                        dataPtr += nChan + padding;
                        dataPtrCopy += nChan + padding;
                    }
                }
            }
        }

        /// <summary>
        /// Filter that uses the Roberts operator to detect edges.
        /// Note: For some reason this only works correctly when I process the core first and then the borders.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int x, y;

                int greenChannelW = widthStep + 1;
                int redChannelW = widthStep + 2;
                int greenChannelC = nChan + 1;
                int redChannelC = nChan + 2;
                int blueChannelWC = widthStep + nChan;
                int greenChannelWC = widthStep + nChan + 1;
                int redChannelWC = widthStep + nChan + 2;

                if (nChan == 3) // Ensure it's a color image
                {
                    // Treat core         
                    for (y = 0; y < height - 1; y++)
                    {
                        for (x = 0; x < width - 1; x++)
                        {
                            dataPtr[0] = (byte)Clamp(Math.Abs(dataPtr[0] - dataPtrCopy[blueChannelWC]) + Math.Abs(dataPtr[nChan] - dataPtrCopy[widthStep]), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelWC]) + Math.Abs(dataPtr[greenChannelC] - dataPtrCopy[greenChannelW]), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Abs(dataPtr[2] - dataPtrCopy[redChannelWC]) + Math.Abs(dataPtr[redChannelC] - dataPtrCopy[redChannelW]), 0, 255);

                            dataPtr += nChan;
                            dataPtrCopy += nChan;
                        }
                        // Skip the last pixel
                        dataPtr += nChan + padding;
                        dataPtrCopy += nChan + padding;
                    }

                    dataPtr = (byte*)m.ImageData.ToPointer();
                    dataPtrCopy = (byte*)mCopy.ImageData.ToPointer();

                    // Treat right border
                    dataPtr += widthStep - nChan - padding;
                    dataPtrCopy += widthStep - nChan - padding;
                    for (y = 0; y < height - 1; y++)
                    {
                        /*
                         * Since the pixels outside the image are replicated, the diagonal:
                         *  1  0
                         *  0 -1
                         * will be equivalent to the following:
                         *  1  0
                         * -1  0
                         * and the diagonal
                         *  0  1
                         * -1  0
                         * will be equivalent to
                         *  1  0
                         * -1  0
                         * 
                         * Since the "diagonals" are the same, we just need to calculate one of them and multiply by 2
                         */
                        dataPtr[0] = (byte)Clamp(Math.Abs(dataPtr[0] - dataPtrCopy[widthStep]) * 2, 0, 255);
                        dataPtr[1] = (byte)Clamp(Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelW]) * 2, 0, 255);
                        dataPtr[2] = (byte)Clamp(Math.Abs(dataPtr[2] - dataPtrCopy[redChannelW]) * 2, 0, 255);

                        dataPtr += widthStep;
                        dataPtrCopy += widthStep;
                    }

                    // Treat bottom border
                    dataPtr = dataPtr - widthStep + nChan + padding;
                    dataPtrCopy = dataPtrCopy - widthStep + nChan + padding;
                    for (x = 0; x < width - 1; x++)
                    {
                        /*
                         * Since the pixels outside the image are replicated, the diagonal:
                         *  1  0
                         *  0 -1
                         * will be equivalent to the following:
                         *  1 -1
                         *  0  0
                         * and the diagonal
                         *  0  1
                         * -1  0
                         * will be equivalent to
                         * -1  1
                         *  0  0
                         * 
                         * Since the "diagonals" have the same absolute value, we just need to calculate one of them and multiply by 2
                         */
                        dataPtr[0] = (byte)Clamp(Math.Abs(dataPtr[0] - dataPtrCopy[nChan]) * 2, 0, 255);
                        dataPtr[1] = (byte)Clamp(Math.Abs(dataPtr[1] - dataPtrCopy[greenChannelC]) * 2, 0, 255);
                        dataPtr[2] = (byte)Clamp(Math.Abs(dataPtr[2] - dataPtrCopy[redChannelC]) * 2, 0, 255);

                        dataPtr += nChan;
                        dataPtrCopy += nChan;
                    }

                    // Last pixel is 0
                    dataPtr[0] = 0;
                    dataPtr[1] = 0;
                    dataPtr[2] = 0;

                    
                }
            }
        }

        public static void MedianBlur(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int kernelSize)
        {
            CvInvoke.MedianBlur(imgCopy, img, kernelSize);
        }

        /// <summary>
        /// Eval function
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            MedianBlur(img, imgCopy, 3);
        }

        
        public static void MedianBlur3D(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;

                // Pad the copy image using OpenCV
                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, halfDim, halfDim, halfDim, halfDim, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the padded image

                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // number of channels - 3 (RGB)
                int padding = m.WidthStep - m.NChannels * m.Width; // alignment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int filterLineAdvance = widthStep - dim * nChan;

                if (nChan == 3) // image in RGB
                {
                    for (int y = halfDim; y < height - halfDim; y++)
                    {
                        for (int x = halfDim; x < width - halfDim; x++)
                        {
                            double minDistance = double.MaxValue;
                            ((int, int), double) bestDistance = ((0, 0), 0);

                            // Iterate over the kernel, comparing the pixel with every other pixel
                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    int currentY = y + i;
                                    int currentX = x + j;
                                    double totalDistance = 0;

                                    byte* dataPtrAux = dataPtrCopy + currentY * widthStep + currentX * nChan;

                                    // Compare this pixel with every other pixel in the kernel
                                    for (int k = minusHalfDim; k <= halfDim; k++)
                                    {
                                        for (int l = minusHalfDim; l <= halfDim; l++)
                                        {
                                            if (i == k && j == l)
                                                continue; // skip self-comparison

                                            byte* comparisonPtr = dataPtrCopy + (y + k) * widthStep + (x + l) * nChan;
                                            
                                            totalDistance += Math.Abs(dataPtrAux[0] - comparisonPtr[0])
                                                           + Math.Abs(dataPtrAux[1] - comparisonPtr[1])
                                                           + Math.Abs(dataPtrAux[2] - comparisonPtr[2]);
                                        }
                                    }
                                    // 
                                    // Check if the current pixel has the minimum sum of distances
                                    if (totalDistance < minDistance)
                                    {
                                        minDistance = totalDistance;
                                        bestDistance = ((currentY, currentX), minDistance);
                                    }
                                }
                            }

                            // Set the output pixel to the color of the pixel with the minimum distance
                            byte* bestPixelPtr = dataPtrCopy + bestDistance.Item1.Item1 * widthStep + bestDistance.Item1.Item2 * nChan;
                            dataPtr[0] = bestPixelPtr[0];
                            dataPtr[1] = bestPixelPtr[1];
                            dataPtr[2] = bestPixelPtr[2];

                            dataPtr += nChan; // move to the next pixel
                        }
                        dataPtr += padding; // move to the next row
                    }
                }
            }
        }


        /// <summary>
        /// Eval function
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        public static void Median3D(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            MedianBlur3D(img, imgCopy, 3);
        }


        public static int[] Histogram_Gray(Image<Bgr, byte> img)
        {
            int[] histogram = new int[256];
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            histogram[(int)Math.Round((dataPtr[0] + dataPtr[1] + dataPtr[2]) / 3.0)]++;
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
            return histogram;
        }

        public static int[,] Histogram_RGB(Image<Bgr, byte> img)
        {
            int[,] histogram = new int[3, 256];
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            histogram[0, dataPtr[0]]++; // Blue
                            histogram[1, dataPtr[1]]++; // Green
                            histogram[2, dataPtr[2]]++; // Red
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
            return histogram;
        }

        public static int[,] Histogram_All(Image<Bgr, byte> img)
        {
            int[,] histogram = new int[4, 256];
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.NChannels;
                int padding = m.WidthStep - m.NChannels * m.Width;
                int widthStep = m.WidthStep;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            histogram[0, (int)Math.Round((dataPtr[0] + dataPtr[1] + dataPtr[2]) / 3.0)]++; // Gray
                            histogram[1, dataPtr[0]]++; // Blue
                            histogram[2, dataPtr[1]]++; // Green
                            histogram[3, dataPtr[2]]++; // Red
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
            return histogram;
        }


        private static bool IsMatrixSeparable(float[,] matrix, out float[] u, out float[] v)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            u = new float[rows];
            v = new float[cols];

            // Extract the first column as u vector
            for (int i = 0; i < rows; i++)
            {
                u[i] = matrix[i, 0];
            }

            // Check if we can divide all columns by u to find a constant v
            for (int j = 0; j < cols; j++)
            {
                if (u[0] != 0)
                {
                    v[j] = matrix[0, j] / u[0];
                }
                else
                {
                    v = null;
                    return false;
                }
            }

            // Verify if the outer product of u and v matches the original matrix
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (Math.Abs(matrix[i, j] - u[i] * v[j]) > 1e-9) // Tolerance for floating point comparison
                    {
                        v = null;
                        return false;
                    }
                }
            }

            return true;
        }

        private static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        public static void MeanSolutionAPadding(Image<Bgr, byte> img, int dim)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                float count = dim * dim;

                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, Byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
                // Pad the copy image using OpenCV
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, halfDim, halfDim, halfDim, halfDim, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int filterLineAdvance = widthStep - dim * nChan;
                int x, y;
                int sumR, sumG, sumB;

                if (nChan == 3) // image in RGB
                {
                    for (y = halfDim; y < height - halfDim; y++)
                    {
                        for (x = halfDim; x < width - halfDim; x++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (y - halfDim) * widthStep + (x - halfDim) * nChan;

                            for (int i = minusHalfDim; i <= halfDim; i++)
                            {
                                for (int j = minusHalfDim; j <= halfDim; j++)
                                {
                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }

                                dataPtrAux += filterLineAdvance;
                            }

                            dataPtr[0] = (byte)Math.Round(sumB / count);
                            dataPtr[1] = (byte)Math.Round(sumG / count);
                            dataPtr[2] = (byte)Math.Round(sumR / count);

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        private static void NonUniformPadding(Image<Bgr, byte> img, float[,] matrix, float matrixWeight, float offset)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.ImageData.ToPointer(); // Pointer to the image

                int dim = matrix.GetLength(0);
                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                float count = dim * dim;

                Image<Bgr, byte> imgCopyPadded = new Image<Bgr, Byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
                // Pad the copy image using OpenCV
                CvInvoke.CopyMakeBorder(img, imgCopyPadded, halfDim, halfDim, halfDim, halfDim, Emgu.CV.CvEnum.BorderType.Replicate);
                MIplImage mCopy = imgCopyPadded.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.ImageData.ToPointer(); // Pointer to the image

                int width = imgCopyPadded.Width;
                int height = imgCopyPadded.Height;
                int nChan = m.NChannels; // number of channels - 3
                int padding = m.WidthStep - m.NChannels * m.Width; // alingnment bytes (padding)
                int widthStep = mCopy.WidthStep;
                int x, y;
                float sumR, sumG, sumB;
                int filterLineAdvance = widthStep - dim * nChan;

                if (nChan == 3) // image in RGB
                {
                    for (y = halfDim; y < height - halfDim; y++)
                    {
                        for (x = halfDim; x < width - halfDim; x++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (y - halfDim) * widthStep + (x - halfDim) * nChan;

                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0];
                                    sumG += matrix[matrixRow, matrixCol] * dataPtrAux[1];
                                    sumR += matrix[matrixRow, matrixCol] * dataPtrAux[2];
                                    dataPtrAux += nChan;
                                }
                                dataPtrAux += filterLineAdvance;
                            }

                            dataPtr[0] = (byte)Clamp(Math.Round(sumB / matrixWeight + offset), 0, 255);
                            dataPtr[1] = (byte)Clamp(Math.Round(sumG / matrixWeight + offset), 0, 255);
                            dataPtr[2] = (byte)Clamp(Math.Round(sumR / matrixWeight + offset), 0, 255);

                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }
    }

    class DistanceComparer : IComparer<((int, int), double)>
    {
        // The best distance is the one with the smallest value
        public int Compare(((int, int), double) x, ((int, int), double) y)
        {
            return x.Item2.CompareTo(y.Item2);
        }
    }
}
