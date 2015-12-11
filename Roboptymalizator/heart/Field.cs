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
        private Move[] moves;

        public Field(double _height)
        {
            this.height = _height;
            this.isStart = this.isStop = false;
            this.moves = new Move[4]; // or 8 if we set up movement diagonally
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

        public void AddMove(int where, Move move)
        {
            if ((where < 4) && (where > -1))
                moves[where] = move;
        }
    }
}
