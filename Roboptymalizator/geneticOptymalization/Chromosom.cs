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
        public double fitness {  get; set; }


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
          //  fitness = null;
        }

        public void ReplaceGenes(int indStart, int indStop, Gene [] g)
        {
            if ((indStart < 0) || (indStart >= lenght))
                throw new ArgumentOutOfRangeException();
            int last = (indStart + g.Length);
            if (last >= lenght)
                throw new ArgumentException("Too many genes to replace");
            Array.Copy(g, 0, genes, indStart, g.Length);
           // fitness = null;
        }

        public Gene GetGene(int ind)
        {
            return genes[ind];
        }

        public Gene[] GetGenes()
        {
            return genes;
        }

        private int GenerateGenesFromTo(Tuple<int, int> from, Tuple<int, int> to, int ind, ref Gene [] genesTab)
        {
            //  zaczynamy generować step <ind>
            // idziemy from -> to w linii prostej poziomo lub pionowo
            System.Console.WriteLine(ind);
            if (from.Item1 == to.Item1)
            {
                // pionowo
                int steps = Math.Abs(from.Item2 - to.Item2);
                for (int i = 1; i<=steps; i++)
                {
                    if (from.Item2 < to.Item2)
                        genesTab[ind++]=new Gene(new Tuple<int, int> (from.Item1, from.Item2 + i));
                    else
                        genesTab[ind++] = new Gene(new Tuple<int, int>(from.Item1, from.Item2 - i));
                }
            }
            else
            {
                // poziomo
                int steps = Math.Abs(from.Item1 - to.Item1);
                for (int i = 1; i<=steps; i++)
                {
                    if (from.Item1 < to.Item1)
                        genesTab[ind++] = new Gene(new Tuple<int, int>(from.Item1 + i, from.Item2));
                    else
                        genesTab[ind++] = new Gene(new Tuple<int, int>(from.Item1 - i, from.Item2));
                }
            }
            return ind;
        }

        private void GenerateStep(Tuple<int, int> from, Tuple<int, int> to, ref int ind, ref Gene[] genes)
        {
            if (from.Item1 == to.Item1)
            {
                int ind_pom = GenerateGenesFromTo(from, to, ind, ref genes);
                ind = ind_pom;
            }
            else if (from.Item2 == to.Item2)
            {
                int ind_pom = GenerateGenesFromTo(from, to, ind, ref genes);
                ind = ind_pom;
            }
            else
            {
                Random rn = new Random();

                Tuple<int, int> pom;
                
                int decission = rn.Next(); // czy najpierw poziomo, czy najpierw pionowo
                if (decission % 2 == 0)
                    // najpierw pionowo
                    pom = new Tuple<int, int>(from.Item1, to.Item2);
                else
                    // najpierw poziomo
                    pom = new Tuple<int, int>(to.Item1, from.Item2);

                int ind_pom = GenerateGenesFromTo(from, pom, ind, ref genes);
                ind = GenerateGenesFromTo(pom, to, ind_pom, ref genes);
            }
        }
        public void RandomGenes(Tuple<int, int> start, Tuple<int, int> stop, Tuple<int,int> sizeOfMap)
        {
            
            int ind = 1;
            int ind_pom;

            int n = sizeOfMap.Item1;
            int m = sizeOfMap.Item2;
            
            int taxicMetric = Math.Abs(start.Item1 - stop.Item1) + Math.Abs(start.Item2 - stop.Item2);
            int diff = lenght - taxicMetric;
            if (diff <= 0)
            {
                genes = new Gene[taxicMetric+1];
                
                genes[0] = new Gene(start);
                genes[genes.Length - 1] = new Gene(stop);

                lenght = taxicMetric;

                //Tuple<int, int> pom = new Tuple<int, int>(start.Item1, stop.Item2);
                //int ind_pom = GenerateGenesFromTo(start, pom , ind);
                //ind = GenerateGenesFromTo(pom, stop, ind_pom);

                // losowanie punktu przez który będziemy przechodzić
                Random rn = new Random();
                int x, y;
                x = rn.Next(Math.Min(start.Item1, stop.Item1), Math.Max(start.Item1, stop.Item1));
                y = rn.Next(Math.Min(start.Item2, stop.Item2), Math.Max(start.Item2, stop.Item2));

                Tuple<int, int> point = new Tuple<int, int>(x, y);

                GenerateStep(start, point, ref ind, ref genes);
                GenerateStep(point, stop, ref ind, ref genes);

                //if (point.Item1 == start.Item1)
                //{
                //    point = new Tuple<int, int>(start.Item1, point.Item2);
                //    ind_pom = GenerateGenesFromTo(start, point, ind, ref this.genes);
                //    ind = GenerateGenesFromTo(point, stop, ind_pom, ref this.genes);
                //}
                //else if (point.Item2 == start.Item2)
                //{
                //    point = new Tuple<int, int>(point.Item1, start.Item2);
                //    ind_pom = GenerateGenesFromTo(start, point, ind, ref this.genes);
                //    ind = GenerateGenesFromTo(point, stop, ind_pom, ref this.genes);
                //}
                
                //else
                //{
                //    int decission = rn.Next();
                //    Tuple<int, int> pom;
                //    if (decission % 2 == 0)
                //        pom = new Tuple<int, int>(point.Item1, start.Item2);
                //    else
                //        pom = new Tuple<int, int>(start.Item1, point.Item2);

                //    ind_pom = GenerateGenesFromTo(start, pom, ind, ref this.genes);
                //    ind = GenerateGenesFromTo(pom, point, ind_pom, ref this.genes);

                //    decission = rn.Next();
                //    if (decission % 2 == 0)
                //        pom = new Tuple<int, int>(point.Item1, stop.Item2);
                //    else
                //        pom = new Tuple<int, int>(stop.Item1, pom.Item2);
                //    ind_pom = GenerateGenesFromTo(point, pom, ind, ref this.genes);
                //    ind = GenerateGenesFromTo(pom, stop, ind_pom, ref this.genes);
                //}
                
            }
            else
            {
                int div = diff / 2; // kazde odchylenie generuje +2 ruchy -> div = ile odchyleń
                if ((start.Item1 < stop.Item1) && (start.Item1 - div > 0))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 - div, start.Item2);
                    ind = GenerateGenesFromTo(start, away, ind, ref this.genes);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    ind = GenerateGenesFromTo(away, pom, ind, ref this.genes);
                    ind = GenerateGenesFromTo(pom, stop, ind, ref this.genes);
                }
                else if ((start.Item1 >= stop.Item1) && (start.Item1 + div < sizeOfMap.Item1))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 + div, start.Item2);
                    ind = GenerateGenesFromTo(start, away, ind, ref this.genes);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    ind = GenerateGenesFromTo(away, pom, ind, ref this.genes);
                    ind = GenerateGenesFromTo(pom, stop, ind, ref this.genes);
                }
                else if ((start.Item2 < stop.Item2) && (start.Item2 - div > 0))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1, start.Item2 - div);
                    ind = GenerateGenesFromTo(start, away, ind, ref this.genes);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    ind = GenerateGenesFromTo(away, pom, ind, ref this.genes);
                    ind = GenerateGenesFromTo(pom, stop, ind, ref this.genes);
                }
                else if ((start.Item2 >= stop.Item2) && (start.Item2 + div < sizeOfMap.Item2))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 + div, start.Item2);
                    ind = GenerateGenesFromTo(start, away, ind, ref this.genes);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2 + div);
                    ind = GenerateGenesFromTo(away, pom, ind, ref this.genes);
                    ind = GenerateGenesFromTo(pom, stop, ind, ref this.genes);
                }
                else
                    System.Console.WriteLine("Not implemented full initial random generator");
            }
        }

        public void clone(int from, int to, Gene[] newGenes, int ind)
        {
            for (int i = from; i < to; i++)
            {
                newGenes[ind++] = this.genes[i];
            }
        }
        private int TryMutationCorner(Tuple<int, int> a, ref Tuple<int, int> b, Tuple<int, int> c)
        {
            int success = 0;

            if ((a.Item1 == b.Item1 + 1) && (a.Item2 == b.Item2 - 1))
            {
                if (a.Item1 == c.Item1)
                {
                    // zamieniamy na prawo-dół
                    c = new Tuple<int, int>(c.Item1 + 1, c.Item2 + 1);
                }
                else
                {
                    // zamieniamy na lewo-góra
                    c = new Tuple<int, int>(c.Item1 - 1, c.Item2 - 1);
                }

            }
            else if ((a.Item1 == b.Item1 - 1) && (a.Item2 == b.Item2 - 1))
            {
                if (a.Item1 == c.Item1)
                {
                    // zamieniamy na prawo-góra
                    c = new Tuple<int, int>(c.Item1 + 1, c.Item2 - 1);
                }
                else
                {
                    // zamieniamy na lewo - dół
                    c = new Tuple<int, int>(c.Item1 - 1, c.Item2 + 1);
                }
            }
            else
                success = -1;
            return success;
        }
        public void Mutation()
        {
            Random rn = new Random();

            int ind = rn.Next(1, lenght-2);

            Tuple<int, int> a = genes[ind-1].value;
            Tuple<int, int> b = genes[ind+1].value;
            Tuple<int, int> c = genes[ind].value;

            if (TryMutationCorner(a,ref c,b) == 0)
            {
                Gene[] newGenes = new Gene[lenght];

                clone(0, ind - 1, newGenes, 0);
                newGenes[ind] = new Gene(c);
                clone(ind + 1, lenght, newGenes, ind + 1);
            }
            // w całkiem przeciwnym razie nie mutujemy

        }

        public void MutationWithLenghtChanging()
        {
            Random rn = new Random();
            int ind = rn.Next(1, lenght-2);

            Tuple<int, int> a = genes[ind - 1].value;
            Tuple<int, int> b = genes[ind].value; // ten gen będziemy zmieniać
            Tuple<int, int> c = genes[ind + 1].value;

            if (TryMutationCorner(a, ref b, c) == 0)
            {
                Gene[] newGenes = new Gene[lenght];

                clone(0, ind - 1, newGenes, 0);
                newGenes[ind] = new Gene(c);
                clone(ind + 1, lenght, newGenes, ind + 1);
            }
            else
            {
                Gene[] firstGenes = new Gene[lenght];
                clone(0, ind - 1, firstGenes, 0);
                clone(ind + 1, lenght, firstGenes, ind + 1);

                Tuple<int, int> newB;
                // przypadek pierwszy - idziemy pionowo
                if (a.Item1 == c.Item1)
                {
                    int decission = rn.Next();
                    if (decission % 2 == 0)
                        // idziemy w lewo
                        newB = new Tuple<int, int>(b.Item1 - 1, b.Item2);
                    else
                        // idziemy w prawo
                        newB = new Tuple<int, int>(b.Item1 + 1, b.Item2);

                    lenght += 2;

                    if ((newB.Item1 < 0) || (newB.Item1 >= lenght))
                    {
                        lenght = lenght - 2;
                        newB = b;
                    }

                    Gene[] newGenes = new Gene[lenght];
                    clone(0, ind - 1, newGenes, ind);
                }
                else
                {

                }
            }
        }
        public Chromosom Cross(Chromosom ch, Tuple<int, int> sizeOfMap)
        {
            Chromosom son = new Chromosom(lenght);
            
            Random rn = new Random();

            int ind = rn.Next(0, lenght / 2);
            int value = rn.Next(Math.Min(genes[ind].value.Item2, ch.genes[ind].value.Item2), Math.Max(genes[ind].value.Item2, ch.genes[ind].value.Item2));
            
            Gene[] newGenes = new Gene[lenght];
            clone(0, 1, newGenes, 0);
            clone(lenght - 1, lenght, newGenes, lenght - 1);
            Tuple <int, int>  pom = new Tuple<int, int>(genes[ind].value.Item1, value);
            int ind_pom = GenerateGenesFromTo(newGenes[0].value,pom  , 1, ref ch.genes);

            ind = rn.Next(lenght / 2, lenght-1);
            value = rn.Next(Math.Min(genes[ind].value.Item2, ch.genes[ind].value.Item2), Math.Max(genes[ind].value.Item2, ch.genes[ind].value.Item2));
            GenerateGenesFromTo(pom, newGenes[lenght-1].value, ind_pom, ref ch.genes);
            
            return ch;
        }
        public void SetFitness(double fit)
        {
            fitness = fit;
        }

        public String ToString()
        {
            String a = "";

            for (int i = 0; i<lenght; i++)
            {
                a += genes[i].value.Item1 + " : " + genes[i].value.Item2 + " | ";
                a += "\n fit: " + fitness;
            }

            return a;
        }
    }
}
