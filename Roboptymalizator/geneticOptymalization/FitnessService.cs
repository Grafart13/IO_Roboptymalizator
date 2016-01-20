using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.geneticOptymalization
{
    class FitnessService
    {
        public heart.TerrainMap terrain {  get; set; }
        private heart.Robot robot;
        public FitnessService(heart.TerrainMap terrain, heart.Robot rob)
        {
            this.terrain = terrain;
            this.robot = rob;
        }

        private double ComputeOneStepFuel(Tuple<int, int> from, Tuple<int, int> to)
        {
            heart.Field fromField = terrain.GetField(from.Item1, from.Item2);
            heart.Move m;
            if (from.Item1 == to.Item1)
            {
                if (from.Item2 < to.Item2)
                    // idziemy w dol
                    m = fromField.GetMove(2);
                else
                    // idziemy w gore
                    m = fromField.GetMove(0);
            }
            else
            {
                if (from.Item1 < to.Item1)
                    // idziemy w prawo
                    m = fromField.GetMove(1);
                else
                    // idziemy w lewo
                    m = fromField.GetMove(3);
            }
            return robot.BurnFuel(m);
        }
        public double ComputeFittness(Chromosom ch)
        {
            double fitness = 0.0;

            double maxFuel = robot.fuelLevel;
            double currFuel = maxFuel;

            System.Console.WriteLine(ch.ToString());

            for (int i=0; i<ch.GetGenesList().ToArray().Length - 1; i++)
            {
                Gene g = ch.GetGene(i);
                heart.Field fromField = terrain.GetField(g.value.Item1, g.value.Item2);
                Gene g2 = ch.GetGene(i + 1);

                double burn = ComputeOneStepFuel(g.value, g2.value);

                currFuel -= burn;
            }

            //fitness = currFuel / maxFuel * 100.0;
            fitness = currFuel;
            return fitness ;
        }

        public int GetN()
        {
          //  return terrain.
            return 0;
        }
        
    }

}
