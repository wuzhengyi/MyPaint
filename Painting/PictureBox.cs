using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting
{
    partial class Form1
    {
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

                //将选择区域的图像保存至ChoseRegion
                pictureBox.Image = Step.RefreshStep();
                ChoseRegion = new Bitmap(x2 - x1, y2 - y1);
                Bitmap map = new Bitmap(pictureBox.Image);
                for (int i = x1; i < x2; i++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        ChoseRegion.SetPixel(i - x1, j - y1, map.GetPixel(i, j));
                    }
                }
                DrawRectangle(x1, y1, x2, y2);
                //更改NowCase
                CaseChange(CASE.chose);
                ChoseSize.Location = new Point(x2, y2);
                ChoseSize.Show();
            }
            else if (NowCase == CASE.panning)
            {
                PanningChoseRegion(e.X - x0, e.Y - y0);
                NowCase = CASE.NoOperation;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            if (IsMouseDown)
            {
                if (NowCase == CASE.dot)
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
                            Bresenhamellipse(Math.Abs((x0 - e.X) / 2), Math.Abs((y0 - e.Y) / 2), (x0 + e.X) / 2, (y0 + e.Y) / 2);
                            break;
                        case CASE.rectangle:
                        case CASE.choose:
                            DrawRectangle(x0, y0, e.X, e.Y);
                            break;
                        case CASE.panning:
                            PanningChoseRegion(e.X - x0, e.Y - y0);
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

            if (NowCase != CASE.chose)
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
                case CASE.chose:
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
