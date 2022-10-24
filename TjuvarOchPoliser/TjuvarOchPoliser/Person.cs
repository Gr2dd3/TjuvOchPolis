using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TjuvarOchPoliser
{
    internal class Person
    {
        public int[] Direction { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }

        public Person(Random random)
        {
            XPos = random.Next(100);
            YPos = random.Next(25);

            Direction = new int[2];
            Direction[0] = random.Next(-1, 2);
            Direction[1] = random.Next(-1, 2);

            Color = ConsoleColor.White;
            Name = "X";
        }

        public void Move(Person[,] matrix)
        {
            YPos += Direction[0];
            XPos += Direction[1];

            CheckOutOfBounds(matrix);
        }

        private void CheckOutOfBounds(Person[,] matrix)
        {
            if (YPos >= matrix.GetLength(0))
            {
                YPos = 0;
            }
            else if (YPos < 0)
            {
                YPos = matrix.GetLength(0) - 1;
            }
            if (XPos >= matrix.GetLength(1))
            {
                XPos = 0;
            }
            else if (XPos < 0)
            {
                XPos = matrix.GetLength(1) - 1;
            }
        }
    }
}
