using System;
using System.ComponentModel;
using TjuvarOchPoliser;



internal class Program
{
    private static void Main(string[] args)
    {
        City city = new();
        List<Person> persons = new();
        Person p = new Person();

        for (int i = 0; i < 10; i++)
        {
            Police police = new();
            persons.Add(police);
        }
        for (int i = 0; i < 20; i++)
        {
            Citizen citizen = new();
            persons.Add(citizen);
        }
        for (int i = 0; i < 10; i++)
        {
            Thief thief = new();
            persons.Add(thief);
        }

        while (true)
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

            p.Movement(persons, city.Matrix);

            // Placerar personerna i city.Matrix
            for (int rows = 0; rows < city.Matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < city.Matrix.GetLength(1); cols++)
                {
                    foreach (var person in persons)
                    {
                        if (person.XPos == cols && person.YPos == rows)
                        {

                            if (city.Matrix[rows, cols] != null)
                            {

                                // Police möter Thief
                                if (person is Police && city.Matrix[rows, cols] is Thief)
                                {
                                    Console.Write("[X]");
                                    // Seize();
                                    if (((Thief)city.Matrix[rows, cols]).Loot.Count == 0)
                                    {
                                        break;
                                    }
                                    ((Police)person).Seized.AddRange(((Thief)city.Matrix[rows, cols]).Loot);
                                    ((Thief)city.Matrix[rows, cols]).Loot.Clear();
                                    city.SeizedCounter++;
                                    // GoToJail();
                                }
                                // Thief möter Police
                                if (person is Thief && city.Matrix[rows, cols] is Police)
                                {
                                    Console.Write("[X]");
                                    //Seize();
                                    if (((Thief)person).Loot.Count == 0)
                                    {
                                        break;
                                    }
                                    ((Police)city.Matrix[rows, cols]).Seized.AddRange(((Thief)person).Loot);
                                    ((Thief)person).Loot.Clear();
                                    city.SeizedCounter++;
                                    // GoToJail();
                                }
                                // Citizen möter Thief
                                if (person is Citizen && city.Matrix[rows, cols] is Thief)
                                {
                                    Console.Write("[X]");
                                    //Rob();
                                    if (((Citizen)person).Belongings.Count == 0)
                                    {
                                        break;
                                    }
                                    Random random = new();
                                    int removeAtIndex = random.Next(((Citizen)person).Belongings.Count - 1);
                                    ((Thief)city.Matrix[rows, cols]).Loot.Add(((Citizen)person).Belongings[removeAtIndex]);
                                    ((Citizen)person).Belongings.RemoveAt(removeAtIndex);
                                    city.RobbedCounter++;
                                }
                                // Thief möter Citizen
                                if (person is Thief && city.Matrix[rows, cols] is Citizen)
                                {
                                    Console.Write("[X]");
                                    //Rob();
                                    if (((Citizen)city.Matrix[rows, cols]).Belongings.Count == 0)
                                    {
                                        break;
                                    }
                                    Random random = new();
                                    int removeAtIndex = random.Next(((Citizen)city.Matrix[rows, cols]).Belongings.Count - 1);
                                    ((Thief)person).Loot.Add(((Citizen)city.Matrix[rows, cols]).Belongings[removeAtIndex]);
                                    ((Citizen)city.Matrix[rows, cols]).Belongings.RemoveAt(removeAtIndex);
                                    city.RobbedCounter++;
                                }
                            }
                            else
                            {
                                city.Matrix[rows, cols] = person;
                            }
                        }
                    }
                    if (city.Matrix[rows, cols] is Police)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (city.Matrix[rows, cols] is Thief)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (city.Matrix[rows, cols] is Citizen)
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(city.Matrix[rows, cols] == null ? " " : city.Matrix[rows, cols].Name);
                    city.Matrix[rows, cols] = null;       // Raderar "spåret" på fult sätt
                }
                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void Collide(List<Person> cops, List<Person> citizens, List<Person> thieves)
    {
        foreach (var cop in cops)
        {
            foreach (var thief in thieves)
            {
                if (cop.XPos == thief.XPos && cop.YPos == thief.YPos)
                {
                    // Skriv ut "X" och Seize();
                }
            }
        }
    }
}
//while (true)
//{
//    for (int rows = 0; rows < city.Matrix.GetLength(0); rows++)
//    {
//        for (int cols = 0; cols < city.Matrix.GetLength(1); cols++)
//        {
//            foreach (Person person1 in persons)
//            {
//                if (rows == person1.Ypos && cols == person1.Xpos)
//                {
//                    if (person1 is Police)
//                        Console.ForegroundColor = ConsoleColor.Blue;
//                    if (person1 is Thief)
//                        Console.ForegroundColor = ConsoleColor.Red;
//                    if (person1 is Citizen)
//                        Console.ForegroundColor = ConsoleColor.Green;
//                    city.Matrix[rows, cols] = person1.Name;


//                }
//                else
//                {
//                    city.Matrix[rows, cols] = city.Matrix[rows, cols] == null ? " " : "[X]";
//                }
//            }
//            Console.Write(city.Matrix[rows, cols]);
//            city.Matrix[rows, cols] = null;
//        }
//        Console.WriteLine();
//    }

//    p.Movement(persons, city.Matrix);
//    Collide(cops, citizens, thieves);
//    Thread.Sleep(500);
//    //Console.ReadKey();
//    Console.Clear();
//}