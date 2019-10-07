using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX_CG_AULA_2
{
    public partial class Form1 : Form
    {
        private Image imagem;
        private Bitmap imgBitmap;
        private HSI[,] M_HSI;
        private RGB[,] M_RGB;
        private CMY[,] M_CMY;

        public Form1()
        {
            InitializeComponent();
            tbBrilho.SetRange(-100, 100);
            tbMatiz.SetRange(0, 360);

            //M_HSI = new HSI[300, 300];
            //M_RGB = new RGB[300, 300];

            /*for(int i = 0; i < 300; i++)
            {
                for(int j = 0; j < 300; j++)
                {
                    //M_HSI[i, j] = new HSI(0, 0, 0);
                    //M_RGB[i, j] = new RGB(0, 0, 0);
                }
            }*/
        }

        private void BtAbrir_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                double r, g, b, h, s, i, soma;
                imagem = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = imagem;

                M_RGB = new RGB[imagem.Width, imagem.Height];
                M_HSI = new HSI[imagem.Width, imagem.Height];
                M_CMY = new CMY[imagem.Width, imagem.Height];

                labelInfo.Text = openFileDialog1.FileName;
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Color cor;

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        cor = bmp.GetPixel(x, y);
                        Console.WriteLine("[ " + y + " , " + x + " ]");
                        M_RGB[x, y] = new RGB(cor.R, cor.G, cor.B);

                        M_CMY[x, y] = new CMY(255 - cor.R, 255 - cor.G, 255 - cor.B);

                        soma = cor.R + cor.G + cor.B;

                        r = cor.R / soma;
                        g = cor.G / soma;
                        b = cor.B / soma;

                        if (b <= g)
                        {
                            h = (double)Math.Acos((0.5 * ((r - g) + (r - b)) / Math.Sqrt(Math.Pow((r - g), 2) + (r - b) * (g - b))));
                        }
                        else
                        {
                            h = (double)2 * Math.PI - Math.Acos((0.5 * ((r - g) + (r - b)) / Math.Sqrt(Math.Pow((r - g), 2) + (r - b) * (g - b))));
                        }


                        s = 1 - 3 * Math.Min(r, Math.Min(g, b));
                        i = (double)(cor.R + cor.G + cor.B) / (3 * 255);

                        h = (double)(h * 180) / Math.PI;
                        s = (double)s * 100;
                        i = (double)i * 255;

                        M_HSI[x, y] = new HSI(h, s, i);
                    }
                }

                Bitmap bmph = new Bitmap(pictureBox1.Image);
                Bitmap bmps = new Bitmap(pictureBox1.Image);
                Bitmap bmpi = new Bitmap(pictureBox1.Image);

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        double xx, yy, zz;

                        h = (double) M_HSI[x, y].getH() * Math.PI / 180;
                        s = (double) M_HSI[x, y].getS() / 100;
                        i = (double) M_HSI[x, y].getI() / 255;

                        xx = (double) i * (1 - s);
                        
                        if (h < (2 * Math.PI / 3))
                        {
                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            bmph.SetPixel(x, y, Color.FromArgb((int)xx, (int)xx, (int)xx));
                            bmps.SetPixel(x, y, Color.FromArgb((int)yy, (int)yy, (int)yy));
                            bmpi.SetPixel(x, y, Color.FromArgb((int)zz, (int)zz, (int)zz));
                        }
                        else if(h >= (2 * Math.PI / 3) && h < (4 * Math.PI / 3))
                        {
                            h = (double) h - (2 * (Math.PI / 3));

                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            bmph.SetPixel(x, y, Color.FromArgb((int)xx, (int)xx, (int)xx));
                            bmps.SetPixel(x, y, Color.FromArgb((int)yy, (int)yy, (int)yy));
                            bmpi.SetPixel(x, y, Color.FromArgb((int)zz, (int)zz, (int)zz));
                        }
                        else if(h >= (2 * Math.PI / 3) && h < (2 * Math.PI))
                        {
                            h = (double) h - (4 * (Math.PI / 3));

                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            bmph.SetPixel(x, y, Color.FromArgb((int)yy, (int)yy, (int)yy));
                            bmps.SetPixel(x, y, Color.FromArgb((int)xx, (int)xx, (int)xx));
                            bmpi.SetPixel(x, y, Color.FromArgb((int)zz, (int)zz, (int)zz));
                        }
                    }
                }
                pictureBox2.Image = bmph;
                pictureBox3.Image = bmps;
                pictureBox4.Image = bmpi;
            }
        }

        private void BtRemover_Click(object sender, EventArgs e)
        {
            imagem = null;
            pictureBox1.Image = null;
            labelInfo.Text = "Nenhuma Img. Aberta";
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Color cor;
            
            labelX.Text = "X: " + e.X;
            labelY.Text = "Y: " + e.Y;
            int c, m, y;
            double r, g, b, h, s, i;

            if (imagem != null)
            {
                labelR.Text = "R: " + M_RGB[e.X, e.Y].getR();
                labelG.Text = "G: " + M_RGB[e.X, e.Y].getG();
                labelB.Text = "B: " + M_RGB[e.X, e.Y].getB();

                labelC.Text = "C: " + M_CMY[e.X, e.Y].getC();
                labelM.Text = "M: " + M_CMY[e.X, e.Y].getM();
                labelYlw.Text = "Y: " + M_CMY[e.X, e.Y].getY();

                labelH.Text = "H: " + M_HSI[e.X, e.Y].getH();
                labelS.Text = "S: " + M_HSI[e.X, e.Y].getS();
                labelI.Text = "I: " + M_HSI[e.X, e.Y].getI();

            }
        }

        private void alteraBrilho(object sender, MouseEventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                double brilho = tbBrilho.Value;

                Bitmap novo = new Bitmap(pictureBox1.Image);

                for (int y = 0; y < novo.Height; y++)
                {
                    for (int x = 0; x < novo.Width; x++)
                    {
                        double xx, yy, zz, h, s, i;

                        h = (double)M_HSI[x, y].getH() * Math.PI / 180;
                        s = (double)M_HSI[x, y].getS() / 100;
                        i = (double)M_HSI[x, y].getI() / 255;

                        i += (double)i * ((double)tbBrilho.Value / 100);
                        Console.WriteLine(i);

                        xx = (double)i * (1 - s);

                        if (h < (2 * Math.PI / 3))
                        {
                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            if (xx > 255)
                                xx = 255;
                            if (yy > 255)
                                yy = 255;
                            if (zz > 255)
                                zz = 255;

                            novo.SetPixel(x, y, Color.FromArgb((int)yy, (int)zz, (int)xx));
                        }
                        else if (h >= (2 * Math.PI / 3) && h < (4 * Math.PI / 3))
                        {
                            h = (double)h - (2 * (Math.PI / 3));

                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            if (xx > 255)
                                xx = 255;
                            if (yy > 255)
                                yy = 255;
                            if (zz > 255)
                                zz = 255;

                            novo.SetPixel(x, y, Color.FromArgb((int)xx, (int)yy, (int)zz));
                        }
                        else if (h >= (2 * Math.PI / 3) && h < (2 * Math.PI))
                        {
                            h = (double)h - (4 * (Math.PI / 3));

                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            if (xx > 255)
                                xx = 255;
                            if (yy > 255)
                                yy = 255;
                            if (zz > 255)
                                zz = 255;

                            novo.SetPixel(x, y, Color.FromArgb((int)zz, (int)xx, (int)yy));
                        }
                        /*h = (double)(h * 180 / Math.PI);
                        s = (double)s * 100;
                        i = (double)i * 255;

                        M_HSI[x, y].setH(h);
                        M_HSI[x, y].setS(s);
                        M_HSI[x, y].setI(i);*/
                    }
                }
                pictureBox1.Image = novo;
                Console.WriteLine(brilho);
            }
        }

        private void alteraMatiz(object sender, MouseEventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                double matiz = tbMatiz.Value;

                Bitmap novo = new Bitmap(pictureBox1.Image);
                Bitmap bmph = new Bitmap(pictureBox1.Image);

                for (int y = 0; y < novo.Height; y++)
                {
                    for (int x = 0; x < novo.Width; x++)
                    {
                        double xx, yy, zz, h, s, i;

                        h = (double)M_HSI[x, y].getH() * Math.PI / 180;
                        s = (double)M_HSI[x, y].getS() / 100;
                        i = (double)M_HSI[x, y].getI() / 255;

                        h = (double)h * ((double)tbMatiz.Value / 360);
                        Console.WriteLine(h);

                        xx = (double)i * (1 - s);

                        if (h < (2 * Math.PI / 3))
                        {
                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            if (xx > 255)
                                xx = 255;
                            if (yy > 255)
                                yy = 255;
                            if (zz > 255)
                                zz = 255;

                            novo.SetPixel(x, y, Color.FromArgb((int)yy, (int)zz, (int)xx));
                        }
                        else if (h >= (2 * Math.PI / 3) && h < (4 * Math.PI / 3))
                        {
                            h = (double)h - (2 * (Math.PI / 3));

                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            if (xx > 255)
                                xx = 255;
                            if (yy > 255)
                                yy = 255;
                            if (zz > 255)
                                zz = 255;

                            novo.SetPixel(x, y, Color.FromArgb((int)xx, (int)yy, (int)zz));
                        }
                        else if (h >= (2 * Math.PI / 3) && h < (2 * Math.PI))
                        {
                            h = (double)h - (4 * (Math.PI / 3));

                            yy = (double)i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));

                            zz = (double)3 * i - (xx + yy);
                            xx = xx * 255;
                            yy = yy * 255;
                            zz = zz * 255;

                            if (xx > 255)
                                xx = 255;
                            if (yy > 255)
                                yy = 255;
                            if (zz > 255)
                                zz = 255;

                            novo.SetPixel(x, y, Color.FromArgb((int)zz, (int)xx, (int)yy));
                        }
                        /*h = (double)(h * 180 / Math.PI);
                        s = (double)s * 100;
                        i = (double)i * 255;

                        M_HSI[x, y].setH(h);
                        M_HSI[x, y].setS(s);
                        M_HSI[x, y].setI(i);*/
                    }
                }
                pictureBox1.Image = novo;
            }
            
        }

        private void BtLuminancia_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            int width = bmp.Width;
            int height = bmp.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = bmp.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);

                    //nova cor
                    Color newcolor = Color.FromArgb(gs, gs, gs);

                    bmp.SetPixel(x, y, newcolor);
                }
            }

            pictureBox1.Image = bmp;
        }
    }
}
