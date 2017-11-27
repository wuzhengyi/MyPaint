using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Painting
{
    enum CASE { NoOperation, dot, line, curve, roundness, ellipse, rectangle, pencil, fill, choose, choosed, panning };
    /*  no_operation：   无操作
     *  dot：            点
     *  line：           线
     *  curve：          曲线
     *  roundness：      圆形
     *  ellipse：        椭圆
     *  rectangle：      矩形
     *  pencil：         铅笔
     *  fill：           填充
     *  choose：         选择
     *  choosed：        选择完毕
     *  panning：        平移
     */
    enum BREATH { ss, s, b, bb};

    public struct point
    {
        public int x;
        public int y;
    };

    public partial class Form1 : Form
    {
        private Color color;
        private CASE NowCase;
        private BREATH bh;
        private int x0, y0, x1, y1, x2, y2;
        private bool IsMouseDown, IsBack;
        private StepPaint Step = new StepPaint();
        private Bitmap ChoosedRegion;
        
        //private Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void InitForm1()
        {
            color = Color.Black;
            NowCase = CASE.NoOperation;
            x0 = y0 = 0;
            button_color.BackColor = color;
            IsMouseDown = IsBack = false;
            bh = BREATH.ss;
            //pictureBox.Image = new Bitmap(2000, 2000);
            Bitmap b = new Bitmap(pictureBox.Width, pictureBox.Height);     //新建位图b1
            Graphics g1 = Graphics.FromImage(b);  //创建b1的Graphics
            g1.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));   //把b1涂成红色
            pictureBox.Image = b;
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
                    for (int i = 1; i < 4; i++)
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

        private void drawPixel(int x, int y, Color cr)//画点
        {
            Brush br = new SolidBrush(cr);
            Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
            pictureBox.Invalidate();
            br.Dispose();
        }

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

        private void DDADottedLine(int x1, int y1, int x2, int y2)
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
                if (i % 10 < 5)
                    drawPixel((int)(x + 0.5), (int)(y + 0.5),Color.Blue);
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

        private void DrawRectangle(int x0, int y0, int x1, int y1)
        {
            if (NowCase == CASE.choose || NowCase==CASE.panning)
            {
                DDADottedLine(x0, y0, x0, y1);
                DDADottedLine(x0, y0, x1, y0);
                DDADottedLine(x1, y0, x1, y1);
                DDADottedLine(x0, y1, x1, y1);
            }
           else
            {
                DDALine(x0, y0, x0, y1);
                DDALine(x0, y0, x1, y0);
                DDALine(x1, y0, x1, y1);
                DDALine(x0, y1, x1, y1);
                
            }
        }

        
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

        private void Bresenhamellipse(int rx, int ry, int xc, int yc)
        {
            /* double的版本 所以有点卡
             
            double x, y, p, t1, t2;
            x = 0; y = ry;
            p = ry * ry - rx * rx * ry + rx * rx / 4;
            t1 = 0;
            t2 = 2 * rx * rx * y;
            for (; ry * ry * x < rx * rx * y; x++)
            {
                drawPixel((int)(x + xc), (int)(y + yc));
                drawPixel((int)(x + xc), (int)(-y + yc));
                drawPixel((int)(-x + xc), (int)(y + yc));
                drawPixel((int)(-x + xc), (int)(-y + yc));
                t1 += 2 * ry * ry;
                if (p >= 0)
                {
                    y--;
                    t2 -= 2 * rx * rx;
                    p += t1 - t2 + ry * ry;
                }
                else
                {
                    p += t1 + ry * ry;
                }
            }
            //我靠ppt这里面给错了？？？？我找了半天问题
            p = (ry * (x + 0.5) * ry * (x + 0.5)) + rx * rx * (y - 1) * (y - 1) - rx * ry * rx * ry;
            for (; y >= 0; y--)
            {
                drawPixel((int)(x + xc), (int)(y + yc));
                drawPixel((int)(x + xc), (int)(-y + yc));
                drawPixel((int)(-x + xc), (int)(y + yc));
                drawPixel((int)(-x + xc), (int)(-y + yc));

                t2 -= 2 * rx * rx;
                if (p >= 0)
                {
                    p += rx * rx - t2;
                }
                else
                {
                    x++;
                    t1 += 2 * ry * ry;
                    p += rx * rx + t1 - t2;
                }
            }*/

            int x, y, p, t1, t2;
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            x = 0; y = ry;
            p = ry2 - rx2 * ry + rx2 / 4;
            t1 = 0;
            t2 = 2 * rx2 * y;
            for (; ry2 * x < rx2 * y; x++)
            {
                drawPixel(x + xc, y + yc);
                drawPixel(x + xc, -y + yc);
                drawPixel(-x + xc, y + yc);
                drawPixel(-x + xc, -y + yc);
                t1 += 2 * ry2;
                if (p >= 0)
                {
                    y--;
                    t2 -= 2 * rx2;
                    p += t1 - t2 + ry2;
                }
                else
                {
                    p += t1 + ry2;
                }
            }
            //我靠ppt这里面给错了？？？？我找了半天问题
            p = ry2 * (x * x + x) + ry2 / 4 + rx2 * (y - 1) * (y - 1) - rx2 * ry2;
            for (; y >= 0; y--)
            {
                drawPixel(x + xc, y + yc);
                drawPixel(x + xc, -y + yc);
                drawPixel(-x + xc, y + yc);
                drawPixel(-x + xc, -y + yc);

                t2 -= 2 * rx2;
                if (p >= 0)
                {
                    p += rx2 - t2;
                }
                else
                {
                    x++;
                    t1 += 2 * ry2;
                    p += rx2 + t1 - t2;
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
        private void openfile_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            openFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
            openFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
            openFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
            openFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
            string path = "";                                     //用于保存打开文件的路径
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                //MessageBox.Show(path);                            //显示该路径名
                pictureBox.ImageLocation = path;
                Step.InitStep(Image.FromFile(path));
            }
           
            
        }

        private void savefile_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            saveFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
            saveFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
            saveFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
            saveFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
            string path = "";                                     //用于保存打开文件的路径
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                //MessageBox.Show(path);                            //显示该路径名
                pictureBox.Image.Save(path);
            }
            
        }

        private bool PointInRectangle(int x,int y)
        {
            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                return true;
            else
                return false;
        }

        private void FillColor(Stack<point> pointStack, Bitmap map, Color OldColor, int x, int y)
        {
            point temp;
            while (pointStack.Count != 0)
            {
                temp = pointStack.Pop();
                if (temp.x < 0 || temp.y < 0 || temp.x >= map.Width || temp.y >= map.Height || temp.x >= pictureBox.Width || temp.y >= pictureBox.Height) 
                    continue;
                if (map.GetPixel(temp.x, temp.y) == OldColor && map.GetPixel(temp.x, temp.y) != color)
                {
                    map.SetPixel(temp.x, temp.y, color);
                    temp.x++;
                    pointStack.Push(temp);
                    temp.x -= 2;
                    pointStack.Push(temp);
                    temp.x++;
                    temp.y++;
                    pointStack.Push(temp);
                    temp.y -= 2;
                    pointStack.Push(temp);
                }
            }

        }

        private void FillPic(Bitmap pic, int x0, int y0, int x1, int y1)
        {
            Bitmap map = new Bitmap(pictureBox.Image);
            for (int i = x0; i < x1; i++)
            {
                for (int j = y0; j < y1; j++)
                {
                    map.SetPixel(i, j, pic.GetPixel(i - x0, j - y0));
                }
            }
            pictureBox.Image = map;
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
            {
                pictureBox.Image = Step.RefreshStep();
                NowCase = CASE.NoOperation;
            }
                
            if (!IsBack)
            {
                Image temp = (Image)pictureBox.Image.Clone();
                Step.AddStep(temp);
                pictureBox.Image = temp;
                IsBack = true;
            }
            
            if(!Step.StepIsNull())
                pictureBox.Image = Step.PopStep();
        }

        private void button_front_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
            {
                pictureBox.Image = Step.RefreshStep();
                NowCase = CASE.NoOperation;
            }
            if (!Step.StepIsFull())
                pictureBox.Image = Step.PushStep();
        }

        private void button_line_Click_1(object sender, EventArgs e)//直线
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.line;
        }

        private void button_dot_Click(object sender, EventArgs e)//点
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.dot;
        }

        private void button_pencil_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.pencil;
        }

        private void button_ellipse_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.ellipse;
        }

        private void button_roundness_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.roundness;
        }

        private void FillColor(Color c,int x1,int y1,int x2,int y2)
        {
            //在矩形内填充纯色
            Bitmap map = new Bitmap(pictureBox.Image);
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    map.SetPixel(i, j, color);
                }
            }
            pictureBox.Image = map;
        }

        private void button_fill_Click(object sender, EventArgs e)
        {
            if (NowCase != CASE.choosed)
            {
                NowCase = CASE.fill;
            }
            else
            {
                pictureBox.Image = Step.RefreshStep();
                //在选择框内填充纯色
                FillColor(color, x1, y1, x2, y2);

                NowCase = CASE.NoOperation;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button but = new Button();
            but.Location = new Point(pictureBox.Image.Width-2,
                pictureBox.Image.Height-2);
            but.Size = new Size(5, 5);
            pictureBox.Controls.Add(but);
        }

        private void button_rectangle_Click(object sender, EventArgs e)
        {
            if(NowCase==CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.rectangle;
        }

        private void button_choose_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.choosed)
                pictureBox.Image = Step.RefreshStep();
            NowCase = CASE.choose;
        }

        private void button_fillpic_Click(object sender, EventArgs e)
        {
            if (NowCase != CASE.choosed)
            {
                MessageBox.Show("请先选择填充范围！");
            }
            else
            {
                pictureBox.Image = Step.RefreshStep();
                openFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
                openFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
                openFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
                openFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
                string path = "";                                     //用于保存打开文件的路径
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    //MessageBox.Show(path);                            //显示该路径名
                    Bitmap FilledPic = new Bitmap(path);
                    FillPic(FilledPic, x1, y1, x2, y2);
                }
                NowCase = CASE.NoOperation;
            }
        }

        private void PanningChoosedRegion(int dx, int dy)
        {
            if (x1 + dx > 0 && x2 + dx < pictureBox.Width && y1 + dy > 0 && y2 + dy < pictureBox.Height) 
            {
                FillColor(color, x1, y1, x2, y2);
                FillPic(ChoosedRegion, x1 + dx, y1 + dy, x2 + dx, y2 + dy);
                if(IsMouseDown)
                    DrawRectangle(x1 + dx, y1 + dy, x2 + dx - 1, y2 + dy - 1);
            }
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)//鼠标左键松开
        {
            if (IsMouseDown)
                IsMouseDown = false;

            if (IsBack)
            {
                Step.RemoveNullStep();
                IsBack = false;
            }

            if (NowCase == CASE.choose)
            {                
                //(x1,y1),(x2,y2)为选择区域的左上角与右下角
                x2 = Math.Max(x0, e.X);
                x1 = Math.Min(x0, e.X);
                y2 = Math.Max(y0, e.Y);
                y1 = Math.Min(y0, e.Y);

                //将选择区域的图像保存至ChoosedRegion
                pictureBox.Image = Step.RefreshStep();
                ChoosedRegion = new Bitmap(x2 - x1, y2 - y1);
                Bitmap map = new Bitmap(pictureBox.Image);
                for (int i = x1; i < x2; i++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        ChoosedRegion.SetPixel(i - x1, j - y1, map.GetPixel(i, j));
                    }
                }
                DrawRectangle(x1, y1, x2, y2);
                //更改NowCase
                NowCase = CASE.choosed;
            }
            else if (NowCase == CASE.panning)
            {
                PanningChoosedRegion(e.X - x0, e.Y - y0);
                NowCase = CASE.NoOperation;
            }
            
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            if (IsMouseDown)
            {
                if(NowCase == CASE.dot)
                {
                    drawPixel(e.X, e.Y);
                }
                else if (NowCase == CASE.pencil)
                {
                    DDALine(x0, y0, e.X, e.Y);
                    x0 = e.X;
                    y0 = e.Y;
                }
                else
                {
                    pictureBox.Image = Step.RefreshStep();
                    switch (NowCase)
                    {
                        case CASE.line:
                            DDALine(x0, y0, e.X, e.Y);
                            //BresenhamLine(x1, y1, e.X, e.Y);
                            //MidpointLine(x1, y1, e.X, e.Y);
                            break;                       
                        case CASE.roundness:
                            BresenhamCircle((int)Math.Sqrt((x0 - e.X) * (x0 - e.X) + (y0 - e.Y) * (y0 - e.Y)) / 2, (x0 + e.X) / 2, (y0 + e.Y) / 2);
                            break;
                        case CASE.ellipse:
                            Bresenhamellipse(Math.Abs((x0 - e.X)/2), Math.Abs((y0 - e.Y)/2), (x0 + e.X) / 2, (y0 + e.Y) / 2);
                            break;
                        case CASE.rectangle:
                        case CASE.choose:
                            DrawRectangle(x0, y0, e.X, e.Y);
                            break;
                        case CASE.panning:
                            PanningChoosedRegion(e.X - x0, e.Y - y0);
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
            x0 = e.X;
            y0 = e.Y;

            if (NowCase != CASE.choosed)
            {
                Image temp = (Image)pictureBox.Image.Clone();
                Step.AddStep(temp);
            }            

            switch (NowCase)
            {
                case CASE.dot:
                    drawPixel(x0, y0);
                    break;
                case CASE.fill:
                    Bitmap map = new Bitmap(pictureBox.Image);
                    Stack<point> pointStack = new Stack<point>();
                    point tempPoint = new point();
                    tempPoint.x = x0;
                    tempPoint.y = y0;
                    pointStack.Push(tempPoint);
                    FillColor(pointStack, map, map.GetPixel(x0, y0), x0, y0);
                    pictureBox.Image = map;
                    break;
                case CASE.choosed:
                    if (PointInRectangle(x0, y0))
                    {
                        NowCase = CASE.panning;
                    }
                    else
                    {
                        //设定为旋转
                    }
                    break;
                default:
                    break;
            }              

            //标记鼠标摁下
            IsMouseDown = true;
        }

    }
}
