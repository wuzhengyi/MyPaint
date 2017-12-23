using System.Drawing;
using System.Windows.Forms;
using Painting.Shapes;
using System;
using System.Runtime.InteropServices;

namespace Painting
{
    partial class Form1
    {
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)//鼠标左键松开
        {
            if (NowCase == CASE.selected)
            {
                if (selectedShape != null)
                    selectedShape.unSelectShape();
                selectedShape = OperaStep.SelectShape(x0, y0);
                if(selectedShape != null)
                {                    
                    selectedShape.InitButton();
                    selectedShape.SelectShape();
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
                    break;
                case CASE.polygon:
                    pn.AddPoint(e.X, e.Y);
                    pn.UpdateEdge(e.X, e.Y);
                    pn.Draw();
                    break;
                case CASE.Panning:
                    selectedShape.Setdxdy(e.X - x0, e.Y - y0);
                    if (selectedShape.GetShapeType() != ShapeType.polygon)
                        selectedShape.UpdatePoint();
                    else
                        ((Polygon)selectedShape).Updatedxdy();
                    selectedShape.Draw();
                    CaseChange(CASE.selected);
                    break;
                default:
                    break;
            }
        }

        //[DllImport("User32.dll")]
        //private static extern bool SetCursorPos(int x, int y);

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
                    case CASE.polygon:
                        break;
                    case CASE.Panning:
                        selectedShape.Setdxdy(e.X - x0, e.Y - y0);
                        selectedShape.Draw();
                        break;
                    default:
                        break;
                }
            }
            if(NowCase==CASE.polygon && !pn.FinishDraw && pn.GetCount()>0)
            {
                pictureBox.Image = FrontImage.Clone() as Image;
                pn.AddPoint(e.X, e.Y);
                pn.Draw();
                pn.RemovePoint(e.X, e.Y);
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

            switch (NowCase)
            {
                case CASE.NoOperation:
                    break;
                case CASE.dot:
                    break;
                case CASE.line:
                    break;
                case CASE.roundness:
                    break;
                case CASE.ellipse:
                    break;
                case CASE.rectangle:
                    break;
                case CASE.pencil:
                    break;
                case CASE.fill:
                    break;
                case CASE.selected:
                    if (selectedShape != null)
                    {
                        if (selectedShape.PointInIt(x0, y0))
                            CaseChange(CASE.Panning);
                        else
                        {
                            selectedShape.unSelectShape();
                            //CaseChange(CASE.NoOperation);
                            selectedShape = null;
                        }                           
                    }
                    break;
                case CASE.polygon:
                    break;
                case CASE.Panning:
                    break;
                default:
                    break;
            }

        }
        
        private void RefreshPictureBox()
        {
            pictureBox.Image = (Image)Backgroud.Clone();
            OperaStep.DrawOperation();
        }

        
    }
}
