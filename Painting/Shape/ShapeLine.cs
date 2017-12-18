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
    
}
