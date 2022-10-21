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

        public Person()
        {
            Random random = new();

            XPos = random.Next(100);
            YPos = random.Next(25);

            Direction = new int[2];
            Direction[0] = random.Next(-1, 2);
            Direction[1] = random.Next(-1, 2);

            Name = "X";
        }

        public void Move(List<Person> listOfPeople, Person[,] matrix)
        {
            foreach (var person in listOfPeople)
            {
                if (person is Batman)
                {

                }
                person.YPos += person.Direction[0];
                person.XPos += person.Direction[1];

                if (person.YPos == matrix.GetLength(0))
                {
                    person.YPos = 0;
                }
                if (person.YPos < 0)
                {
                    person.YPos = matrix.GetLength(0) - 1;
                }
                if (person.XPos == matrix.GetLength(1))
                {
                    person.XPos = 0;
                }
                if (person.XPos < 0)
                {
                    person.XPos = matrix.GetLength(1) - 1;
                }

            }
        }
    }
}
