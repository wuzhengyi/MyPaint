﻿using System;
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
        private void PictureBoxSize_MouseDown(object sender, MouseEventArgs e)
        {
            CaseChange(CASE.NoOperation);
            Image temp = (Image)pictureBox.Image.Clone();
            Step.AddStep(temp);
            p = e.Location;
        }

        private void PictureBoxSize_MouseUp(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }

        private void PictureBoxSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.pictureBoxSize.Location = new Point(this.pictureBoxSize.Left + (e.X - p.X), this.pictureBoxSize.Top + (e.Y - p.Y));
            RefreshPicutreBoxSize();
        }

        private void ChoseSize_MouseUp(object sender, MouseEventArgs e)
        {
            p = e.Location;
            pictureBox.Image = Step.RefreshStep();
            FillColor(color, x1, y1, x2, y2);
            x2 = ChoseSize.Location.X;
            y2 = ChoseSize.Location.Y;
            FillPic(SetBitmapSize(ChoseRegion, x2 - x1, y2 - y1), x1, y1, x2, y2);
            ChoseSize.Hide();
        }

        private void ChoseSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (NowCase == CASE.chose)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    this.ChoseSize.Location = new Point(this.ChoseSize.Left + (e.X - p.X), this.ChoseSize.Top + (e.Y - p.Y));
                RefreshChoseSize();
            }

        }

        private void ChoseSize_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;

        }

        private void RefreshChoseSize()
        {
            pictureBox.Image = Step.RefreshStep();
            FillColor(color, x1, y1, x2, y2);
            x2 = ChoseSize.Location.X;
            y2 = ChoseSize.Location.Y;
            FillPic(SetBitmapSize(ChoseRegion, x2 - x1, y2 - y1), x1, y1, x2, y2);
            DrawRectangle(x1, y1, x2, y2);

        }

        private void RefreshPicutreBoxSize()
        {
            Bitmap NewImage = new Bitmap(pictureBoxSize.Location.X, pictureBoxSize.Location.Y);
            Graphics g = Graphics.FromImage(NewImage);
            g.Clear(color);
            //绘制原图   
            g.DrawImage(pictureBox.Image, 0, 0);
            g.TranslateTransform(pictureBox.Image.Width, 0);
            pictureBox.Image = NewImage;
        }

        private bool PointInRectangle(int x, int y)
        {
            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                return true;
            else
                return false;
        }

        private void PanningChoseRegion(int dx, int dy)
        {
            FillColor(color, x1, y1, x2, y2);
            if (x1 + dx > 0 && x2 + dx < pictureBox.Width && y1 + dy > 0 && y2 + dy < pictureBox.Height)
            {
                FillPic(ChoseRegion, x1 + dx, y1 + dy, x2 + dx, y2 + dy);
                if (IsMouseDown)
                    DrawRectangle(x1 + dx, y1 + dy, x2 + dx - 1, y2 + dy - 1);
            }

        }
    }
}