using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class Chromosom
    {
       // private Gene[] genes; // field number in each column
        private List<Gene> genesList;
        private int numOfGenes;
//        public int lenght { get; set; } // lenght of gene
        public double fitness {  get; set; }


        public Chromosom()
        {
  //          this.lenght = lenght;
            //this.genes = new Gene[lenght];
            this.genesList = new List<Gene>();
        }

        public Chromosom(int numOfGenes)
        {
            this.numOfGenes = numOfGenes;
            this.genesList = new List<Gene>();
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

        /*
        public void ReplaceGene(int ind, Gene g)
        {
            if ((ind < 0) || (ind >= lenght))
                throw new ArgumentOutOfRangeException();
            genes[ind] = g;

            fitness = null;
        }
        */

        public void ReplaceGenes(int indStart, int indStop, List<Gene> g)
        {
            if ((indStart < 0) || (indStart >= genesList.ToArray().Length))
                throw new ArgumentOutOfRangeException();
            int last = (indStart + g.ToArray().Length);
            if (last >= genesList.ToArray().Length)
                throw new ArgumentException("Too many genes to replace");
         //   Array.Copy(g, 0, genes, indStart, g.Length);
           // fitness = null;
        }

        public Gene GetGene(int ind)
        {
            return genesList[ind];
        }

        public List<Gene> GetGenes()
        {
            return genesList;
        }
        public List<Gene> GetGenesList()
        {
            return genesList;
        }
        
        /*
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
        */

        private void GenerateGenesFromTo(Tuple<int, int> from, Tuple<int, int> to, ref List<Gene> genesAim)
        {
            // idziemy from -> to w linii prostej poziomo lub pionowo
            
            if (from.Item1 == to.Item1)
            {
                // pionowo
                int steps = Math.Abs(from.Item2 - to.Item2);
                for (int i = 1; i <= steps; i++)
                {
                    if (from.Item2 < to.Item2)
                        genesAim.Add(new Gene(new Tuple<int, int>(from.Item1, from.Item2 + i)));
                    else
                        genesAim.Add(new Gene(new Tuple<int, int>(from.Item1, from.Item2 - i)));
                }
            }
            else
            {
                // poziomo
                int steps = Math.Abs(from.Item1 - to.Item1);
                for (int i = 1; i <= steps; i++)
                {
                    if (from.Item1 < to.Item1)
                        genesAim.Add(new Gene(new Tuple<int, int>(from.Item1 + i, from.Item2)));
                    else
                        genesAim.Add(new Gene(new Tuple<int, int>(from.Item1 - i, from.Item2)));
                }
            }
        }

        private void GenerateStep(Tuple<int, int> from, Tuple<int, int> to, ref List<Gene> genesAim)
        {
            if (from.Item1 == to.Item1)
            {
                GenerateGenesFromTo(from, to, ref genesAim);
            }
            else if (from.Item2 == to.Item2)
            {
                GenerateGenesFromTo(from, to, ref genesAim);
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

                GenerateGenesFromTo(from, pom, ref genesAim);
                GenerateGenesFromTo(pom, to, ref genesAim);
            }
        }
        public void RandomGenes(Tuple<int, int> start, Tuple<int, int> stop, Tuple<int,int> sizeOfMap)
        {
            //int lenght = genesList.ToArray().Length;
            int ind = 1;
            int ind_pom;

            int n = sizeOfMap.Item1;
            int m = sizeOfMap.Item2;
            
            int taxicMetric = Math.Abs(start.Item1 - stop.Item1) + Math.Abs(start.Item2 - stop.Item2);
            int diff = numOfGenes - taxicMetric;
            if (diff <= 0)
            {
               // genes = new Gene[taxicMetric+1];

                genesList.Add(new Gene(start));

                // losowanie punktu przez który będziemy przechodzić
                Random rn = new Random();
                int x, y;
                x = rn.Next(Math.Min(start.Item1, stop.Item1), Math.Max(start.Item1, stop.Item1));
                y = rn.Next(Math.Min(start.Item2, stop.Item2), Math.Max(start.Item2, stop.Item2));

                Tuple<int, int> point = new Tuple<int, int>(x, y);

                GenerateStep(start, point, ref genesList);
                GenerateStep(point, stop, ref genesList);
              //  genesList.Add(new Gene(stop));
            }
            else
            {
                genesList.Add(new Gene(start));

                int div = diff / 2; // kazde odchylenie generuje +2 ruchy -> div = ile odchyleń
                if ((start.Item1 < stop.Item1) && (start.Item1 - div > 0))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 - div, start.Item2);
                    GenerateGenesFromTo(start, away, ref this.genesList);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    GenerateGenesFromTo(away, pom, ref this.genesList);
                    GenerateGenesFromTo(pom, stop, ref this.genesList);
                }
                else if ((start.Item1 >= stop.Item1) && (start.Item1 + div < sizeOfMap.Item1))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 + div, start.Item2);
                    GenerateGenesFromTo(start, away, ref this.genesList);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    GenerateGenesFromTo(away, pom, ref this.genesList);
                    GenerateGenesFromTo(pom, stop, ref this.genesList);
                }
                else if ((start.Item2 < stop.Item2) && (start.Item2 - div > 0))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1, start.Item2 - div);
                    GenerateGenesFromTo(start, away, ref this.genesList);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2);
                    GenerateGenesFromTo(away, pom, ref this.genesList);
                    GenerateGenesFromTo(pom, stop, ref this.genesList);
                }
                else if ((start.Item2 >= stop.Item2) && (start.Item2 + div < sizeOfMap.Item2))
                {
                    Tuple<int, int> away = new Tuple<int, int>(start.Item1 + div, start.Item2);
                    GenerateGenesFromTo(start, away, ref this.genesList);
                    Tuple<int, int> pom = new Tuple<int, int>(away.Item1, stop.Item2 + div);
                    GenerateGenesFromTo(away, pom, ref this.genesList);
                    GenerateGenesFromTo(pom, stop, ref this.genesList);
                }
                else
                    System.Console.WriteLine("Not implemented full initial random generator");
                
                //genesList.Add(new Gene(stop));
            }
        }

        public void Clone(int from, int to, List<Gene> newGenes)
        {
            for (int i = from; i<to; i++)
            {
                newGenes.Add(this.genesList[i]);
            }
        }
        private int TryMutationCorner(Tuple<int, int> a, ref Tuple<int, int> b, Tuple<int, int> c)
        {
            int success = 1;

            if ((a.Item1 == c.Item1 + 1) && (a.Item2 == c.Item2 - 1))
            {
                if (a.Item1 == b.Item1)
                {
                    // zamieniamy na prawo-dół
                    b = new Tuple<int, int>(b.Item1 + 1, b.Item2 + 1);
                }
                else
                {
                    // zamieniamy na lewo-góra
                    b = new Tuple<int, int>(b.Item1 - 1, b.Item2 - 1);
                }

            }
            else if ((a.Item1 == c.Item1 - 1) && (a.Item2 == c.Item2 - 1))
            {
                if (a.Item1 == b.Item1)
                {
                    // zamieniamy na prawo-góra
                    b = new Tuple<int, int>(b.Item1 + 1, b.Item2 - 1);
                }
                else
                {
                    // zamieniamy na lewo - dół
                    b = new Tuple<int, int>(b.Item1 - 1, b.Item2 + 1);
                }
            }
            else
                success = -1;
            return success;
        }
        public void Mutation()
        {
            Random rn = new Random();

            int lenght = genesList.ToArray().Length;
            int ind = rn.Next(1, lenght-2);

            Tuple<int, int> a = genesList[ind-1].value;
            Tuple<int, int> b = genesList[ind+1].value;
            Tuple<int, int> c = genesList[ind].value;

            if (TryMutationCorner(a,ref c,b) == 0)
            {
                List<Gene> newGenesList = new List<Gene>();

                Clone(0, ind - 1, newGenesList);
                newGenesList.Add(new Gene(c));
                Clone(ind + 1, genesList.ToArray().Length, newGenesList);
                genesList = newGenesList;
            }
        }

        public void MutationWithLenghtChanging(Tuple<int, int> sizeOfMap)
        {
            System.Console.WriteLine("Mutation with length changing...");
            Random rn = new Random();
            int lenght = genesList.ToArray().Length;

            int ind = rn.Next(1, lenght-2);

            Tuple<int, int> a = genesList[ind - 1].value;
            Tuple<int, int> b = genesList[ind].value; // ten gen będziemy zmieniać
            Tuple<int, int> c = genesList[ind + 1].value;

            if (TryMutationCorner(a, ref b, c) == 1)
            {
                System.Console.WriteLine("TryingMutationCorner...");
                Gene[] newGenes = new Gene[lenght];
                List<Gene> newGenesList = new List<Gene>();

                Clone(0, ind, newGenesList);
                newGenesList.Add(new Gene(b));
                Clone(ind + 1, lenght, newGenesList);
            }
            else
            {
                System.Console.WriteLine("TryingOtherMutation...");
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

                    if ((newB.Item1 < 0) || (newB.Item1 >= sizeOfMap.Item1))
                    {
                        lenght = lenght - 2;
                        newB = b;
                    }
                }
                else
                {
                    //a.Item2 == c.Item2
                    int decission = rn.Next();
                    if (decission % 2 == 0)
                        // idziemy do góry
                        newB = new Tuple<int, int>(b.Item1, b.Item2-1);
                    else
                        // idziemy w dół
                        newB = new Tuple<int, int>(b.Item1, b.Item2+1);

                    lenght += 3;

                    if ((newB.Item2 < 0) || (newB.Item2 >= sizeOfMap.Item2))
                    {
                        lenght = lenght - 2;
                        newB = b;
                    }
                    
                }

                List<Gene> newGenesList = new List<Gene>();

                Clone(0, ind, newGenesList);
                
                int indNew = ind;
                GenerateStep(a, newB, ref newGenesList);
                GenerateStep(newB, c, ref newGenesList);
                
                Clone(ind + 2, genesList.ToArray().Length, newGenesList);

                genesList = newGenesList;
            }
        }
        public Chromosom Cross(Chromosom ch, Tuple<int, int> sizeOfMap)
        {
            System.Console.WriteLine("Crossing...");
            Chromosom son = new Chromosom();
            
            Random rn = new Random();
            int lenght = genesList.ToArray().Length;

            int ind = rn.Next(0, lenght / 2);
            int ind_ch = ind * ch.genesList.ToArray().Length / lenght;
            int value = rn.Next(Math.Min(genesList[ind].value.Item2, ch.genesList[ind_ch].value.Item2), Math.Max(genesList[ind].value.Item2, ch.genesList[ind_ch].value.Item2));
            
            List<Gene> newGenesList = new List<Gene>();
            Clone(0, 1, newGenesList);
            
            Tuple <int, int>  pom = new Tuple<int, int>(genesList[ind].value.Item1, value);
            GenerateGenesFromTo(newGenesList[0].value,pom  , ref newGenesList);
            
            ind = rn.Next(lenght / 2, lenght-1);
            value = rn.Next(Math.Min(genesList[ind].value.Item2, ch.genesList[ind_ch].value.Item2), Math.Max(genesList[ind].value.Item2, ch.genesList[ind_ch].value.Item2));
            GenerateStep(newGenesList[newGenesList.ToArray().Length-1].value, genesList[lenght-1].value, ref newGenesList);
            
            //Clone(lenght - 1, lenght, newGenesList);

            son.genesList = newGenesList;
            return son;
        }
        public void SetFitness(double fit)
        {
            fitness = fit;
        }

        public String ToString()
        {
            String a = "";
            a += "fit: " + fitness  + "\n";

            for (int i = 0; i<genesList.ToArray().Length; i++)
            {
                a += genesList[i].value.Item1 + " : " + genesList[i].value.Item2 + " | ";
            }

            return a;
        }
    }
}
