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
    }
}
