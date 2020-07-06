using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageFilter
{
    class ImgManipulation
    {
        public Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = File.Open(fileName, FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            }
            return bitmap;
        }

        public Bitmap ConvertToGrayScale(Bitmap bmp)
        {
            Bitmap imgbmp = bmp;
            int x, y;
            for (x = 0; x < bmp.Width; x++)
            {
                for (y = 0; y < bmp.Height; y++)
                {
                    Color oc = bmp.GetPixel(x, y);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color newColor = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    imgbmp.SetPixel(x, y, newColor);
                }
            }
            return imgbmp;
        }

        public Bitmap ConvertToKernel(Bitmap bmp, int sel)
        {
            double[,] kernel;
            Bitmap original = bmp;
            Bitmap clone = new Bitmap(original);
            DirectConvolution(original, clone);
            //switch (sel)
            //{
            //    case 1:
            //        kernel = new double[3, 3] { { 0.0625, 0.125, 0.0625 }, { 0.125, 0.25, 0.125 }, { 0.0625, 0.125, 0.0625 } };
            //        break;
            //    case 2:
            //        kernel = new double[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            //        break;
            //    case 3:
            //        kernel = new double[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            //        break;
            //    case 4:
            //        kernel = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            //        break;
            //    case 5:
            //        kernel = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            //        break;
            //    case 6:
            //        kernel = new double[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            //        break;
            //    case 7:
            //        kernel = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
            //        break;
            //    case 8:
            //        kernel = new double[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            //        break;
            //    case 9:
            //        kernel = new double[3, 3] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
            //        break;
            //    case 10:
            //        kernel = new double[3, 3] { { 0, 0, 0 }, { 0, 2, 0 }, { 0, 0, 0 } };
            //        break;
            //    default:
            //        break;
            //}
            //BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            //ImageLockMode.ReadOnly,
            //PixelFormat.Format32bppRgb);
            ////Bitmap original = bmp;
            ////Bitmap clone = new Bitmap(original);
            return clone;
        }

        public Bitmap DirectConvolution(Bitmap original, Bitmap clone)
        {
            double[,] kernelx = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            double val = 0;
            double sum = 0;
            for (int i = 0; i < kernelx.GetLength(0); i++)
            {
                for (int j = 0; j < kernelx.GetLength(1); j++)
                {
                    sum += kernelx[i, j];
                }
            }
            if (sum == 0)
            {
                sum = 1;
            }
            for (int i = 0; i < original.Width - 2; i++)
            {
                for (int j = 0; j < original.Height - 2; j++)
                {
                    var pixel = original.GetPixel(i, j);
                    var gg = pixel.R;
                    val = (kernelx[0, 0] * original.GetPixel(i, j).R) + (kernelx[0, 1] * original.GetPixel(i, j + 1).R) +
                          (kernelx[0, 2] * original.GetPixel(i, j + 2).R) + (kernelx[1, 0] * original.GetPixel(i + 1, j).R) +
                          (kernelx[1, 1] * original.GetPixel(i + 1, j + 1).R) + (kernelx[1, 2] * original.GetPixel(i + 1, j + 2).R) +
                          (kernelx[2, 0] * original.GetPixel(i + 2, j).R) + (kernelx[2, 1] * original.GetPixel(i + 2, j + 1).R) +
                          (kernelx[2, 2] * original.GetPixel(i + 2, j + 2).R);

                    val = val / sum;
                    if (val < 0)
                    {
                        val = 0;
                    }
                    else if (val > 255)
                    {
                        val = 255;
                    }
                    Color newColor = Color.FromArgb((int)val, (int)val, (int)val);
                    clone.SetPixel(i, j, newColor);
                }
                //File outputfile = new File("src/image/edge.png");
                //ImageIO.write(temp, "png", outputfile);
            }
            return clone;
        }
    }
}
