using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class Population
    {
        // populacja wszystkich pokoleń
        public List<Generation> generations { get; private set; }

        private double mutationRate;
        private int currGeneration;
        private int numOfGeneration;
        private int numOfChromosoms;
        private int lenghtOfChromosom;
        private Chromosom chFirst { get; set; }

        private FitnessService fs;

        public Population (int lenghtOfChromosom, int numOfChromosoms, int numOfGeneration, double mutationRate, FitnessService fs)
        {
            this.mutationRate = mutationRate;
            this.lenghtOfChromosom = lenghtOfChromosom;
            this.numOfChromosoms = numOfChromosoms;
            this.numOfGeneration = numOfGeneration;
            generations = new List<Generation>();
            this.fs = fs;
            currGeneration = 0;
        }

        public void createInitialGeneration()
        {
            int numGeneration = 0;
            List<Chromosom> chromosoms = new List<Chromosom>();

            heart.TerrainMap tm = fs.terrain;

            for (int i=0; i<numOfChromosoms; i++)
            {
                Chromosom ch = new Chromosom(lenghtOfChromosom);
                ch.RandomGenes(tm.GetStartInd(), tm.GetStopInd(), tm.GetSizeOfMap());
                fs.ComputeFittness(ch);
                chromosoms.Add(ch);
            }
            // oblicz fitness dla każdego osobnika
            foreach (Chromosom ch in chromosoms)
                ch.SetFitness(fs.ComputeFittness(ch));

            generations.Add(new Generation(numGeneration, chromosoms));
        }
        private static int ComputeSort (Chromosom ch1, Chromosom ch2)
        {
            double fit1 = ch1.fitness;
            double fit2 = ch2.fitness;

            return - fit1.CompareTo(fit2);
        }
        public void SortChromosoms(List<Chromosom> listOfChromosoms)
        {
            listOfChromosoms.Sort(ComputeSort);
            //generations[++currGeneration] = new Generation()
        }
        private List<Chromosom> SelectParents(List<Chromosom> listOfChromosoms, int howMany)
        {
            List<Chromosom> parents = new List<Chromosom>();
            for (int i = 0; i<howMany; i++)
                parents.Add(listOfChromosoms[i]);
            return parents;
        }
        private void CreateNextGeneration()
        {
            // wybieranie najlepszych osobników
            
            List<Chromosom> listOfChromosoms = generations[currGeneration].chromosomes;
            
            // oblicz fitness dla każdego osobnika
            foreach (Chromosom ch in listOfChromosoms)
                ch.SetFitness(fs.ComputeFittness(ch));
            
            SortChromosoms(listOfChromosoms);
            // selekcja najlepszych osobników
            List<Chromosom> parents = SelectParents(listOfChromosoms, numOfChromosoms / 2 + 1);
            
            // krzyżowanie
            Random rn = new Random();
            for (int i = 0; i < numOfChromosoms - parents.ToArray().Length ; i++)
            {
                int n = i;
                while (i == n)
                    n = rn.Next(0, numOfChromosoms / 2 + 1);
                Chromosom son = parents[i].Cross(parents[n],fs.terrain.GetSizeOfMap());
                parents.Add(son);
            }
            // mutacje
            foreach (Chromosom ch in parents)
            {
                if (rn.NextDouble() <= mutationRate)
                {
                    // ch.Mutation();
                    ch.MutationWithLenghtChanging(fs.terrain.GetSizeOfMap());
                }
            }

            foreach (Chromosom ch in parents)
                ch.SetFitness(fs.ComputeFittness(ch));

            SortChromosoms(parents);

            generations.Add(new Generation(++currGeneration, parents));
        }
        public void CreateGenerations()
        {
            createInitialGeneration();
            for(int i=1; i<numOfGeneration; i++)
            {
                CreateNextGeneration();
            }
        }
        public void ToString()
        {
            foreach(Generation g in generations)
            {
                System.Console.WriteLine("\n\n ### GENERATION : {0:D}\n", g.number);
                foreach (Chromosom ch in g.chromosomes)
                {
                    System.Console.WriteLine(ch.ToString());
                }
            }
        }
    }
}
