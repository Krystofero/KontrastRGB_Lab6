using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdytorZdjec_v1
{
    public partial class Histogram : Form
    {
        private int szer=0, wys=0;

        public Histogram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                szer = pictureBox1.Image.Width;
                wys = pictureBox1.Image.Height;
                pictureBox2.Image = new Bitmap(szer, wys);

            }
        }

        private void show_Histogram(Bitmap b2)
        {
            int[] red = new int[256];
            int[] green = new int[256];
            int[] blue = new int[256];
            for (int i = 0; i < szer; i++)
            {
                for (int j = 0; j < wys; j++)
                {
                    Color pixel = b2.GetPixel(i, j);
                    red[pixel.R]++;
                    green[pixel.G]++;
                    blue[pixel.B]++;
                }
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            for (int i = 0; i <= 255; i++)
            {
                if (checkBox1.Checked) chart1.Series[0].Points.AddXY(i, red[i]);
                if (checkBox2.Checked) chart1.Series[1].Points.AddXY(i, green[i]);
                if (checkBox3.Checked) chart1.Series[2].Points.AddXY(i, blue[i]);
            }
            chart1.Invalidate();
        }

//Kontrast - wariant nr.1
        private void button15_Click(object sender, EventArgs e)
        {
            int val = (int)(numericUpDown1.Value);
            Bitmap b1 = (Bitmap)pictureBox1.Image;
            Bitmap b2 = (Bitmap)pictureBox2.Image;
            Color k1;
            Color k2;

            int delta_c = val;
            int red_prim;
            int green_prim;
            int blue_prim;

            if (val >= 0)
            {
                for (int i = 0; i < szer; i++)
                {
                    for (int j = 0; j < wys; j++)
                    {
                        k1 = b1.GetPixel(i, j);
                        int red = (int)(k1.R);
                        int green = (int)(k1.G);
                        int blue = (int)(k1.B);

                        red_prim = (int)((127 / (127 - delta_c)) * (red - delta_c));
                        green_prim = (int)((127 / (127 - delta_c)) * (green - delta_c));
                        blue_prim = (int)((127 / (127 - delta_c)) * (blue - delta_c));

                        if (red_prim >= 0 && red_prim <= 255) red = red_prim;
                        else red = red;

                        if (green_prim >= 0 && green_prim <= 255) green = green_prim;
                        else green = green;

                        if (blue_prim >= 0 && blue_prim <= 255) blue = blue_prim;
                        else blue = blue;

                        k2 = Color.FromArgb(red, green, blue);
                        b2.SetPixel(i, j, k2);
                    }
                }
            }

            else if (val < 0) 
            {
                for (int i = 0; i < szer; i++)
                {
                    for (int j = 0; j < wys; j++)
                    {
                        k1 = b1.GetPixel(i, j);
                        int red = (int)(k1.R);
                        int green = (int)(k1.G);
                        int blue = (int)(k1.B);
                        red_prim = (int)(((127 - delta_c) / 127) * (red - delta_c));
                        green_prim = (int)(((127 - delta_c) / 127) * (green - delta_c));
                        blue_prim = (int)(((127 - delta_c) / 127) * (blue - delta_c));

                        if (red_prim >= 0 && red_prim <= 255) red = red_prim;
                        else red = red;

                        if (green_prim >= 0 && green_prim <= 255) green = green_prim;
                        else green = green;

                        if (blue_prim >= 0 && blue_prim <= 255) blue = blue_prim;
                        else blue = blue;

                        k2 = Color.FromArgb(red, green, blue);
                        b2.SetPixel(i, j, k2);
                    }
                }
            }
            show_Histogram(b2);
            pictureBox2.Invalidate();
        }

        //Zapisywanie otrzymanego obrazu o nazwie wpisanej w textBox1
        private void button12_Click(object sender, EventArgs e)
        {
            if(pictureBox2.Image != null)
            {
                pictureBox2.Image.Save("C:\\Users\\krzys\\OneDrive\\Obrazy\\JPG\\"+ textBox1.Text + "(zmienione).jpg", ImageFormat.Jpeg);
            }
            else
            {
                string message = "Nie ma zmienionego obrazu. Anulować operację?";
                string caption = "Error Detected";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                //Wyświetla MassageBox
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }

            }
        }



    }
}
