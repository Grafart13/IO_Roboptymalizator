using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.heart
{
    class Robot
    {
        private double fuelLevel;
        private const double burning = 2.3; // burning fuel / dist
        private const double maxFuel = 100.0;
        
        private TerrainMap terrain;
        // where is robot on grid?
        private Field start;
        private LinkedList<Field> visited;

        public Robot(TerrainMap terrain)
        {
            this.terrain = terrain;

            this.start = terrain.GetStart();
            if (start == null)
                System.Console.WriteLine("Something went wrong.");
            visited = new LinkedList<Field>();
            visited.AddFirst(start);
            fuelLevel = maxFuel;
        }

        public Field GetStart()
        { 
            return this.start;
        }

        private void BurnFuel(Move move)
        {
            // function of burning fuel
            // for example

            if (move.IsUp())
                fuelLevel -= move.GetDist() * burning + Math.Pow(5.4, move.GetAlfa());
            else
                fuelLevel -= move.GetDist() * burning + Math.Log10(move.GetAlfa());
        }
    }
}
