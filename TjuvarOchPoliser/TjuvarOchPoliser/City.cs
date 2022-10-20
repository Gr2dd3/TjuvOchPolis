using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TjuvarOchPoliser
{
    internal class City
    {
        public int RobbedCounter { get; set; }
        public int SeizedCounter { get; set; }
        public Person[,] Matrix { get; set; }

        public string action = "";

        public City()
        {
            Matrix = new Person[25, 100];
        }

        public void CityRun()
        {
            List<Person> persons = new();
            Person p = new Person();

            Prison prison = new();

            for (int i = 0; i < 20; i++)
            {
                Police police = new();
                persons.Add(police);
            }
            for (int i = 0; i < 20; i++)
            {
                Citizen citizen = new();
                persons.Add(citizen);
            }
            for (int i = 0; i < 20; i++)
            {
                Thief thief = new();
                persons.Add(thief);
            }

            while (true)
            {
                p.Movement(persons, Matrix);

                Console.WriteLine("┌" + "".PadRight(100, '─') + "┐");

                // Placerar personerna i Matrix
                for (int rows = 0; rows < Matrix.GetLength(0); rows++)
                {
                    Console.Write("│");
                    for (int cols = 0; cols < Matrix.GetLength(1); cols++)
                    {
                        for(int index = 0; index < persons.Count; index++)
                        {
                            if (persons[index].XPos == cols && persons[index].YPos == rows)
                            {

                                if (Matrix[rows, cols] != null)
                                {
                                    Collide(rows, cols, Matrix, prison, persons, index);
                                }
                                else
                                {
                                    Matrix[rows, cols] = persons[index];
                                }
                            }
                        }
                        if (Matrix[rows, cols] is Police)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        if (Matrix[rows, cols] is Thief)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (Matrix[rows, cols] is Citizen)
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Matrix[rows, cols] == null ? " " : Matrix[rows, cols].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Matrix[rows, cols] = null;       // Raderar "spåret" på fult sätt
                    }
                    Console.Write("│");

                    Console.WriteLine();
                }
                Console.WriteLine("└" + "".PadRight(100, '─') + "┘");

                Console.SetCursorPosition(45, 33);
                Console.WriteLine("Antal tjuvar arresterade: " + SeizedCounter);
                Console.SetCursorPosition(45, 34);
                Console.WriteLine("Antal rånade: " + RobbedCounter);
                if (action != "")
                {
                    Console.SetCursorPosition(45, 30);
                    Console.WriteLine(action);
                    Console.SetCursorPosition(45, 31);
                    Console.WriteLine("-------------------");

                    Thread.Sleep(2000);
                    action = "";
                }
                prison.DrawPrison();
                Thread.Sleep(250);
                // ActionList(persons);
                //Console.ReadKey();
                Console.Clear();
            }
        }


        public void Collide(int rows, int cols, Person[,] Matrix, Prison prison, List<Person> persons, int index)
        {

            // Rån
            if (persons[index] is Thief && Matrix[rows, cols] is Citizen || persons[index] is Citizen && Matrix[rows, cols] is Thief)
            {
                Thief thief = new();
                action = thief.Rob(Matrix, rows, cols, action, persons, index);
                if (action != "")
                {
                    RobbedCounter++;
                }
            }
            // Beslagta
            if (persons[index] is Police && Matrix[rows, cols] is Thief || persons[index] is Thief && Matrix[rows, cols] is Police)
            {
                Police police = new();
                action = police.Seize(Matrix, rows, cols, action, prison, persons, index);
                if (action != "")
                {
                    SeizedCounter++;
                }
            }
        }

        private void ActionList(List<Person> persons)
        {
            int counter = 1;
            foreach (Person person in persons)
            {
                Console.Write("Person " + counter + ": " + person.GetType().Name + " - " + person.XPos + "," + person.YPos + " : ");
                if (person is Citizen)
                {
                    for (int i = 0; i < ((Citizen)person).Belongings.Count; i++)
                        Console.Write(((Citizen)person).Belongings[i].GetType().Name + " ");
                }
                if (person is Thief)
                {
                    for (int i = 0; i < ((Thief)person).Loot.Count; i++)
                        Console.Write(((Thief)person).Loot[i].GetType().Name + " ");
                }
                if (person is Police)
                {
                    for (int i = 0; i < ((Police)person).Seized.Count; i++)
                        Console.Write(((Police)person).Seized[i].GetType().Name + " ");
                }
                Console.WriteLine();
                counter++;
            }
        }
    }
}
