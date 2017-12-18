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
    class OperationStep : ArrayList
    {
        private int StepImage_now;

        public void AddStep(Object obj)
        {
            this.Add(obj);
            StepImage_now++;
        }

        public void InitStep()
        {
            this.Clear();
            StepImage_now = -1;
        }

        public bool StepIsFirst()
        {
            return StepImage_now == -1;
        }

        public Shape BackStep()
        {
            if (StepIsFirst())
            {
                MessageBox.Show("StepImage_now == 0!");
                return null;
            }
            else
            {
                return (Shape)this[StepImage_now--];
            }

        }

        public bool StepIsLast()
        {
            return StepImage_now == this.Count - 1;
        }

        public Shape NextStep()
        {
            if (StepIsLast())
            {
                MessageBox.Show("StepImage_now is Last one");
                return null;
            }
            else
                return (Shape)this[StepImage_now++];
        }

        public void RemoveNullStep()
        {
            this.RemoveRange(StepImage_now + 1, this.Count - StepImage_now - 1);
        }

        public void DrawOperation()
        {
            for(int i = 0; i < StepImage_now; i++)
            {
                ((Shape)this[i]).Draw();
            }
        }
    }

}
