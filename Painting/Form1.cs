using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Painting
{
    enum CASE { NoOperation, dot, line, curve, roundness, ellipse,
        rectangle, pencil, fill, choose, chose, panning
        };
    /*  no_operation：   无操作
     *  dot：            点
     *  line：           线
     *  curve：          曲线
     *  roundness：      圆形
     *  ellipse：        椭圆
     *  rectangle：      矩形
     *  pencil：         铅笔
     *  fill：           填充
     *  choose：         选择
     *  chose：        选择完毕
     *  panning：        平移
     */
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
        private StepPaint Step = new StepPaint();
        private Bitmap ChoseRegion;



        private Button pictureBoxSize;
        private Button ChoseSize;

        //private Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void InitButton()
        {
            //pictureBoxSize
            pictureBoxSize = new Button();
            pictureBoxSize.Size = new Size(5, 5);
            pictureBoxSize.Location = new Point(pictureBox.Width, pictureBox.Height);
            pictureBox.Controls.Add(pictureBoxSize);
            pictureBoxSize.MouseDown += PictureBoxSize_MouseDown;
            pictureBoxSize.MouseMove += PictureBoxSize_MouseMove;
            pictureBoxSize.MouseUp += PictureBoxSize_MouseUp;

            //ChoseSize           
            ChoseSize = new Button();
            ChoseSize.Size = new Size(5, 5);
            ChoseSize.Location = new Point(x2, y2);
            pictureBox.Controls.Add(ChoseSize);
            ChoseSize.MouseDown += ChoseSize_MouseDown;
            ChoseSize.MouseMove += ChoseSize_MouseMove;
            ChoseSize.MouseUp += ChoseSize_MouseUp;
            ChoseSize.Hide();
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
            InitButton();
            Step.InitStep((Image)pictureBox.Image.Clone());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitForm1();
        }         

        


    }
}
