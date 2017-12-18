using System;
using System.Collections;
using System.Windows.Forms;

namespace Painting
{
    class OperationStep : ArrayList
    {
        private int StepImage_now;
        public OperationStep()
        {
            InitStep();
        }
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

        public void BackStep()
        {
            if (StepIsFirst())
            {
                MessageBox.Show("ERROR: All Clear!");
            }
            else
            {
                StepImage_now--;
            }

        }

        public bool StepIsLast()
        {
            return StepImage_now == this.Count - 1;
        }

        public void NextStep()
        {
            if (StepIsLast())
            {
                MessageBox.Show("ERROR: This Step Is Last One");
            }
            else
                StepImage_now++;
        }

        public void RemoveNullStep()
        {
            this.RemoveRange(StepImage_now + 1, this.Count - StepImage_now - 1 );
        }

        public void DrawOperation()
        {
            for(int i = 0; i <= StepImage_now; i++)
            {
                ((Shape)this[i]).Draw();
                
            }
        }
    }

}
