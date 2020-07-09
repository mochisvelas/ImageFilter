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
            Bitmap original = bmp;
            Bitmap clone = new Bitmap(original);
            int x, y;
            for (x = 0; x < bmp.Width; x++)
            {
                for (y = 0; y < bmp.Height; y++)
                {
                    Color originalColor = bmp.GetPixel(x, y);
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    Color newColor = Color.FromArgb(originalColor.A, grayScale, grayScale, grayScale);
                    clone.SetPixel(x, y, newColor);
                }
            }
            return clone;
        }

        public Bitmap ConvertToKernel(Bitmap bmp, string sel, double[,] persKernel)
        {
            double[,] kernel= { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
            if (string.IsNullOrEmpty(sel))
            {
                kernel = persKernel;
            }
            else
            {
                switch (sel)
                {
                    case "Difuminar":
                        kernel = new double[3, 3] { { 0.0625, 0.125, 0.0625 }, { 0.125, 0.25, 0.125 }, { 0.0625, 0.125, 0.0625 } };
                        break;
                    case "Realzar":
                        kernel = new double[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                        break;
                    case "Sobel inferior":
                        kernel = new double[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
                        break;
                    case "Sobel superior":
                        kernel = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
                        break;
                    case "Sobel izquierdo":
                        kernel = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
                        break;
                    case "Soberl derecho":
                        kernel = new double[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                        break;
                    case "Contorno":
                        kernel = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
                        break;
                    case "Afilar":
                        kernel = new double[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                        break;
                    case "Original":
                        kernel = new double[3, 3] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                        break;
                    default:
                        break;
                }
            }
            
            Bitmap original = bmp;
            Bitmap clone = new Bitmap(original);            
            double sum = 0;
            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < kernel.GetLength(1); j++)
                {
                    sum += kernel[i, j];
                }
            }
            if (sum == 0)
            {
                sum = 1;
            }
            double val;
            for (int i = 0; i < original.Width - 2; i++)
            {
                for (int j = 0; j < original.Height - 2; j++)
                {                    
                    val = (kernel[0, 0] * original.GetPixel(i, j).R) + (kernel[0, 1] * original.GetPixel(i, j + 1).R) +
                          (kernel[0, 2] * original.GetPixel(i, j + 2).R) + (kernel[1, 0] * original.GetPixel(i + 1, j).R) +
                          (kernel[1, 1] * original.GetPixel(i + 1, j + 1).R) + (kernel[1, 2] * original.GetPixel(i + 1, j + 2).R) +
                          (kernel[2, 0] * original.GetPixel(i + 2, j).R) + (kernel[2, 1] * original.GetPixel(i + 2, j + 1).R) +
                          (kernel[2, 2] * original.GetPixel(i + 2, j + 2).R);

                    val /= sum;
                    if (val < 0)
                    {
                        val = 0;
                    }
                    else if (val > 255)
                    {
                        val = 255;
                    }
                    Color newColor = Color.FromArgb((int)val, (int)val, (int)val);
                    clone.SetPixel(i+1, j+1, newColor);
                    if (i == 0 && j == original.Height - 2)
                    {
                        clone.SetPixel(i, j + 1, newColor);
                        clone.SetPixel(i, j + 2, newColor);
                        clone.SetPixel(i + 1, j + 2, newColor);
                        clone.SetPixel(i, j, newColor);
                    }
                    else if (j == 0 && i == original.Width - 2)
                    {
                        clone.SetPixel(i + 1, j, newColor);
                        clone.SetPixel(i + 2, j, newColor);
                        clone.SetPixel(i + 2, j + 1, newColor);
                        clone.SetPixel(i, j, newColor);
                    }
                    else if (i == original.Width - 2 && j == original.Height - 2)
                    {
                        clone.SetPixel(i + 2, j + 1, newColor);
                        clone.SetPixel(i + 1, j + 2, newColor);
                        clone.SetPixel(i + 2, j + 2, newColor);
                    }
                    else if (j == original.Height - 2)
                    {
                        clone.SetPixel(i + 1, j + 2, newColor);
                    }
                    else if (i == original.Width - 2)
                    {
                        clone.SetPixel(i + 2, j + 1, newColor);
                    }
                    else if (j == 0)
                    {
                        clone.SetPixel(i, j, newColor);
                    }
                    else if (i == 0)
                    {
                        clone.SetPixel(i, j, newColor);
                    }
                }
            }
            return clone;
        }        
    }
}
