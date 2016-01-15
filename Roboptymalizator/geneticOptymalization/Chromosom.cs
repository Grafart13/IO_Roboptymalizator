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

        private int GenerateGenesFromTo(Tuple<int, int> from, Tuple<int, int> to, int ind)
        {
            //  zaczynamy generować step <ind>
            // idziemy from -> to w linii prostej poziomo lub pionowo
            if (from.Item1 == to.Item1)
            {
                // pionowo
                int steps = Math.Abs(from.Item2 - to.Item2);
                for (int i = 1; i<=steps; i++)
                {
                    if (from.Item2 < to.Item2)
                        genes[ind++]=new Gene(new Tuple<int, int> (from.Item1, from.Item2 + i));
                    else
                        genes[ind++] = new Gene(new Tuple<int, int>(from.Item1, from.Item2 - i));
                }
            }
            else
            {
                // poziomo
                int steps = Math.Abs(from.Item1 - to.Item1);
                for (int i = 1; i<=steps; i++)
                {
                    if (from.Item1 < to.Item1)
                        genes[ind++] = new Gene(new Tuple<int, int>(from.Item1 + i, from.Item2));
                    else
                        genes[ind++] = new Gene(new Tuple<int, int>(from.Item1 - i, from.Item2));
                }
            }
            return ind;
        }

        public void RandomGenes(Tuple<int, int> start, Tuple<int, int> stop, Tuple<int,int> sizeOfMap)
        {
            genes[0] = new Gene(start);
            genes[genes.Length - 1] = new Gene(stop);
            int ind = 1;

            int n = sizeOfMap.Item1;
            int m = sizeOfMap.Item2;
            
            int taxicMetric = Math.Abs(start.Item1 - stop.Item1) + Math.Abs(start.Item2 - stop.Item2);
            int diff = lenght - taxicMetric;
            if (diff <= 0)
            {
                genes = new Gene[taxicMetric];
                lenght = taxicMetric;

                //Tuple<int, int> pom = new Tuple<int, int>(start.Item1, stop.Item2);
                //int ind_pom = GenerateGenesFromTo(start, pom , ind);
                //ind = GenerateGenesFromTo(pom, stop, ind_pom);

                // a możeby tak wylosować punkt przez który ma przechodzić?
                Random rn = new Random();
                int x, y;
                x = rn.Next(Math.Min(start.Item1, stop.Item1), Math.Max(start.Item1, stop.Item1));
                y = rn.Next(Math.Min(start.Item2, stop.Item2), Math.Max(start.Item2, stop.Item2));

                Tuple<int, int> point = new Tuple<int, int>(x, y);
                int decission = rn.Next();
                Tuple<int, int> pom;
                if (decission % 2 == 0)
                    pom = new Tuple<int, int>(point.Item1, start.Item2);
                else
                    pom = new Tuple<int,int>(start.Item1, point.Item2);

                ind = GenerateGenesFromTo(start, pom, ind);
                ind = GenerateGenesFromTo(pom, point, ind);

                decission = rn.Next();
                if (decission % 2 == 0)
                    pom = new Tuple<int, int>(point.Item1, stop.Item2);
                else
                    pom = new Tuple<int, int>(stop.Item1, pom.Item2);
                ind = GenerateGenesFromTo(point, pom, ind);
                ind = GenerateGenesFromTo(pom, stop, ind);
            }
            else
            {
                int div = diff / 2; // kazde odchylenie generuje +2 ruchy -> div = ile odchyleń
                if ((start.Item1 < stop.Item1) && (start.Item1 - div > 0))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 - div, start.Item2);
                    ind = GenerateGenesFromTo(start, away, ind);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    ind = GenerateGenesFromTo(away, pom, ind);
                    ind = GenerateGenesFromTo(pom, stop, ind);
                }
                else if ((start.Item1 >= stop.Item1) && (start.Item1 + div < sizeOfMap.Item1))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 + div, start.Item2);
                    ind = GenerateGenesFromTo(start, away, ind);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    ind = GenerateGenesFromTo(away, pom, ind);
                    ind = GenerateGenesFromTo(pom, stop, ind);
                }
                else if ((start.Item2 < stop.Item2) && (start.Item2 - div > 0))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1, start.Item2 - div);
                    ind = GenerateGenesFromTo(start, away, ind);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    ind = GenerateGenesFromTo(away, pom, ind);
                    ind = GenerateGenesFromTo(pom, stop, ind);
                }
                else if ((start.Item2 >= stop.Item2) && (start.Item2 + div < sizeOfMap.Item2))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 + div, start.Item2);
                    ind = GenerateGenesFromTo(start, away, ind);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2 + div);
                    ind = GenerateGenesFromTo(away, pom, ind);
                    ind = GenerateGenesFromTo(pom, stop, ind);
                }
                else
                    System.Console.WriteLine("Not implemented full initial random generator");
            }
        }
    }
}
