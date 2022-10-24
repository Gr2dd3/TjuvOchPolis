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
<<<<<<< Updated upstream
            List<Person> persons = new();
            Person p = new Person();
            Prison prison = new();
            Batman batman = new();
=======
            List<Person> people = new();
            Batman batman = new(_random);
            Joker joker = new(_random);
            Hero hero = new(_random);
>>>>>>> Stashed changes

            for (int i = 0; i < 10; i++)
            {
                Batman batman1 = new();
                //Police police = new();
                persons.Add(batman1);
            }
            for (int i = 0; i < 30; i++)
            {
                Citizen citizen = new();
                persons.Add(citizen);
            }
<<<<<<< Updated upstream
            for (int i = 0; i < 25; i++)
=======
            for (int i = 0; i < 20; i++)
>>>>>>> Stashed changes
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
                prison.Draw(persons);

<<<<<<< Updated upstream
                p.Move(persons, Matrix);
=======
                WriteAction();

                MovePeopleInCity(people, Matrix);

                DeployTimer(hero, batman, joker, people);
>>>>>>> Stashed changes

                DrawAction(prison);
                persons = batman.BatDeployTimer(persons, batman);
                Thread.Sleep(200);
                // Console.ReadKey();

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
<<<<<<< Updated upstream
            // Batman
            if (person is Batman && Matrix[rows, cols] is Thief || person is Thief && Matrix[rows, cols] is Batman)
=======

            // TurnEvil()
            if (person is Joker && otherPerson is Citizen)
            {
                TurnEvil((Joker)person, (Citizen)otherPerson, people);
            }
            else if (person is Citizen && otherPerson is Joker)
            {
                TurnEvil((Joker)otherPerson, (Citizen)person, people);  
            }
        }

        private void TurnEvil(Joker joker, Citizen citizen, List<Person> people)
        {
                action = "The Joker strips you of the loot and turn you into a thief!";
                joker.TurnEvil(citizen, people, _random);
        }

        private void Rob(Thief thief, Citizen citizen)
        {
            if (citizen.Belongings.Count > 0)
>>>>>>> Stashed changes
            {
                Batman batman = new();

                action = batman.KaPow(person, Matrix, rows, cols, action, persons);
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
                    else if (Matrix[rows, cols] is Thief)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (Matrix[rows, cols] is Citizen)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (Matrix[rows, cols] is Batman)
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.Write(Matrix[rows, cols] == null ? " " : Matrix[rows, cols].Name);
                    Console.ResetColor();
                }
                Console.Write("│");
                Console.WriteLine();
            }
            Console.WriteLine("└" + "".PadRight(Matrix.GetLength(1), '─') + "┘");
        }

<<<<<<< Updated upstream
        private void DrawAction(Prison prison)
=======
        public void MovePeopleInCity(List<Person> people, Person[,] matrix)
        {
            foreach (var person in people)
            {
                if (person is Batman || person is Joker)
                {
                    person.Direction[0] = _random.Next(-1, 2);
                    person.Direction[1] = _random.Next(-1, 2);
                }

                person.Move(matrix);
            }
        }

        private void WriteAction()
>>>>>>> Stashed changes
        {
            Console.CursorVisible = false;

            Console.SetCursorPosition(25, 30);
            Console.WriteLine("Number of inmates: " + prison.Prisoners.Count + "       ");
            Console.SetCursorPosition(25, 31);
            Console.WriteLine("Number of robberies: " + RobbedCounter + "    ");
            if (action != null)
            {
                Console.SetCursorPosition(25, 28);
                Console.WriteLine(action + "               ");

<<<<<<< Updated upstream

                Thread.Sleep(2500);
                action = null;
            }
            else
            {
                Console.SetCursorPosition(25, 28);
                Console.WriteLine("".PadRight(90, ' '));
=======
                Thread.Sleep(2000);
                action = "";
            }
        }

        public void DeployTimer(Hero hero, Batman batman, Joker joker, List<Person> persons)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (hero.Counter < 100)
            {
                Console.SetCursorPosition(25, 35);
                Console.WriteLine("LOADING REINFORCEMENTS");
                Console.SetCursorPosition(25, 36);
                Console.WriteLine($"{hero.Counter}%");
            }
            else if (hero.Counter == 100)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(25, 35);
                Console.WriteLine("Press B to deploy BATMAN");
                Console.SetCursorPosition(25, 36);
                Console.WriteLine("Press J to deploy THE JOKER");
>>>>>>> Stashed changes


<<<<<<< Updated upstream
            }
=======
                switch (key.KeyChar)
                {
                    case 'b':
                        persons.Add(batman);
                        Console.SetCursorPosition(25, 35);
                        Console.WriteLine("".PadRight(90, ' '));
                        Console.SetCursorPosition(25, 36);
                        Console.WriteLine("".PadRight(90, ' '));
                        hero.Counter = 0;
                        break;

                    case 'j':
                        persons.Add(joker);
                        Console.SetCursorPosition(25, 35);
                        Console.WriteLine("".PadRight(90, ' '));
                        Console.SetCursorPosition(25, 36);
                        Console.WriteLine("".PadRight(90, ' '));
                        hero.Counter = 0;
                        break;
                }
            }
            Console.ResetColor();

            hero.Counter++;
>>>>>>> Stashed changes
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