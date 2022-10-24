using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
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

        private readonly Prison _prison;

        private readonly Random _random;

        public string action = "";

        public City()
        {
            Matrix = new Person[25, 100];
            _random = new();
            _prison = new();
        }

        public void Run()
        {
            List<Person> people = new();
            Batman batman = new(_random);

            for (int i = 0; i < 30; i++)
            {
                Police police = new(_random);
                people.Add(police);
            }
            for (int i = 0; i < 30; i++)
            {
                Citizen citizen = new(_random);
                people.Add(citizen);
            }
            for (int i = 0; i < 30; i++)
            {
                Thief thief = new(_random);
                people.Add(thief);
            }

            while (true)
            {
                Matrix = new Person[25, 100];

                //ActionList(people);

                PutPeopleInMatrix(people);

                RunPrison(people);
                DrawCity();
                DrawPrison();

                WriteAction();

                MovePeopleInCity(people, Matrix);

                DeployTimer(batman, people);

                Thread.Sleep(200);
            }
        }

        private void DrawPrison()
        {
            Console.WriteLine("┌" + "".PadRight(_prison.Matrix.GetLength(1), '─') + "┐");

            for (int rows = 0; rows < _prison.Matrix.GetLength(0); rows++)
            {
                Console.Write("│");
                for (int cols = 0; cols < _prison.Matrix.GetLength(1); cols++)
                {
                    if (_prison.Matrix[rows, cols] is not null)
                    {
                        Console.ForegroundColor = _prison.Matrix[rows, cols].Color;
                        Console.Write(_prison.Matrix[rows, cols].Name);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("│");
                Console.WriteLine();
            }

            Console.WriteLine("└" + "".PadRight(_prison.Matrix.GetLength(1), '─') + "┘");
        }

        public void RunPrison(List<Person> people)
        {
            _prison.Matrix = new Person[10, 20];

            List<Thief> thieves = people.OfType<Thief>().ToList();
            List<Thief> isArrested = thieves.Where(thief => thief.IsArrested).ToList();
            _prison.PutThievesInPrison(people, isArrested);

            List<Thief> releasedThieves = _prison.GetReleasedPrisoners();
            people.AddRange(releasedThieves);

            _prison.UpdatePrisonMatrix();

            foreach (var _prisoner in _prison.Prisoners)
            {
                _prisoner.Move(_prison.Matrix);
            }
        }

        // OBS! Vi hanterar ej mer än två möten på samma plats
        private void PutPeopleInMatrix(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                Person otherPerson = Matrix[people[i].YPos, people[i].XPos];

                if (otherPerson is null)
                {
                    Matrix[people[i].YPos, people[i].XPos] = people[i];
                }
                else
                {
                    Matrix[people[i].YPos, people[i].XPos] = new Person(_random);

                    Collide(otherPerson, people[i], people);
                }
            }
        }

        private void Collide(Person person, Person otherPerson, List<Person> people)
        {
            // Rob()
            if (person is Thief && otherPerson is Citizen)
            {
                Rob((Thief)person, (Citizen)otherPerson);
            }
            else if (person is Citizen && otherPerson is Thief)
            {
                Rob((Thief)otherPerson, (Citizen)person);
            }

            // Seize()
            if (person is Police && otherPerson is Thief)
            {
                Seize((Police)person, (Thief)otherPerson, people);
            }
            else if (person is Thief && otherPerson is Police)
            {
                Seize((Police)otherPerson, (Thief)person, people);
            }

            // KaPow()
            if (person is Batman && otherPerson is Thief)
            {
                KaPow((Batman)person, (Thief)otherPerson, people);
            }
            else if (person is Thief && otherPerson is Batman)
            {
                KaPow((Batman)otherPerson, (Thief)person, people);
            }
        }

        private void Rob(Thief thief, Citizen citizen)
        {
            if (citizen.Belongings.Count > 0)
            {
                RobbedCounter++;
                thief.Rob(citizen);
                action = "Citizen gets robbed by thief";
            }
        }

        private void Seize(Police police, Thief thief, List<Person> people)
        {
            if (thief.Loot.Count > 0)
            {
                action = "Police arrests thief";
                SeizedCounter++;
                police.Seize(thief);
            }
        }

        private void KaPow(Batman batman, Thief thief, List<Person> people)
        {
            if (thief.Loot.Count == 0)
            {
                action = "Batman want to kill the thief for being a bad thief";
            }
            else
            {
                batman.KaPow(thief, people);
            }
        }

        private void DrawCity()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("┌" + "".PadRight(Matrix.GetLength(1), '─') + "┐");

            for (int rows = 0; rows < Matrix.GetLength(0); rows++)
            {
                Console.Write("│");
                for (int cols = 0; cols < Matrix.GetLength(1); cols++)
                {
                    if (Matrix[rows, cols] is not null)
                    {
                        Console.ForegroundColor = Matrix[rows, cols].Color;
                        Console.Write(Matrix[rows, cols].Name);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("│");
                Console.WriteLine();
            }
            Console.WriteLine("└" + "".PadRight(Matrix.GetLength(1), '─') + "┘");
        }

        public void MovePeopleInCity(List<Person> people, Person[,] matrix)
        {
            foreach (var person in people)
            {
                if (person is Batman)
                {
                    person.Direction[0] = _random.Next(-1, 2);
                    person.Direction[1] = _random.Next(-1, 2);
                }

                person.Move(matrix);
            }
        }

        private void WriteAction()
        {
            Console.CursorVisible = false;

            Console.SetCursorPosition(25, 30);
            Console.WriteLine("Number of inmates: " + _prison.Prisoners.Count + "       ");
            Console.SetCursorPosition(25, 31);
            Console.WriteLine("Number of robberies: " + RobbedCounter);

            Console.SetCursorPosition(25, 28);
            if (string.IsNullOrEmpty(action))
            {
                Console.WriteLine("".PadRight(90, ' '));
            }
            else
            {
                Console.WriteLine(action + "                                   ");

                Thread.Sleep(2000);
                action = "";
            }
        }

        public void DeployTimer(Batman batman, List<Person> persons)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (batman.Counter < 100)
            {
                Console.SetCursorPosition(25, 35);
                Console.WriteLine("LOADING REINFORCEMENTS");
                Console.SetCursorPosition(25, 36);
                Console.WriteLine($"{batman.Counter}%");
            }
            else if (batman.Counter == 100)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(25, 35);
                Console.WriteLine("Press B to deploy BATMAN");
                Console.SetCursorPosition(25, 36);
                Console.WriteLine("                                    ");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'b':
                        persons.Add(batman);
                        Console.SetCursorPosition(25, 35);
                        Console.WriteLine("".PadRight(90, ' '));
                        Console.SetCursorPosition(25, 36);
                        Console.WriteLine("".PadRight(90, ' '));
                        break;
                }
            }
            Console.ResetColor();

            batman.Counter++;
        }

        // Endast en utskriftskontroll
        private void ActionList(List<Person> people)
        {
            int counter = 1;
            foreach (Person person in people)
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