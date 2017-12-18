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
            InitShape();
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
