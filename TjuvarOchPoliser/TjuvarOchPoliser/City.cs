using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class City
    {
        public int RobbedCounter { get; set; }
        public int SeizedCounter { get; set; }
        public Person[,] Matrix { get; set; }

        public City()
        {
            Matrix = new Person[25, 100];
        }

    }

    internal class Prison
    {
        public void DrawPrison()
        {
            int[,] prison = new int[10, 10];

            for (int rows = 0; rows < prison.GetLength(0); rows++)
            {
                for (int cols = 0; cols < prison.GetLength(1); cols++)
                {
                    Console.Write(prison[rows, cols]);
                }
                Console.WriteLine();
            }
        }
    }
}
