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

        public Field(double _height)
        {
            this.height = _height;
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
