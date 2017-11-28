using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painting
{
    partial class Form1
    {
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

        private void FillColor(Color c, int x1, int y1, int x2, int y2)
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

        private void FillPic(Bitmap pic, int x0, int y0, int x1, int y1)
        {
            Bitmap map = new Bitmap(pictureBox.Image);
            for (int i = x0; i < x1; i++)
            {
                for (int j = y0; j < y1; j++)
                {
                    if (i - x0 > 0 && i - x0 < map.Width && j - y0 > 0 && j - y0 < map.Height)
                        map.SetPixel(i, j, pic.GetPixel(i - x0, j - y0));
                }
            }
            pictureBox.Image = map;
        }
    }
}
