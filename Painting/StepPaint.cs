using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting
{
    class StepPaint: ArrayList
    {
        private int StepImage_now;
        public void ClearStep()
        {
            this.Clear();
            StepImage_now = -1;
        }
        public void AddStep(Object obj)
        {
            this.Add(obj);
            StepImage_now++;
            //((Image)obj).Save("E:\\1.png");
        }

        public void InitStep(Image obj)
        {
            ClearStep();
            //AddStep(obj);
        }

        public bool StepIsNull()
        {
            return StepImage_now == 0;
        }
        public Image PopStep()
        {
            if (StepIsNull())
                MessageBox.Show("StepImage_now == 0!");
            else
            {
                StepImage_now--;    
            }
            return (Image)(((Image)this[StepImage_now]).Clone());

        }

        public bool StepIsFull()
        {
            return StepImage_now == this.Count - 1 ;
        }

        public Image PushStep()
        {
            if (StepIsFull())
                MessageBox.Show("StepImage_now is full");
            StepImage_now++;
            return (Image)(((Image)this[StepImage_now]).Clone());
        }

        public Image RefreshStep()
        {
            Image temp = (Image)((Image)this[StepImage_now]).Clone();
            //this[StepImage_now] = temp;
            return temp;
        }

        public void RemoveNullStep()
        {
            //MessageBox.Show("set count to now!");
            this.RemoveRange(StepImage_now + 1, this.Count - StepImage_now - 1);
        }

        //public void ShowImageStep(int i)
        //{
        //    Form fm = new Form();
        //    fm.BackgroundImage = (Image)this[i];
        //    fm.Show();
        //}
    }
}
