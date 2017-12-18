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
    class Dot : Shape
    {
        public void InitDot(PictureBox pictureBox, Color color, int x, int y)
        {
            type = ShapeType.Dot;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x, y);
            visible = true;
        }

        public override void Draw()
        {
            if (visible)
                Form1.drawPixel(pictureBox, x0, y0, color);
        }
    }
}
