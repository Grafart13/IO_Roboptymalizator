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
        private Tuple<int, int> startInd;
        private Tuple<int, int> stopInd;

        public TerrainMap()
        {
            // generate Terrain Map for testing
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    fields[i, j] = new Field(2 * i + 3 * j + 0.2, new Tuple<int,int>(i,j));
            
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
                    fields[i, j] = new Field(r.NextDouble() * 200.0, new Tuple<int,int>(i,j));
                }
            int a = r.Next(0, n);
            int b = r.Next(0, m);
            fields[a,b].SetStart();
            
            startInd = new Tuple<int,int>(a, b);
            
            a = r.Next(0, n);
            b = r.Next(0, m);
            fields[a,b].SetStop();
            stopInd = new Tuple<int, int>(a, b);
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

        public Field GetField(int x, int y)
        {
            return fields[x, y];
        }
        public Field GetStartField()
        {
            return GetField(startInd.Item1, startInd.Item2);
        }
        public Tuple<int, int> GetStartInd()
        {
            return startInd;
        }

        public Tuple<int, int> GetStopInd()
        {
            return stopInd;
        
        }
        public Field GetStopField()
        {
            return GetField(stopInd.Item1, stopInd.Item2);
        }
        
        public int getLenght0()
        {
            return fields.GetLength(0);
        }
        public int getLenght1()
        {
            return fields.GetLength(1);
        }

        public Tuple<int, int> GetSizeOfMap()
        {
            return new Tuple<int, int>(getLenght0(), getLenght1());
        }

    }
}
