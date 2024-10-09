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
                byte blue, green, red, gray;

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
                            //retrieve 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

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
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            dataPtr[0] = (byte)Math.Round(dataPtr[0] * contrast + bright);
                            dataPtr[1] = (byte)Math.Round(dataPtr[1] * contrast + bright);
                            dataPtr[2] = (byte)Math.Round(dataPtr[2] * contrast + bright);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
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


                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {

                            xOrigin = xDestin - dx;
                            yOrigin = yDestin - dy;

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

                        //at the end of the line advance the pointer by the alignment bytes (padding)
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
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {

                            xOrigin = (int)Math.Round((xDestin - halfWidth) * cos - (halfHeight - yDestin) * sin + halfWidth);
                            yOrigin = (int)Math.Round((halfHeight - (xDestin - halfWidth) * sin - (halfHeight - yDestin) * cos));

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

                        //at the end of the line advance the pointer by the alignment bytes (padding)
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
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {

                            xOrigin = (int)Math.Round(xDestin * inverseScaleFactor);
                            yOrigin = (int)Math.Round(yDestin * inverseScaleFactor);

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


                float xOffset = centerX - (width-1) / 2.0f / scaleFactor;
                float yOffset = centerY - (height-1) / 2.0f / scaleFactor;

                float inverseScaleFactor = 1 / scaleFactor;


                if (nChan == 3) // image in RGB
                {
                    for (yDestin = 0; yDestin < height; yDestin++)
                    {
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {

                            xOrigin = (int)Math.Round(xDestin * inverseScaleFactor + xOffset);
                            yOrigin = (int)Math.Round(yDestin * inverseScaleFactor + yOffset);


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

                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


        /// <summary>
        /// Applies a mean filter to an image with special treatment of the borders
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        /// <param name="dim"></param>
        public static void MeanSolutionA(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dim)
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
                    for (yDestin = halfDim; yDestin < height-halfDim; yDestin++)
                    {
                        for (xDestin = width-halfDim; xDestin < width; xDestin++)
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
                        for (xDestin = halfDim; xDestin < width - halfDim; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (yDestin-halfDim) * widthStep + (xDestin-halfDim) * nChan;

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

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            MeanSolutionA(img, imgCopy, 15);
            //MeanSolutionAPadding(img, 3);
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

                Image<Bgr, byte> imgCopyPadded = new Image<Bgr,Byte>(img.Width + halfDim * 2, img.Height + halfDim * 2);
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
                NonUniformPadding(img, matrix, matrixWeight, offset);
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
                float  sumR, sumG, sumB;
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

                int copyWidthStep = width * nChan;

                // Create a buffer for the intermediate results (pointer)
                float[] intermediateBuffer = new float[imgCopyPadded.Width * img.Height * nChan];

                fixed (float* p = intermediateBuffer)
                {
                    float* intermediateBufferPtr = p;

                    if (nChan == 3) // image in RGB
                    {
                        for (y = halfDim; y < height - halfDim; y++)
                        {
                            for (x = 0; x < width; x++)
                            {
                                sumB = 0;
                                sumG = 0;
                                sumR = 0;                           
                                byte* dataPtrAux = dataPtrCopy + (y - halfDim) * widthStep + x * nChan;

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

                        for (y = 0; y < height; y++)
                        {
                            for (x = halfDim; x < width - halfDim; x++)
                            {
                                sumB = 0;
                                sumG = 0;
                                sumR = 0;
                                float* dataPtrAux = p + y * copyWidthStep + (x - halfDim) * nChan;

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


        /*
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
                        for (xDestin = halfDim; xDestin < width - halfDim; xDestin++)
                        {

                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (yDestin-halfDim) * widthStep + (xDestin - halfDim) * nChan;

                            for (int i = minusHalfDim, matrixRow = 0; i <= halfDim; i++, matrixRow++)
                            {
                                for (int j = minusHalfDim, matrixCol = 0; j <= halfDim; j++, matrixCol++)
                                {                     
                                    sumB += matrix[matrixRow, matrixCol] * dataPtrAux[0] ;
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
        }*/

        /*private static void NonUniformSeparable(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[] u, float[] v, float matrixWeight, float offset)
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

                // Temporary buffer to store intermediate results
                (float, float, float)[] intermediateBuffer = new (float, float, float)[width * height * nChan];
                int index = 0;    

                int dim = u.Length; // or v.Length
                int halfDim = dim / 2;
                int minusHalfDim = -halfDim;
                int filterLineAdvance = widthStep - dim * nChan;
                
                if (nChan == 3) // image in RGB
                {
                    //-----------------------------------------------------------FIRST PASS (Vertical)-----------------------------------------------------------
                    // Treat top border
                    for (yDestin = 0; yDestin < halfDim; yDestin++)
                    {
                        for (xDestin = 0; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim, row = 0; i <= halfDim; i++, row++)
                            {
                                yOrigin = yDestin + i;

                                if (yOrigin < 0)
                                    yOrigin = 0;

                                byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xDestin * nChan;
                                sumB += u[row] * dataPtrAux[0];
                                sumG += u[row] * dataPtrAux[1];
                                sumR += u[row] * dataPtrAux[2];
                            }
                            intermediateBuffer[index++] = (sumB, sumG, sumR);
                        }
                    }

                    // Treat left border
                    for (yDestin = halfDim; yDestin < height; yDestin++)
                    {
                        for (xDestin = 0; xDestin < halfDim; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            for (int i = minusHalfDim, row = 0; i <= halfDim; i++, row++)
                            {
                                yOrigin = yDestin + i;

                                if (yOrigin >= height)
                                    yOrigin = yDestin;

                                byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xDestin * nChan;
                                sumB += u[row] * dataPtrAux[0];
                                sumG += u[row] * dataPtrAux[1];
                                sumR += u[row] * dataPtrAux[2];
                            }

                            intermediateBuffer[index++] = (sumB, sumG, sumR);
                        }
                    }                    

                    // Treat bottom border
                    for (yDestin = height - halfDim; yDestin < height; yDestin++)
                    {
                        for (xDestin = halfDim; xDestin < width; xDestin++)
                        {
                            sumB = 0;
                            sumG = 0;
                            sumR = 0;


                            for (int i = minusHalfDim, row = 0; i <= halfDim; i++, row++)
                            {
                                yOrigin = height - halfDim + i;

                                if (yOrigin >= height)
                                    yOrigin = height - 1;

                                byte* dataPtrAux = dataPtrCopy + yOrigin * widthStep + xDestin * nChan;
                                sumB += u[row] * dataPtrAux[0];
                                sumG += u[row] * dataPtrAux[1];
                                sumR += u[row] * dataPtrAux[2];
                            }

                            intermediateBuffer[index++] = (sumB, sumG, sumR);
                        }
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

                                    xOrigin = width - halfDim + j;
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
                        for (xDestin = halfDim; xDestin < width - halfDim; xDestin++)
                        {

                            sumB = 0;
                            sumG = 0;
                            sumR = 0;
                            byte* dataPtrAux = dataPtrCopy + (yDestin - halfDim) * widthStep + (xDestin - halfDim) * nChan;

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
        }*/
    }
}
