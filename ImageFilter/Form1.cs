using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageFilter
{
    public partial class Form1 : Form
    {
        ImgManipulation imgManager = new ImgManipulation();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var filePath = dlg.FileName;
                Bitmap imgbmp = imgManager.ConvertToBitmap(filePath);
                pictureBox1.Image = imgManager.ConvertToGrayScale(imgbmp);
            } 
        }
    }
}
