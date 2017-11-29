using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting
{
    enum CASE { no_operation, dot, line, curve, square, roundness, rectangle, pencil };
    enum BREATH { ss, s, b, bb};
    
    public partial class Form1 : Form
    {
        private Color color;
        private CASE now_case;
        private BREATH bh;
        private int x1, y1;
        private bool mouse_down, is_back;
        private StepPaint Step = new StepPaint();
        
        //private Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void InitForm1()
        {
            color = Color.Black;
            now_case = CASE.no_operation;
            x1 = y1 = 0;
            button_color.BackColor = color;
            mouse_down = is_back = false;
            bh = BREATH.ss;
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Step.InitStep((Image)pictureBox.Image.Clone());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitForm1();
        }

        private void drawPixel(int x, int y)//画点
        {
            Brush br = new SolidBrush(color);
            switch (bh)
            {
                case BREATH.ss:
                    Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
                    break;
                case BREATH.s:
                    bh = BREATH.ss;
                    Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
                    BresenhamCircle(1, x, y);
                    bh = BREATH.s;
                    break;
                case BREATH.b:
                    bh = BREATH.ss;
                    Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
                    for (int i = 1; i < 3; i++)
                        BresenhamCircle(1 + i, x, y);
                    bh = BREATH.b;
                    break;
                case BREATH.bb:
                    bh = BREATH.ss;
                    Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
                    for (int i = 1; i < 5; i++)
                        BresenhamCircle(1 + i, x, y);
                    bh = BREATH.bb;
                    break;
                default:
                    Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
                    break;
            }
            
            pictureBox.Invalidate();
            br.Dispose();
        }

        //private void drawPixel(int x, int y,Color cr)//画点
        //{
        //    Brush br = new SolidBrush(cr);
        //    //Graphics g = pictureBox.CreateGraphics();
        //    //Graphics g = Graphics.FromImage(bmp);
        //    //g.FillRectangle(br, x, y, 1, 1);
        //    Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
        //    pictureBox.Invalidate();
        //    br.Dispose();
        //}

        private void DDALine(int x1, int y1, int x2, int y2)
        {
            double dx, dy, e, x, y;
            dx = x2 - x1;
            dy = y2 - y1;
            e = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx /= e; dy /= e;
            x = x1;
            y = y1;
            for (int i = 1; i <= e; i++)
            {
                drawPixel((int)(x + 0.5), (int)(y + 0.5));
                x += dx;
                y += dy;
            }
        }

        private void BresenhamLine(int x1, int y1, int x2, int y2)
        {
            int x, y, dx, dy, p;
            x = x1;
            y = y1;
            dx = x2 - x1;
            dy = y2 - y1;
            p = 2 * dy - dx;
            for (; x <= x2; x++)
            {
                drawPixel(x, y);
                if (p >= 0)
                {
                    y++;
                    p += 2 * (dy - dx);
                }
                else
                {
                    p += 2 * dy;
                }
            }
        }

        private void MidpointLine(int x0, int y0, int x1, int y1)
        {
            int a, b, delta1, delta2, d, x, y;
            a = y0 - y1;
            b = x1 - x0;
            d = 2 * a + b;
            delta1 = 2 * a;
            delta2 = 2 * (a + b);
            x = x0;
            y = y0;
            drawPixel(x, y);
            while (x < x1)
            {
                if (d < 0)
                {
                    x++;
                    y++;
                    d += delta2;
                }
                else
                {
                    x++;
                    d += delta1;
                }
                drawPixel(x, y);
            } /* while */
        }/* MidpointLine */


        //private void DDALine(int x1, int y1, int x2, int y2, Color cr)
        //{
        //    double dx, dy, e, x, y;
        //    dx = x2 - x1;
        //    dy = y2 - y1;
        //    e = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
        //    dx /= e; dy /= e;
        //    x = x1;
        //    y = y1;
        //    for (int i = 1; i <= e; i++)
        //    {
        //        drawPixel((int)(x + 0.5), (int)(y + 0.5), cr);
        //        x += dx;
        //        y += dy;
        //    }
        //}

        //private void MidpointCircle2(int R, int xc, int yc)
        //{
        //    int x, y, deltax, deltay, d;
        //    x = 0; y = R; d = 1 - R;
        //    deltax = 3;
        //    deltay = 5 - R - R;
        //    drawPixel(x + xc, y + yc);
        //    while (x < y)
        //    {
        //        if (d < 0)
        //        {
        //            d += deltax;
        //            deltax += 2;
        //            deltay += 2;
        //            x++;
        //        }
        //        else
        //        {
        //            d += deltay;
        //            deltax += 2;
        //            deltay += 4;
        //            x++;
        //            y--;
        //        }
        //        drawPixel(x + xc, y + yc);
        //    }
        //}

        private void BresenhamCircle(int R, int xc, int yc)
        {
            int x, y, p;
            x = 0; y = R;
            p = 3 - 2 * R;
            for (; x <= y; x++)
            {
                drawPixel(x + xc, y + yc);
                drawPixel(x + xc, -y + yc);
                drawPixel(y + xc, x + yc);
                drawPixel(y + xc, -x + yc);
                drawPixel(-x + xc, y + yc);
                drawPixel(-x + xc, -y + yc);
                drawPixel(-y + xc, x + yc);
                drawPixel(-y + xc, -x + yc);
                if (p >= 0)
                {
                    p += 4 * (x - y) + 10;
                    y--;
                }
                else
                {
                    p += 4 * x + 6;
                }
            }
        }

        //private void BresenhamCircle(int R, int xc, int yc, Color cr)
        //{
        //    int x, y, p;
        //    x = 0; y = R;
        //    p = 3 - 2 * R;
        //    for (; x <= y; x++)
        //    {
        //        drawPixel(x + xc, y + yc, cr);
        //        drawPixel(x + xc, -y + yc, cr);
        //        drawPixel(y + xc, x + yc, cr);
        //        drawPixel(y + xc, -x + yc, cr);
        //        drawPixel(-x + xc, y + yc, cr);
        //        drawPixel(-x + xc, -y + yc, cr);
        //        drawPixel(-y + xc, x + yc, cr);
        //        drawPixel(-y + xc, -x + yc, cr);

        //        if (p >= 0)
        //        {
        //            p += 4 * (x - y) + 10;
        //            y--;
        //        }
        //        else
        //        {
        //            p += 4 * x + 6;
        //        }
        //    }
        //}
        private void button_white_Click(object sender, EventArgs e)
        {
            color = Color.White;
            button_color.BackColor = color;
        }

        private void button_black_Click(object sender, EventArgs e)
        {
            color = Color.Black;
            button_color.BackColor = color;
        }

        private void button_red_Click(object sender, EventArgs e)
        {
            color = Color.Red;
            button_color.BackColor = color;
        }

        private void button_orange_Click(object sender, EventArgs e)
        {
            color = Color.Orange;
            button_color.BackColor = color;
        }

        private void button_yellow_Click(object sender, EventArgs e)
        {
            color = Color.Yellow;
            button_color.BackColor = color;
        }

        private void button_lime_Click(object sender, EventArgs e)
        {
            color = Color.Lime;
            button_color.BackColor = color;
        }

        private void button_aqua_Click(object sender, EventArgs e)
        {
            color = Color.Aqua;
            button_color.BackColor = color;
        }

        private void button_blue_Click(object sender, EventArgs e)
        {
            color = Color.Blue;
            button_color.BackColor = color;
        }

        private void button_fuchsia_Click(object sender, EventArgs e)
        {
            color = Color.Fuchsia;
            button_color.BackColor = color;
        }

        private void button_silver_Click(object sender, EventArgs e)
        {
            color = Color.Silver;
            button_color.BackColor = color;
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
            openFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
            openFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
            openFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
            string path = "";                                     //用于保存打开文件的路径
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                //MessageBox.Show(path);                            //显示该路径名
            }
           
            pictureBox.ImageLocation = path;
            Step.InitStep(Image.FromFile(path));
            //Step.ShowImageStep(0);
        }

        private void savefile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
            saveFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
            saveFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
            saveFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
            string path = "";                                     //用于保存打开文件的路径
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                //MessageBox.Show(path);                            //显示该路径名
            }
            pictureBox.Image.Save(path);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            is_back = true;
            if(!Step.StepIsNull())
                pictureBox.Image = Step.PopStep();
        }

        private void button_front_Click(object sender, EventArgs e)
        {
            if (!Step.StepIsFull())
                pictureBox.Image = Step.PushStep();
        }

        private void button_line_Click_1(object sender, EventArgs e)//直线
        {
            now_case = CASE.line;
        }

        private void button_dot_Click(object sender, EventArgs e)//点
        {
            now_case = CASE.dot;
        }

        private void button_pencil_Click(object sender, EventArgs e)
        {
            now_case = CASE.pencil;
        }

        private void button_roundness_Click(object sender, EventArgs e)
        {
            now_case = CASE.roundness;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)//鼠标左键松开
        {
            if (mouse_down)
                mouse_down = false;
            if (is_back)
            {
                Step.RemoveNullStep();
                is_back = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            if (mouse_down)
            {
                if(now_case == CASE.dot)
                {
                    drawPixel(e.X, e.Y);
                }
                else if (now_case == CASE.pencil)
                {
                    DDALine(x1, y1, e.X, e.Y);
                    x1 = e.X;
                    y1 = e.Y;
                }
                else
                {
                    pictureBox.Image = Step.RefreshStep();
                    switch (now_case)
                    {
                        case CASE.no_operation:
                            break;
                        case CASE.dot:
                            //drawPixel(e.X, e.Y);
                            break;
                        case CASE.line:
                            DDALine(x1, y1, e.X, e.Y);
                            //BresenhamLine(x1, y1, e.X, e.Y);
                            //MidpointLine(x1, y1, e.X, e.Y);
                            break;
                        case CASE.curve:
                            break;
                        case CASE.square:
                            break;
                        case CASE.roundness:
                            BresenhamCircle((int)Math.Sqrt((x1 - e.X) * (x1 - e.X) + (y1 - e.Y) * (y1 - e.Y)) / 2, (x1 + e.X) / 2, (y1 + e.Y) / 2);
                            break;
                        case CASE.rectangle:
                            break;
                        case CASE.pencil:
                            break;
                        default:
                            break;
                    }
                }
                
            }
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
   
            //设置起点
            x1 = e.X;
            y1 = e.Y;

            //标记鼠标摁下
            mouse_down = true;

            Image temp = (Image)pictureBox.Image.Clone();
            //Form fm = new Form();
            //fm.BackgroundImage=temp;
            //fm.Show();
            
            Step.AddStep(temp);
            pictureBox.Image = temp;
        }

        private void 好细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.ss;
        }

        private void 细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.s;
        }

        private void 粗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.b;
        }

        private void 好粗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.bb;
        }
    }
}
