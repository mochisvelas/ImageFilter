using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageFilter
{
    public partial class Form1 : Form
    {
        ImgManipulation imgManager = new ImgManipulation();
        string filePath = string.Empty;
        Bitmap img;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Difuminar");
            comboBox1.Items.Add("Realzar");
            comboBox1.Items.Add("Sobel inferior");
            comboBox1.Items.Add("Sobel superior");
            comboBox1.Items.Add("Sobel izquierdo");
            comboBox1.Items.Add("Soberl derecho");
            comboBox1.Items.Add("Contorno");
            comboBox1.Items.Add("Afilar");
            comboBox1.Items.Add("Original");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName;
                img = imgManager.ConvertToBitmap(filePath);
                pictureBox2.Image = img;
                pictureBox1.Image = imgManager.ConvertToGrayScale(img);                
            } 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox3.Image = imgManager.ConvertToKernel(imgManager.ConvertToGrayScale(img), 1);
        }
    }
}
