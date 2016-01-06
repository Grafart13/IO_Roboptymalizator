using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class Chromosom
    {
        private Gene[] genes; // field number in each column
        public int lenght { get; set; } // lenght of gene
        public double? fitness { private get; set; }


        public Chromosom(int lenght)
        {
            this.lenght = lenght;
            this.genes = new Gene[lenght];
        }

        public int CompareTo(Chromosom a)
        {
            if (a == null)
                return -1;
            if (a.fitness == this.fitness)
                return 0;
            return this.fitness > a.fitness ? 1 : -1;
        }

        public static bool operator == (Chromosom a, Chromosom b)
        {
            if (Object.ReferenceEquals(a,b))
            {
                return true;
            }

            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }
            return a.CompareTo(b) == 0;
        }

        public static bool operator != (Chromosom a, Chromosom b)
        {
            return !(a == b);
        }

        public static bool operator <(Chromosom a, Chromosom b)
        {
            if ((Object.ReferenceEquals(a, b)) || ((object) b == null))
            {
                return false;
            }

            if ((object)a == null)
            {
                return true;
            }
            return a.CompareTo(b) > 0;
        }
        public static bool operator >(Chromosom a, Chromosom b)
        {
            return !(a < b) && (a != b);
        }

        public void ReplaceGene(int ind, Gene g)
        {
            if ((ind < 0) || (ind >= lenght))
                throw new ArgumentOutOfRangeException();
            genes[ind] = g;
            fitness = null;
        }

        public void ReplaceGenes(int indStart, int indStop, Gene [] g)
        {
            if ((indStart < 0) || (indStart >= lenght))
                throw new ArgumentOutOfRangeException();
            int last = (indStart + g.Length);
            if (last >= lenght)
                throw new ArgumentException("Too many genes to replace");
            Array.Copy(g, 0, genes, indStart, g.Length);
            fitness = null;
        }

        public Gene GetGene(int ind)
        {
            return genes[ind];
        }

        public Gene[] GetGenes()
        {
            return genes;
        }

        public double GetFitness()
        {
            //FitnessService.
            return 0.0;
        }
    }
}
