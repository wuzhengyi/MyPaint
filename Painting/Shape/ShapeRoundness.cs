﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting
{
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
            InitShape();
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
                DrawPixel(x, x0, y, y0);

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

        public void DrawPixel(int x,int x0,int y,int y0)
        {
            if (selected)
            {
                if (x % 10 < 5)
                    return;
                Form1.drawPixel(pictureBox, x + x0, y + y0, Color.Blue);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, Color.Blue);
                Form1.drawPixel(pictureBox, y + x0, x + y0, Color.Blue);
                Form1.drawPixel(pictureBox, y + x0, -x + y0, Color.Blue);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, Color.Blue);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, Color.Blue);
                Form1.drawPixel(pictureBox, -y + x0, x + y0, Color.Blue);
                Form1.drawPixel(pictureBox, -y + x0, -x + y0, Color.Blue);
            }
            else
            {
                Form1.drawPixel(pictureBox, x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, y + x0, x + y0, color);
                Form1.drawPixel(pictureBox, y + x0, -x + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, -y + x0, x + y0, color);
                Form1.drawPixel(pictureBox, -y + x0, -x + y0, color);
            }
        }
    }
}
