using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Prison
    {
        public List<Person> Prisoners { get; set; }
        public int PrisonCounter { get; set; }
        public Person[,] Matrix { get; set; }

        public Prison()
        {
            Matrix = new Person[10, 10];
            Prisoners = new List<Person>();
        }


        public void DrawPrison(Prison prison)
        {


            for (int rows = 0; rows < Matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < Matrix.GetLength(1); cols++)
                {
                    Console.Write(Matrix[rows, cols] == null ? " " : Matrix[rows, cols]);
                }
                Console.WriteLine();
            }
        }





        //public void RunPrison(Person person, City city)
        //{
        //    for (int i = 0; i < Prisoners.Count; i++)
        //    {
        //        if (((Thief)person).Loot.Count == 4)
        //        {
        //            PrisonCounter = 40;

        //            PrisonCounter--;
        //        }
        //    }
        //}
    }
}
