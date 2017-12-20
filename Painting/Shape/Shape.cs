using Painting.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting.Shapes
{
    enum ShapeType {Dot, Line, Roundness, Ellipse, Rectangle, Pencil, FillCr, FillPic};

    abstract class Shape
    {
        protected ShapeType type;
        protected int x0, y0, x1, y1;
        protected PictureBox pictureBox;
        public Color color;
        public bool visible;
        protected int replaceid;
        protected bool selected;
        protected ControlSizeButton NWButton = new ControlSizeButton();
        protected ControlSizeButton SEButton = new ControlSizeButton();

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

        public void SetLocation(int x0, int y0,int x1, int y1)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
        }

        public void InitShape()
        {
            selected = false;
            visible = true;
            replaceid = -1;
        }

        public void SelectShape()
        {
            selected = true;
            NWButton.Show();
            SEButton.Show();
        }

        public void unSelectShape()
        {
            selected = false;
            NWButton.Hide();
            SEButton.Hide();
        }

        private void DrawSelectArea()
        {

        }

        public void InitButton(MouseEventHandler up, MouseEventHandler down, MouseEventHandler move)
        {
            NWButton.InitButton(new Point(x0, y0), pictureBox, up, down, move);
            SEButton.InitButton(new Point(x1, y1), pictureBox, up, down, move);
        }

        public abstract void Draw();
        public abstract bool PointOnIt(int x,int y);
    }

    

    
}
