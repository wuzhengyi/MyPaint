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
        public void drawPixel(int x, int y)//画点
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

        public void drawPixel(int x, int y, Color cr)//画点
        {
            Brush br = new SolidBrush(cr);
            Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
            pictureBox.Invalidate();
            br.Dispose();
        }

        public static void drawPixel(PictureBox pictureBox,int x, int y, Color cr)//画点
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
                    drawPixel((int)(x + 0.5), (int)(y + 0.5), Color.Blue);
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
            if (NowCase == CASE.choose || NowCase == CASE.panning || NowCase == CASE.chose)
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

        private bool PointInRectangle(int x, int y)
        {
            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                return true;
            else
                return false;
        }

    }
    enum ShapeType {Dot, Line, Roundness, Ellipse, Rectangle, Pencil};
    abstract class Shape
    {
        protected ShapeType type;
        protected int x, y;
        protected PictureBox pictureBox;
        public Color color;

        public ShapeType GetShapeType()
        {
            return type;
        }
        public abstract void Draw();
    }

    class Dot : Shape
    {
        public Dot()
        {
            type = ShapeType.Dot;           
        }

        public void SetPictureBox(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetLocation(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Draw()
        {
            Form1.drawPixel(pictureBox, x, y, color);
        }

    }

}
