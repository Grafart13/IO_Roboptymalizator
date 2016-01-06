using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class FitnessService
    {
        public heart.TerrainMap terrain;

        public FitnessService(heart.TerrainMap terrain)
        {
            this.terrain = terrain;
        }

        public double ComputeFittness(Chromosom ch)
        {
            double fitness = 0.0;
            return fitness ;
        }
        
    }

}
