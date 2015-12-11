using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.heart
{
    class Move
    {
        private double dist;
        private double tanAlfa;
        private bool isUp;
        public Move(double dist, double tanAlfa)
        {
            this.dist = dist;
            this.tanAlfa = tanAlfa;
        }

        public Move(Field fromField, Field toField, double x) // x is the width of the grid
        {
            this.dist = ComputeDist(fromField.GetHeight(), toField.GetHeight(), x);
            this.tanAlfa = ComputeAlfa(fromField.GetHeight(), toField.GetHeight(), x);
        }

        private double ComputeDist(double h1, double h2, double x)
        {
            double heigth = Math.Abs(h2 - h1);
            double dist = Math.Pow(Math.Pow(heigth, 2) + Math.Pow(x, 2), 0.5); // Pitagoras
            return dist;
        }

        private double ComputeAlfa(double h1, double h2, double x)
        {
            double height = Math.Abs(h2 - h1);
            double alfa = height / x; // in point of fact we need only known of tan (alfa)
            if (h2 - h1 > 0)
                // move up
                this.isUp = true;
            else
                // move down
                this.isUp = false;
             return alfa;
        }

        public bool IsUp()
        {
            return this.isUp;
        }

        public double GetDist()
        {
            return this.dist;
        }

        public double GetAlfa()
        {
            return this.tanAlfa;
        }
    }
}
