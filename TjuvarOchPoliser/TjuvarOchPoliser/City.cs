using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
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

        public string? action;

        public City()
        {
            Matrix = new Person[25, 100];
        }

        public void Run()
        {
            List<Person> persons = new();
            Person p = new Person();
            Prison prison = new();

            for (int i = 0; i < 25; i++)
            {
                Police police = new();
                persons.Add(police);
            }
            for (int i = 0; i < 30; i++)
            {
                Citizen citizen = new();
                persons.Add(citizen);
            }
            for (int i = 0; i < 15; i++)
            {
                Thief thief = new();
                persons.Add(thief);
            }


            while (true)
            {
                //ActionList(persons);

               
                Matrix = new Person[25, 100];
                
                PutPeopleInCity(persons, prison);
                DrawMatrix();
                p.Move(persons, Matrix);
                prison.Draw(persons);
                DrawAction(prison);
                Thread.Sleep(200);
                //Console.ReadKey();                
            }
        }

        private void PutPeopleInCity(List<Person> persons, Prison prison)
        {
            for (int i = 0; i < persons.Count; i++)
            {
                Matrix[persons[i].YPos, persons[i].XPos] = Matrix[persons[i].YPos, persons[i].XPos] is null ? persons[i] : Collide(persons[i].YPos, persons[i].XPos, persons[i], persons, prison);
            }
        }

        private Person Collide(int rows, int cols, Person person, List<Person> persons, Prison prison)
        {
            Person personX = new();

            // Rån
            if (person is Thief && Matrix[rows, cols] is Citizen || person is Citizen && Matrix[rows, cols] is Thief)
            {
                Thief thief = new();
                if ((Matrix[rows, cols] is Thief && ((Citizen)person).Belongings.Count > 0 || person is Thief && ((Citizen)Matrix[rows, cols]).Belongings.Count > 0))
                {
                    RobbedCounter++;
                }
                action = thief.Rob(person, Matrix, rows, cols, action);
            }
            // Beslagta
            if (person is Police && Matrix[rows, cols] is Thief || person is Thief && Matrix[rows, cols] is Police)
            {
                Police police = new();
                if (Matrix[rows, cols] is Thief && ((Thief)Matrix[rows, cols]).Loot.Count > 0 || person is Thief && ((Thief)person).Loot.Count > 0)
                {
                    SeizedCounter++;
                }
                action = police.Seize(person, Matrix, rows, cols, action, persons, prison);
            }
            return personX;
        }

        private void DrawMatrix()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("┌" + "".PadRight(Matrix.GetLength(1), '─') + "┐");
            for (int rows = 0; rows < Matrix.GetLength(0); rows++)
            {
                Console.Write("│");
                for (int cols = 0; cols < Matrix.GetLength(1); cols++)
                {
                    if (Matrix[rows, cols] is Police)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (Matrix[rows, cols] is Thief)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (Matrix[rows, cols] is Citizen)
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Matrix[rows, cols] == null ? " " : Matrix[rows, cols].Name);
                    Console.ResetColor();
                }
                Console.Write("│");
                Console.WriteLine();
            }
            Console.WriteLine("└" + "".PadRight(Matrix.GetLength(1), '─') + "┘");
        }

        private void DrawAction(Prison prison)
        {
            Console.CursorVisible = false;

            Console.SetCursorPosition(45, 33);
            Console.WriteLine("Number of inmates: " + prison.Prisoners.Count + "   ");
            Console.SetCursorPosition(45, 34);
            Console.WriteLine("Number of robberies: " + RobbedCounter);
            if (action != null)
            {
                Console.SetCursorPosition(45, 30);
                Console.WriteLine(action);
                Console.SetCursorPosition(45, 31);

                Thread.Sleep(2000);
                action = null;
            }
            else
            {
                Console.SetCursorPosition(45, 30);
                Console.WriteLine("".PadRight(45, ' '));
                Console.SetCursorPosition(45, 31);

            }
        }

        // Endast en utskriftskontroll
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