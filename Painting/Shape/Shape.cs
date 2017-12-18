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
    enum ShapeType {Dot, Line, Roundness, Ellipse, Rectangle, Pencil, FillCr, FillPic};

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

    

    
}
