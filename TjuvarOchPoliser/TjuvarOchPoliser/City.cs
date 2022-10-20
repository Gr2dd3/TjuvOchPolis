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


                p.Movement(persons, Matrix);


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
                                    Collide(person, rows, cols, Matrix);
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

                    // Thread.Sleep(2000);
                    action = "";
                }

                Thread.Sleep(200);
                // ActionList(persons);
                Console.ReadKey();
                Console.Clear();
            }
        }


        public void Collide(Person person, int rows, int cols, Person[,] Matrix)
        {

            // Rån
            if (person is Thief && Matrix[rows, cols] is Citizen || person is Citizen && Matrix[rows, cols] is Thief)
            {
                Thief thief = new();
                action = thief.Rob(person, Matrix, rows, cols, action);
                if (action != "")
                {
                    RobbedCounter++;
                }


            }
            // Beslagta
            if (person is Police && Matrix[rows, cols] is Thief || person is Thief && Matrix[rows, cols] is Police)
            {
                Police police = new();
                action = police.Seize(person, Matrix, rows, cols, action);
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



    internal class Prison
    {
        public List<Person> Prisoners { get; set; }
        public int PrisonCounter { get; set; }
        public Person[,] Matrix { get; set; }

        public Prison()
        {
            Matrix = new Person [10, 10];
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
