using System.Drawing;
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
            switch (sel)
            {
                case 1:
                    kernel = new double[3, 3] { { 0.0625, 0.125, 0.0625 }, { 0.125, 0.25, 0.125 }, { 0.0625, 0.125, 0.0625 } };
                    break;
                case 2:
                    kernel = new double[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                    break;
                case 3:
                    kernel = new double[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
                    break;
                case 4:
                    kernel = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
                    break;
                case 5:
                    kernel = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
                    break;
                case 6:
                    kernel = new double[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                    break;
                case 7:
                    kernel = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
                    break;
                case 8:
                    kernel = new double[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                    break;
                case 9:
                    kernel = new double[3, 3] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                    break;
                case 10:
                    kernel = new double[3, 3] { { 0, 0, 0 }, { 0, 2, 0 }, { 0, 0, 0 } };
                    break;
                default:
                    break;
            }             
            Bitmap imgbmp = bmp;
            return imgbmp;
        }
    }
}
