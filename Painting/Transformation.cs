using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painting
{
    partial class Form1
    {
        //平移ChoseRegion选区
        private void PanningChoseRegion(int dx, int dy)
        {
            FillColor(color, x1, y1, x2, y2);
            if (x1 + dx > 0 && x2 + dx < pictureBox.Width && y1 + dy > 0 && y2 + dy < pictureBox.Height)
            {
                FillPic(ChoseRegion, x1 + dx, y1 + dy, x2 + dx, y2 + dy);
                if (IsMouseDown)
                    DrawRectangle(x1 + dx, y1 + dy, x2 + dx - 1, y2 + dy - 1);
            }

        }
    }
}
