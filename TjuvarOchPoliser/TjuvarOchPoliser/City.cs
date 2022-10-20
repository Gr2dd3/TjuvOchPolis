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

        public void CityRun()
        {
            List<Person> persons = new();
            Person p = new Person();

            for (int i = 0; i < 10; i++)
            {
                Police police = new();
                persons.Add(police);
            }
            for (int i = 0; i < 30; i++)
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
                // ActionList(persons);

                p.Movement(persons, Matrix);
                Person personX = new();
                string action = "";
                Console.WriteLine("┌" + "".PadRight(100, '─') + "┐");

                // Placerar personerna i Matrix
                for (int rows = 0; rows < Matrix.GetLength(0); rows++)
                {
                    Console.Write("│");
                    for (int cols = 0; cols < Matrix.GetLength(1); cols++)
                    {
                        foreach (var person in persons)
                        {
                            if (person.XPos == cols && person.YPos == rows)
                            {

                                if (Matrix[rows, cols] != null)
                                {
                                    // Police möter Thief
                                    if (person is Police && Matrix[rows, cols] is Thief)
                                    {
                                        // Seize();
                                        if (((Thief)Matrix[rows, cols]).Loot.Count == 0)
                                        {
                                            break;
                                        }
                                        ((Police)person).Seized.AddRange(((Thief)Matrix[rows, cols]).Loot);
                                        ((Thief)Matrix[rows, cols]).Loot.Clear();
                                        Matrix[rows, cols] = personX;
                                        action = "Polis arresterar tjuv";
                                        SeizedCounter++;
                                        // GoToJail();
                                    }
                                    // Thief möter Police
                                    if (person is Thief && Matrix[rows, cols] is Police)
                                    {

                                        //Seize();
                                        if (((Thief)person).Loot.Count == 0)
                                        {
                                            break;
                                        }
                                        ((Police)Matrix[rows, cols]).Seized.AddRange(((Thief)person).Loot);
                                        ((Thief)person).Loot.Clear();
                                        Matrix[rows, cols] = personX;
                                        action = "Polis arresterar tjuv";
                                        SeizedCounter++;
                                        // GoToJail();
                                    }
                                    // Citizen möter Thief
                                    if (person is Citizen && Matrix[rows, cols] is Thief)
                                    {

                                        //Rob();
                                        if (((Citizen)person).Belongings.Count == 0)
                                        {
                                            break;
                                        }
                                        Random random = new();
                                        int removeAtIndex = random.Next(((Citizen)person).Belongings.Count - 1);
                                        ((Thief)Matrix[rows, cols]).Loot.Add(((Citizen)person).Belongings[removeAtIndex]);
                                        ((Citizen)person).Belongings.RemoveAt(removeAtIndex);
                                        Matrix[rows, cols] = personX;
                                        action = "Tjuv rånar medborgare";
                                        RobbedCounter++;
                                    }
                                    // Thief möter Citizen
                                    if (person is Thief && Matrix[rows, cols] is Citizen)
                                    {

                                        //Rob();
                                        if (((Citizen)Matrix[rows, cols]).Belongings.Count == 0)
                                        {
                                            break;
                                        }
                                        Random random = new();
                                        int removeAtIndex = random.Next(((Citizen)Matrix[rows, cols]).Belongings.Count - 1);
                                        ((Thief)person).Loot.Add(((Citizen)Matrix[rows, cols]).Belongings[removeAtIndex]);
                                        ((Citizen)Matrix[rows, cols]).Belongings.RemoveAt(removeAtIndex);
                                        Matrix[rows, cols] = personX;
                                        action = "Tjuv rånar medborgare";
                                        RobbedCounter++;
                                    }
                                }
                                else
                                {
                                    Matrix[rows, cols] = person;
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
                }
                Thread.Sleep(200);
                Console.Clear();
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
