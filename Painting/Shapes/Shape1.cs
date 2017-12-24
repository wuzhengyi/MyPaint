using System;
using System.Collections;
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
        public static void drawPixel(PictureBox pictureBox, int x, int y, Color cr)//画点
        {
            Brush br = new SolidBrush(cr);
            Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
            pictureBox.Invalidate();
            br.Dispose();
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
                    //drawPixel((int)(x + 0.5), (int)(y + 0.5), Color.Blue);
                    x += dx;
                y += dy;
            }
        }

        private bool PointInRectangle(int x, int y)
        {
            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                return true;
            else
                return false;
        }

    }

    enum ShapeType { Dot, Line, Roundness, Ellipse, Rectangle, Pencil, FillCr, FillPic };

    abstract class Shape
    {
        protected ShapeType type;
        protected int x0, y0;
        protected PictureBox pictureBox;
        public Color color;
        public bool visible;
        private int replaceid;
        public ShapeType GetShapeType()
        {
            return type;
        }

        public void SetPictureBox(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetLocation(int x, int y)
        {
            this.x0 = x;
            this.y0 = y;
        }

        public abstract void Draw();
    }



    class Line : Shape
    {
        int x1, y1;
        public void InitLine(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Line;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0);
            this.x1 = x1;
            this.y1 = y1;
            visible = true;
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }

            double dx, dy, e, x, y;
            dx = x1 - x0;
            dy = y1 - y0;
            e = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx /= e; dy /= e;
            x = x0;
            y = y0;
            for (int i = 1; i <= e; i++)
            {
                Form1.drawPixel(pictureBox, (int)(x + 0.5), (int)(y + 0.5), color);
                x += dx;
                y += dy;
            }
        }
    }

    class Roundness : Shape
    {
        int r;
        public void InitRoundness(PictureBox pictureBox, Color color, int r, int x, int y)
        {
            type = ShapeType.Roundness;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x, y);
            this.r = r;
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            int x, y, p;
            x = 0; y = r;
            p = 3 - 2 * r;
            for (; x <= y; x++)
            {
                Form1.drawPixel(pictureBox, x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, y + x0, x + y0, color);
                Form1.drawPixel(pictureBox, y + x0, -x + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, -y + x0, x + y0, color);
                Form1.drawPixel(pictureBox, -y + x0, -x + y0, color);

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
    }

    class Ellipse : Shape
    {
        int rx, ry;
        public void InitEllipse(PictureBox pictureBox, Color color, int rx, int ry, int x, int y)
        {
            type = ShapeType.Ellipse;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x, y);
            this.rx = rx;
            this.ry = ry;
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            int x, y, p, t1, t2;
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            x = 0; y = ry;
            p = ry2 - rx2 * ry + rx2 / 4;
            t1 = 0;
            t2 = 2 * rx2 * y;
            for (; ry2 * x < rx2 * y; x++)
            {
                Form1.drawPixel(pictureBox, x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, color);

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
                Form1.drawPixel(pictureBox, x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, color);

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
    }

    class Rect : Shape
    {
        int x1, y1;
        public void InitRectangle(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Rectangle;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0);
            this.x1 = x1;
            this.y1 = y1;
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            Line l = new Line();
            l.InitLine(pictureBox, color, x0, y0, x0, y1);
            l.Draw();
            l.InitLine(pictureBox, color, x0, y0, x1, y0);
            l.Draw();
            l.InitLine(pictureBox, color, x1, y0, x1, y1);
            l.Draw();
            l.InitLine(pictureBox, color, x0, y1, x1, y1);
            l.Draw();
        }
    }

    class FillCr : Shape
    {
        Color OldColor;
        Bitmap map;
        public void InitFillCr(PictureBox pictureBox, Color color, int x, int y)
        {
            type = ShapeType.FillCr;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x, y);
            Bitmap map = new Bitmap(pictureBox.Image.Clone() as Image);
            OldColor = map.GetPixel(x, y);
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            Point temp;
            Stack<Point> pointStack = new Stack<Point>();
            Point t = new Point(x0, y0);
            while (pointStack.Count != 0)
            {
                temp = pointStack.Pop();
                if (temp.X < 0 || temp.Y < 0 || temp.X >= map.Width || temp.Y >= map.Height || temp.X >= pictureBox.Width || temp.Y >= pictureBox.Height)
                    continue;
                if (map.GetPixel(temp.X, temp.Y) == OldColor && map.GetPixel(temp.X, temp.Y) != color)
                {
                    map.SetPixel(temp.X, temp.Y, color);
                    temp.X++;
                    pointStack.Push(temp);
                    temp.X -= 2;
                    pointStack.Push(temp);
                    temp.X++;
                    temp.Y++;
                    pointStack.Push(temp);
                    temp.Y -= 2;
                    pointStack.Push(temp);
                }
            }
            pictureBox.Image = map;
        }
    }
}
