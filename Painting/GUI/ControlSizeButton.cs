using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting.GUI
{
    class ControlSizeButton
    {
        private Button button;
        public ControlSizeButton(){
            button = new Button();
            SetSize(new Size(5, 5));    
        }
        public void SetSize(Size size)
        {
            button.Size = size;
        }

        public void SetLocation(Point location)
        {
            button.Location = location;
        }

        public void SetPicturebox(PictureBox picturebox)
        {
            picturebox.Controls.Add(button);
        }

        public void SetMouseUpEvent(MouseEventHandler up)
        {
            button.MouseUp += up;
        }

        public void SetMouseDownEvent(MouseEventHandler down)
        {
            button.MouseDown += down;
        }

        public void SetMouseMoveEvent(MouseEventHandler move)
        {
            button.MouseMove += move;
        }

        public void SetMouseEvent(MouseEventHandler up, MouseEventHandler down, MouseEventHandler move)
        {
            button.MouseMove += move;
            button.MouseDown += down;
            button.MouseUp += up;
        }

        public void InitButton(Point location, PictureBox picturebox, MouseEventHandler up, MouseEventHandler down, MouseEventHandler move)
        {
            button.Location = location;
            picturebox.Controls.Add(button);
            button.MouseMove += move;
            button.MouseDown += down;
            button.MouseUp += up;
        }

        public void Show()
        {
            button.Show();
        }

        public void Hide()
        {
            button.Hide();
        }

        public Point GetLocation()
        {
            return button.Location;
        }
    }
}
