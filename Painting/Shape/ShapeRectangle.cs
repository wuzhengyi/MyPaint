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

    class Rect : Shape
    {
        public void InitRectangle(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Rectangle;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0, x1, y1);
            this.x1 = x1;
            this.y1 = y1;
            InitShape();
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            Line l = new Line();
            if (selected) l.SelectShape();
            l.InitLine(pictureBox, color, x0, y0, x0, y1);
            if (selected) l.SelectShape();
            l.Draw();
            l.InitLine(pictureBox, color, x0, y0, x1, y0);
            if (selected) l.SelectShape();
            l.Draw();
            l.InitLine(pictureBox, color, x1, y0, x1, y1);
            if (selected) l.SelectShape();
            l.Draw();
            l.InitLine(pictureBox, color, x0, y1, x1, y1);
            if (selected) l.SelectShape();
            l.Draw();
        }

        public override bool PointOnIt(int x, int y)
        {
            Line a = new Line();
            a.InitLine(pictureBox, color, x0, y0, x0, y1);
            if (a.PointOnIt(x, y))
                return true;
            a.InitLine(pictureBox, color, x0, y0, x1, y0);
            if (a.PointOnIt(x, y))
                return true;
            a.InitLine(pictureBox, color, x1, y0, x1, y1);
            if (a.PointOnIt(x, y))
                return true;
            a.InitLine(pictureBox, color, x0, y1, x1, y1);
            if (a.PointOnIt(x, y))
                return true;
            return false;
        }
    }
}
