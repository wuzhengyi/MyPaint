using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void button_front_Click(object sender, EventArgs e)
        {
            //TODO:快进当前步骤
        }

        private void button_line_Click_1(object sender, EventArgs e)//直线
        {
            CaseChange(CASE.line);
        }

        private void button_dot_Click(object sender, EventArgs e)//点
        {
            CaseChange(CASE.dot);
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
            if (NowCase != CASE.chose)
            {
                NowCase = CASE.fill;
            }
            else
            {
                CaseChange(CASE.NoOperation);
                //在选择框内填充纯色
                FillColor(color, x1, y1, x2, y2);

            }
        }

        private void button_rectangle_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.rectangle);
        }

        private void button_choose_Click(object sender, EventArgs e)
        {
            CaseChange(CASE.choose);
        }

        private void button_fillpic_Click(object sender, EventArgs e)
        {
            if (NowCase != CASE.chose)
            {
                MessageBox.Show("请先选择填充范围！");
            }
            else
            {
                CaseChange(CASE.NoOperation);
                openFileDialog1.InitialDirectory = "D:\\";            // 这里是初始的路径名
                openFileDialog1.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";  //设置打开文件的类型
                openFileDialog1.RestoreDirectory = true;              //设置是否还原当前目录
                openFileDialog1.FilterIndex = 0;                      //设置打开文件类型的索引
                string path = "";                                     //用于保存打开文件的路径
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    //MessageBox.Show(path);                            //显示该路径名
                    Bitmap FilledPic = new Bitmap(path);
                    FillPic(FilledPic, x1, y1, x2, y2);
                }
            }
        }

        private void CaseChange(CASE temp)
        {
            if (NowCase == CASE.chose)
            {
                //pictureBox.Image = Step.RefreshStep();
                //TODO:初始化当前步骤
                ChoseSize.Hide();
            }
            NowCase = temp;
        }
    }
}
