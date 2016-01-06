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
        private double x = 10.0; // is the width of the grid

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
            AddMoves();
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
            fields[r.Next(0, n), r.Next(0, m)].SetStart();
            fields[r.Next(0, n), r.Next(0, m)].SetStop();
        }

        private void AddMoves()
        {
            int n = fields.GetLength(0);
            int m = fields.GetLength(1);
            for (int i = 0; i<n; i++)
            {
                for (int j = 0; j<m; j++)
                {
                    if ( j != 0)
                    {
                        Move upMove = new Move(fields[i, j], fields[i, j - 1], x);
                        fields[i, j].AddMove(0, upMove);
                    }
                    if (i != n-1)
                    {
                        Move rMove = new Move(fields[i, j], fields[i + 1, j], x);
                        fields[i, j].AddMove(1, rMove);
                    }
                    if (j != m-1)
                    {
                        Move dMove = new Move(fields[i, j], fields[i, j + 1], x);
                        fields[i, j].AddMove(2, dMove);
                    }
                    if (i != 0)
                    {
                        Move lMove = new Move(fields[i, j], fields[i - 1, j], x);
                        fields[i, j].AddMove(3, lMove);                    
                    }
                }
            }
        }
        public void ShowTerrainMap()
        {
            Console.WriteLine(fields.GetLength(0) + " | " + fields.GetLength(1));
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    Console.Write(fields[i, j].IsStart() ? " S " : "");
                    Console.Write(fields[i, j].IsStop() ? " X " : "");
                    Console.Write(fields[i, j].GetHeight() + " : ");
                }
                Console.WriteLine();
            }
        }

        public Field GetStart()
        {
            foreach(Field f in fields)
            {
                if (f.IsStart() == true)
                    return f;
            }
            return null;
        }
    }
}
