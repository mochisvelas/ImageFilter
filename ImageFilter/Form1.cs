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
        Bitmap gray;
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
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                gray = imgManager.ConvertToGrayScale(img);
                pictureBox1.Image = gray;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = null;
            } 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sel = comboBox1.Text;
            if (string.IsNullOrEmpty(sel) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Elementos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                pictureBox3.Image = imgManager.ConvertToKernel(gray, sel, null);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Elementos incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                double[,] kernel = { { (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value },
                { (double)numericUpDown6.Value, (double)numericUpDown5.Value, (double)numericUpDown4.Value },
                { (double)numericUpDown9.Value, (double)numericUpDown8.Value, (double)numericUpDown7.Value } };
                pictureBox3.Image = imgManager.ConvertToKernel(gray, "", kernel);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
