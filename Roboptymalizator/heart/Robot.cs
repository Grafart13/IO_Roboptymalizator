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
        // private LinkedList<Move> moves; // is it important?

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

        private double BurnFuel(Move move)
        {
            // function of burning fuel
            // for example
            double loseFuel = 0.0;
            if (move.IsUp())
                loseFuel = move.GetDist() * burning + Math.Pow(5.4, move.GetAlfa());
            else
                loseFuel -= move.GetDist() * burning + Math.Log10(move.GetAlfa());
            return loseFuel;
        }

        public double Move(Move move)
        {
            double loseFuel = BurnFuel(move);
            if (fuelLevel - loseFuel < 0)
            {
                return -1.0;
            }
            else
            {
                fuelLevel -= loseFuel;
                visited.AddLast(move.GetToField());
                return loseFuel;
            }
        }
    }
}
