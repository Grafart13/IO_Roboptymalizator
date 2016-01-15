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
        private int minSize;
        private int numOfGeneration;
        private int numOfChromosoms;
        private int lenghtOfChromosom;
        private Chromosom chFirst { get; set; }

        private FitnessService fs;

        public Population (int lenghtOfChromosom, int numOfChromosoms, int numOfGeneration, FitnessService fs)
        {
            
            this.lenghtOfChromosom = lenghtOfChromosom;
            this.numOfChromosoms = numOfChromosoms;
            this.numOfGeneration = numOfGeneration;
            generations = new List<Generation>();
            this.fs = fs;
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
                chromosoms.Add(ch);
            }
            generations.Add(new Generation(numGeneration, chromosoms));
        }

        public void ToString()
        {
            
        }
    }
}
