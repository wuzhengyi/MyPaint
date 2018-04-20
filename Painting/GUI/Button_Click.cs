using System;
using System.Drawing;
using System.Windows.Forms;
using Painting;
using Painting.Shapes;

namespace Painting
{
    partial class Form1
    {
        private void button_white_Click(object sender, EventArgs e)
        {
            color = Color.White;
            button_color.BackColor = color;
        }

        private void button_black_Click(object sender, EventArgs e)
        {
            color = Color.Black;
            button_color.BackColor = color;
        }

        private void button_red_Click(object sender, EventArgs e)
        {
            color = Color.Red;
            button_color.BackColor = color;
        }

        private void button_orange_Click(object sender, EventArgs e)
        {
            color = Color.Orange;
            button_color.BackColor = color;
        }

        private void button_yellow_Click(object sender, EventArgs e)
        {
            color = Color.Yellow;
            button_color.BackColor = color;
        }

        private void button_lime_Click(object sender, EventArgs e)
        {
            color = Color.Lime;
            button_color.BackColor = color;
        }

        private void button_aqua_Click(object sender, EventArgs e)
        {
            color = Color.Aqua;
            button_color.BackColor = color;
        }

        private void button_blue_Click(object sender, EventArgs e)
        {
            color = Color.Blue;
            button_color.BackColor = color;
        }

        private void button_fuchsia_Click(object sender, EventArgs e)
        {
            color = Color.Fuchsia;
            button_color.BackColor = color;
        }

        private void button_silver_Click(object sender, EventArgs e)
        {
            color = Color.Silver;
            button_color.BackColor = color;
        }

        private void 好细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.ss;
        }

        private void 细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.s;
        }


        private void 粗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.b;
        }

        private void 好粗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bh = BREATH.bb;
        }
        private void openfile_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
            openFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
            openFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
            openFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
            string path = "";                                     //用于保存打开文件的路径
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                //MessageBox.Show(path);                            //显示该路径名
                
                pictureBox.ImageLocation = path;
                Backgroud = Image.FromFile(path);
                //Backgroud = (Image)(pictureBox.Image).Clone();
                //Step.InitStep(Image.FromFile(path));
                //TODO:初始化当前步骤
            }


        }

        private void savefile_Click(object sender, EventArgs e)
        {

            saveFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
            saveFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
            saveFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
            saveFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
            string path = "";                                     //用于保存打开文件的路径
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                //MessageBox.Show(path);                            //显示该路径名
                pictureBox.Image.Save(path);
            }

        }

        private void button_back_Click(object sender, EventArgs e)
        {
            //TODO:撤销当前步骤
            OperaStep.BackStep();
            RefreshPictureBox();
            IsBack=true;
        }

        private void button_front_Click(object sender, EventArgs e)
        {
            //TODO:快进当前步骤
            OperaStep.NextStep();
            RefreshPictureBox();
        }

        private void button_line_Click_1(object sender, EventArgs e)//直线
        {
            CaseChange(CASE.line);
        }

        private void button_dot_Click(object sender, EventArgs e)//点
        {
            CaseChange(CASE.Bezier);
        }

        private void button_pencil_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.pencil);
        }

        private void button_ellipse_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.ellipse);
        }

        private void button_roundness_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.roundness);
        }

        private void button_fill_Click(object sender, EventArgs e)
        {
            if (selectedShape != null) 
            {
                //CaseChange(CASE.NoOperation);
                //在选择框内填充纯色
                //FillColor(color, x1, y1, x2, y2);                
                selectedShape.FillColor(color);
            }
        }

        private void button_rectangle_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.rectangle);
        }

        private void button_polygon_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.polygon)
            {
                CaseChange(CASE.NoOperation);
            }
            else
            {
                CaseChange(CASE.polygon);
                pn = new Polygon();
                pn.InitPolygon(pictureBox, color);
                //pn.FinishDraw = false;
            }             
        }

        private void bezier_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.Bezier)
            {
                CaseChange(CASE.NoOperation);
            }
            else
            {
                CaseChange(CASE.Bezier);
                bz = new Bezier();
                bz.InitBezier(pictureBox, color);                
            }
        }

        private void Bsplines_Click(object sender, EventArgs e)
        {
            if (NowCase == CASE.Bsplines)
            {
                CaseChange(CASE.NoOperation);
            }
            else
            {
                CaseChange(CASE.Bsplines);
                bs = new Bsplines();
                bs.InitBsplines(pictureBox, color);
            }
        }

        private void button_selected_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.selected);
        }

        private void spin_Click(object sender, EventArgs e)
        {
            //Math.Sin(0.261799)
            if (selectedShape != null)
            {
                selectedShape.SetAngle(0.2);
                RefreshPictureBox();
            }
                
        }

        private void button_clip_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.clip);
        }

        private void CaseChange(CASE temp)
        {
            if (NowCase == CASE.polygon && temp!=CASE.polygon)
            {
                pn.FinishDraw = true;
                OperaStep.AddStep(pn);
                RefreshPictureBox();
                pn = null;
            }
            else if(NowCase == CASE.Bezier && temp != CASE.Bezier)
            {
                bz.FinishDraw = true;
                OperaStep.AddStep(bz);
                RefreshPictureBox();
                bz = null;
            }
            else if (NowCase == CASE.Bsplines && temp != CASE.Bsplines)
            {
                bs.FinishDraw = true;
                OperaStep.AddStep(bs);
                RefreshPictureBox();
                bs = null;
            }
            NowCase = temp;
        }

        public void SelectedShapeSize_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }
        public void SelectedShapeSize_MouseUp(object sender, MouseEventArgs e)
        {
            p = e.Location;
            if (selectedShape.GetShapeType() == ShapeType.polygon)
            {
                selectedShape.UpdateLocation();
                RefreshPictureBox();
            }
                
        }
        public void SelectedShapeSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (NowCase == CASE.selected)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ((Button)sender).Location = new Point(((Button)sender).Left + (e.X - p.X), ((Button)sender).Top + (e.Y - p.Y));
                    if(selectedShape.GetShapeType()!=ShapeType.polygon)
                        selectedShape.UpdateLocation();
                    RefreshPictureBox();
                }
            }
        }
    }
}
