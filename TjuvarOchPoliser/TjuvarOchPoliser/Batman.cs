using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TjuvarOchPoliser
{
    internal class Batman : Hero
    {
        public List<Thing> UtilityBelt { get; set; }

        public Batman()
        {
            UtilityBelt = new List<Thing>();
            Counter = 0;
            Name = "B";

        }

        public string KaPow(Person person, Person[,] matrix, int rows, int cols, string action, List<Person> persons)
        {
            // Batman möter Thief
            if (person is Batman)
            {
                if (((Thief)matrix[rows, cols]).Loot.Count == 0)
                {
                    action = "Batman want to kill the thief for being a bad thief";
                }
                if (((Thief)matrix[rows, cols]).Loot.Count > 0 && ((Thief)matrix[rows, cols]).Loot.Count < 4)
                {
                    ((Batman)person).UtilityBelt.AddRange(((Thief)matrix[rows, cols]).Loot);
                    ((Thief)matrix[rows, cols]).Loot.Clear();
                    action = "Batman roughs the thief up a bit!! Oh, and he adds the stuff to his Utility Belt!!";
                }
                if (((Thief)matrix[rows, cols]).Loot.Count > 3)
                {
                    ((Batman)person).UtilityBelt.AddRange(((Thief)matrix[rows, cols]).Loot);
                    ((Thief)matrix[rows, cols]).Loot.Clear();
                    action = "KaPOW! Thief dies and Batman adds the stuff to his Utility Belt!";
                    persons.Remove((Thief)matrix[rows, cols]);
                }
            }

            // Thief möter Batman
            if (person is Thief)
            {
                if (((Thief)person).Loot.Count == 0)
                {
                    action = "Batman want to kill the thief for being a bad thief";
                }
                if (((Thief)person).Loot.Count > 0 && ((Thief)person).Loot.Count < 4)
                {
                    ((Batman)matrix[rows, cols]).UtilityBelt.AddRange(((Thief)person).Loot);
                    ((Thief)person).Loot.Clear();
                    action = "Batman roughs the thief up a bit!! Oh, and he adds the stuff to his Utility Belt!!";
                }
                if (((Thief)person).Loot.Count > 3)
                {
                    ((Batman)matrix[rows, cols]).UtilityBelt.AddRange(((Thief)person).Loot);
                    ((Thief)person).Loot.Clear();
                    action = "KaPOW! Thief dies and Batman adds the stuff to his Utility Belt!";
                    persons.Remove((Thief)person);
                }
            }
            return action;
        }




        public List<Person> BatDeployTimer(List<Person> persons, Batman batman)
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            if (Counter < 100)
            {

                Console.SetCursorPosition(25, 35);
                Console.WriteLine("LOADING REINFORCEMENTS");
                Console.SetCursorPosition(25, 36);
                Console.WriteLine($"{Counter}%");
            }
            else if (Counter == 100)
            {
                Console.SetCursorPosition(25, 35);
                Console.WriteLine("Press B to deploy BATMAN");

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

            Counter++;

            return persons;
        }

        //public void BatMoves()
        //{
        //    foreach (Person person in persons)
        //    {
        //        switch (person.Direction)
        //        {
        //            case 0: // Neråt
        //                person.Ypos++;
        //                if (person.Ypos == city.GetLength(0))
        //                {
        //                    person.Ypos = 0;
        //                }
        //                break;
        //            case 1: // Vänster ner
        //                person.Ypos++; //ner
        //                person.Xpos--; //vänster
        //                if (person.Ypos == city.GetLength(0))
        //                {
        //                    person.Ypos = 0;
        //                    person.Xpos += 1;
        //                }
        //                else if (person.Xpos < 0)
        //                {
        //                    person.Xpos = city.GetLength(1) - 1;
        //                    person.Ypos -= 1;

        //                }
        //                break;
        //            case 2: // Vänster
        //                person.Xpos--;
        //                if (person.Xpos < 0)
        //                {
        //                    person.Xpos = city.GetLength(1) - 1;
        //                }
        //                break;
        //            case 3: // Uppåt
        //                person.Ypos--;
        //                if (person.Ypos < 0)
        //                {
        //                    person.Ypos = city.GetLength(0) - 1;
        //                }
        //                break;
        //            case 4: // Höger uppåt
        //                person.Ypos--;
        //                person.Xpos++;
        //                if (person.Ypos < 0)
        //                {
        //                    person.Ypos = city.GetLength(0) - 1;
        //                    person.Xpos -= 1;
        //                }
        //                else if (person.Xpos == city.GetLength(1))
        //                {
        //                    person.Xpos = 0;
        //                    person.Ypos += 1;

        //                }
        //                break;
        //            case 5: // Höger
        //                person.Xpos++;
        //                if (person.Xpos == city.GetLength(1))
        //                {
        //                    person.Xpos = 0;
        //                }
        //                break;
        //            case 6: // Höger nedåt
        //                person.Xpos++;
        //                person.Ypos++;
        //                if (person.Xpos == city.GetLength(1))
        //                {
        //                    person.Xpos = 0;
        //                    person.Ypos -= 1;
        //                }
        //                else if (person.Ypos == city.GetLength(0))
        //                {
        //                    person.Xpos -= 1;
        //                    person.Ypos = 0;
        //                }
        //                break;
        //            case 7: //Vänster uppåt
        //                person.Xpos--;
        //                person.Ypos--;
        //                if (person.Xpos < 0)
        //                {
        //                    person.Xpos = city.GetLength(1) - 1;
        //                    person.Ypos += 1;
        //                }
        //                else if (person.Ypos < 0)
        //                {
        //                    person.Xpos += 1;
        //                    person.Ypos = city.GetLength(0) - 1;
        //                }
        //                break;
        //        }
        //    }
        //}

    }
}

