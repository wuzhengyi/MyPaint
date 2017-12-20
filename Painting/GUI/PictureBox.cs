using System.Drawing;
using System.Windows.Forms;
using Painting.Shapes;

namespace Painting
{
    partial class Form1
    {
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)//鼠标左键松开
        {
            if (NowCase == CASE.selected)
            {
                selectedShape = OperaStep.SelectShape(x0, y0);
                if(selectedShape != null)
                {
                    selectedShape.SelectShape();
                    selectedShape.InitButton(SelectedShapeSize_MouseUp,SelectedShapeSize_MouseDown, SelectedShapeSize_MouseMove);
                }
                    
            }

            RefreshPictureBox();
            if (IsMouseDown)
                IsMouseDown = false;

            switch (NowCase)
            {
                case CASE.NoOperation:
                    break;
                case CASE.line:
                    Line l = new Line();
                    l.InitLine(pictureBox, color, x0, y0, e.X, e.Y);
                    l.Draw();
                    OperaStep.AddStep(l);
                    break;
                case CASE.roundness:
                    Roundness r = new Roundness();
                    r.InitRoundness(pictureBox, color, x0, y0, e.X, e.Y);
                    r.Draw();
                    OperaStep.AddStep(r);
                    break;
                case CASE.ellipse:
                    Ellipse ee = new Ellipse();
                    ee.InitEllipse(pictureBox, color, x0, y0, e.X, e.Y);
                    ee.Draw();
                    OperaStep.AddStep(ee);
                    break;
                case CASE.rectangle:
                    Rect re = new Rect();
                    re.InitRectangle(pictureBox, color, x0, y0, e.X, e.Y);
                    re.Draw();
                    OperaStep.AddStep(re);
                    break;
                case CASE.pencil:
                    break;
                case CASE.fill:
                    break;
                case CASE.selected:
                    
                    /*TODO:
                     * 
                     */
                    break;
                case CASE.chose:
                    break;
                case CASE.panning:
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            if (IsMouseDown)
            {
                pictureBox.Image = FrontImage.Clone() as Image;

                switch (NowCase)
                {
                    case CASE.NoOperation:
                        break;
                    case CASE.line:
                        Line l = new Line();
                        l.InitLine(pictureBox, color, x0, y0, e.X, e.Y);
                        l.Draw();
                        break;
                    case CASE.roundness:
                        Roundness r = new Roundness();
                        r.InitRoundness(pictureBox, color, x0, y0, e.X, e.Y);
                        r.Draw();
                        break;
                    case CASE.ellipse:
                        Ellipse ee = new Ellipse();
                        ee.InitEllipse(pictureBox, color, x0, y0, e.X, e.Y);
                        ee.Draw();
                        break;
                    case CASE.rectangle:
                        Rect re = new Rect();
                        re.InitRectangle(pictureBox, color, x0, y0, e.X, e.Y);
                        re.Draw();
                        break;
                    case CASE.pencil:
                        break;
                    case CASE.fill:
                        break;
                    case CASE.selected:
                        break;
                    case CASE.chose:
                        break;
                    case CASE.panning:
                        break;
                    default:
                        break;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsBack)
            {
                OperaStep.RemoveNullStep();
            }

            //设置起点
            x0 = e.X;
            y0 = e.Y;

            FrontImage = pictureBox.Image.Clone() as Image;

            //标记鼠标摁下
            IsMouseDown = true;

            if (NowCase == CASE.selected)
            {
                if (selectedShape != null)
                {
                    selectedShape.unSelectShape();
                }
            }
        }
        
        private void RefreshPictureBox()
        {
            pictureBox.Image = (Image)Backgroud.Clone();
            OperaStep.DrawOperation();
        }

        public void SelectedShapeSize_MouseDown(object sender, MouseEventArgs e)
        {

        }
        public void SelectedShapeSize_MouseUp(object sender, MouseEventArgs e)
        {

        }
        public void SelectedShapeSize_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
