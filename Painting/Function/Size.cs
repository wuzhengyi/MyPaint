using System.Drawing;
using System.Windows.Forms;

namespace Painting
{
    partial class Form1
    {
        private void PictureBoxSize_MouseDown(object sender, MouseEventArgs e)
        {
            CaseChange(CASE.NoOperation);
            Image temp = (Image)pictureBox.Image.Clone();
            //Step.AddStep(temp);
            //TODO:存储当前步骤
            p = e.Location;
        }

        private void PictureBoxSize_MouseUp(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }

        /*private void PictureBoxSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.pictureBoxSize.Location = new Point(this.pictureBoxSize.Left + (e.X - p.X), this.pictureBoxSize.Top + (e.Y - p.Y));
                //TODO:change size
            RefreshPicutreBoxSize();
        }*/

        private void ChoseSize_MouseUp(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }

        /*private void ChoseSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (NowCase == CASE.chose)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    //this.ChoseSize.Location = new Point(this.ChoseSize.Left + (e.X - p.X), this.ChoseSize.Top + (e.Y - p.Y));
                    ;//TODO:change size
                RefreshChoseSize();
            }

        }*/

        private void ChoseSize_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;

        }

        private void RefreshChoseSize()
        {
            //pictureBox.Image = Step.RefreshStep();
            //TODO:更新当前步骤
            FillColor(color, x1, y1, x2, y2);
            //x2 = ChoseSize.Location.X;
            //y2 = ChoseSize.Location.Y;
            ;//TODO:change location
           // FillPic(ImageOperation.SetBitmapSize(ChoseRegion, x2 - x1, y2 - y1), x1, y1, x2, y2);
            //DrawRectangle(x1, y1, x2, y2);

        }

        /*private void RefreshPicutreBoxSize()
        {
            Bitmap NewImage = new Bitmap(pictureBoxSize.Location.X, pictureBoxSize.Location.Y);
            Graphics g = Graphics.FromImage(NewImage);
            g.Clear(color);
            //绘制原图   
            g.DrawImage(pictureBox.Image, 0, 0);
            g.TranslateTransform(pictureBox.Image.Width, 0);
            pictureBox.Image = NewImage;
        }*/
    }
}
