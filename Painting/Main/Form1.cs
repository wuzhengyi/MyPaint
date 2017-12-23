using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Painting.Shapes;

namespace Painting
{
    enum CASE { NoOperation, dot, line, roundness, ellipse,
        rectangle, pencil, clip, selected, polygon, Panning
        };

    enum BREATH { ss, s, b, bb};

    public struct point
    {
        public int x;
        public int y;
    };

    public partial class Form1 : Form
    {
        private Color color;
        private CASE NowCase;
        private BREATH bh;
        private int x0, y0, x1, y1, x2, y2;
        private Point p;
        private bool IsMouseDown, IsBack;
        private Image FrontImage;
        private Shape selectedShape;
        private Polygon pn;



        private OperationStep OperaStep = new OperationStep();
        private Image Backgroud;

        //private Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void InitForm1()
        {
            color = Color.Black;
            NowCase = CASE.NoOperation;
            x0 = y0 = x1 = y1 = x2 = y2 = 0;
            button_color.BackColor = color;
            IsMouseDown = IsBack = false;
            bh = BREATH.ss;
            Bitmap b = new Bitmap(pictureBox.Width, pictureBox.Height);     //新建位图b1
            Graphics g1 = Graphics.FromImage(b);  //创建b1的Graphics
            g1.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));   //把b1涂成红色
            pictureBox.Image = b;
            Backgroud = (Image)b.Clone();
            selectedShape = null;
            Shape.SetMouseEvent(SelectedShapeSize_MouseUp, SelectedShapeSize_MouseDown, SelectedShapeSize_MouseMove);
            pn = null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitForm1();
        }         

        


    }
}
