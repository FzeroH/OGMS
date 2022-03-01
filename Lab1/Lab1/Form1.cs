using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private Image<Bgr, byte> inputImage = null;
        private Mat outputImage = new Mat();

        public Form1()
        {
            InitializeComponent();
        }
        private void cvColor()
        {
            CvInvoke.CvtColor(inputImage, outputImage, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            pictureBox2.Image = outputImage.Bitmap;
        }
        private void cvGaussianBlur()
        {
            CvInvoke.GaussianBlur(inputImage, outputImage, new Size(), 8.5, 20);
            pictureBox2.Image = outputImage.Bitmap;
        }

        private void negative()
        {
            Bitmap value = (Bitmap)Invert.invert(inputImage.Bitmap);
            Image<Bgr, byte> img = new Image<Bgr, byte>(value);
            outputImage = img.Mat;
            pictureBox2.Image = outputImage.Bitmap;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();

                if(res == DialogResult.OK)
                {
                    inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                    pictureBox1.Image = inputImage.Bitmap;
                }
                else
                {
                    MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                Thread th = new Thread(new ThreadStart(cvGaussianBlur));
                th.Start();
                th.Join();

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                Thread th = new Thread(new ThreadStart(cvColor));
                th.Start();
                th.Join();

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                Thread th = new Thread(new ThreadStart(negative));
                th.Start();
                th.Join();

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void экспортироватьВPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp1 = outputImage.Bitmap;
                bmp1.Save(@"c:\Users\Alex\Desktop\photo.png", ImageFormat.Png);
                MessageBox.Show("Успешно","Успешно",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void экспортироватьВJPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp1 = outputImage.Bitmap;
                bmp1.Save(@"c:\Users\Alex\Desktop\photo.jpg", ImageFormat.Jpeg);
                MessageBox.Show("Успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
