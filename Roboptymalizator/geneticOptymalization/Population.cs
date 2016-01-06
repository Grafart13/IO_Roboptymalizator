using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class Population
    {
        public List<Generation> generations { get; private set; }
        private int minSize;
        private int maxSize;
        private Chromosom chFirst;

        public Population (int min, int max, Chromosom chromosomFirst)
        {
            // min = min size of population
            // max = max size of population

            this.minSize = min;
            this.maxSize = max;
            this.chFirst = chromosomFirst;
            generations = new List<Generation>();
        }

        public void createInitialGeneration()
        {
            int numGeneration = 0;
            List<Chromosom> chromosoms = new List<Chromosom>();

        }

    }
}
