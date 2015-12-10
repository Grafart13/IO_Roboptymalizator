using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboptymalizator.heart
{
    class TerrainMap
    {
        private Field[,] fields;

        public TerrainMap()
        {
            // generate Terrain Map for testing
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    fields[i, j] = new Field(2 * i + 3 * j + 0.2);
        }
        public TerrainMap(String name)
        {
            // loading terrain map from file
        }

        public TerrainMap(int n, int m)
        {
            GenerateRandomFields(n, m);
        }

        private void GenerateRandomFields(int n, int m)
        {
            fields = new Field[n, m];
            Random r = new Random();
            for(int i=0; i<n; i++)
                for (int j=0; j<m; j++)
                {
                    fields[i, j] = new Field(r.NextDouble() * 200.0);
                }
        }

        public void ShowTerrainMap()
        {
            Console.WriteLine(fields.GetLength(0) + " | " + fields.GetLength(1));
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                    Console.Write(fields[i, j].GetHeight() + " : ");
                Console.WriteLine();
            }
        }
    }
}
