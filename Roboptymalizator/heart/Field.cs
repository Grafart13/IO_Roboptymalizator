using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.heart
{
    class Field
    {
        private double height;
        private bool isStart;
        private bool isStop;

        public Field(double _height)
        {
            this.height = _height;
            this.isStart = this.isStop = false;
        }

        public void SetStart()
        {
            this.isStart = true;
        }

        public bool IsStart()
        {
            return this.isStart;
        }

        public bool IsStop()
        {
            return this.isStop;
        }
        public void SetStop()
        {
            this.isStop = true;
        }
        public double GetHeight()
        {
            return this.height;
        }

        public void SetHeight(double _h)
        {
            this.height = _h;
        }
    }
}
