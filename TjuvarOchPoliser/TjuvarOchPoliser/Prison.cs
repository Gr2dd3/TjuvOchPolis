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


        public void Draw(List<Person> persons)
        {
            Matrix = new Person[10, 20];
            Person p = new();

            p.Move(Prisoners, Matrix);

            foreach (var prisoner in Prisoners)
            {
                Random r = new Random();
                while (Matrix[prisoner.YPos, prisoner.XPos] != null)
                {
<<<<<<< Updated upstream
                    prisoner.XPos = r.Next(Matrix.GetLength(1));
                    prisoner.YPos = r.Next(Matrix.GetLength(0));
=======
                    prisoner.XPos = random.Next(Matrix.GetLength(1));
                    prisoner.YPos = random.Next(Matrix.GetLength(0));
>>>>>>> Stashed changes
                }
                Matrix[prisoner.YPos, prisoner.XPos] = prisoner;
            }

            Console.WriteLine("┌" + "".PadRight(Matrix.GetLength(1), '─') + "┐");
            for (int rows = 0; rows < Matrix.GetLength(0); rows++)
            {
                Console.Write("│");
                for (int cols = 0; cols < Matrix.GetLength(1); cols++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Matrix[rows, cols] == null ? " " : Matrix[rows, cols].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("│");
                Console.WriteLine();
            }
            Console.WriteLine("└" + "".PadRight(Matrix.GetLength(1), '─') + "┘");

            for (int i = 0; i < Prisoners.Count; i++)
            {
                ((Thief)Prisoners[i]).PrisonCount--;
                if (((Thief)Prisoners[i]).PrisonCount == 0)
                {
                    persons.Add((Thief)Prisoners[i]);
                    Prisoners.Remove((Thief)Prisoners[i]);
                }
            };
        }
    }
}
