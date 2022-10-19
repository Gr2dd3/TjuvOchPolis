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

            Citizen citizen = new();
            persons.Add(citizen);

            Thief thief = new();
            persons.Add(thief);
        }
        int counter = 1;
        foreach (Person person in persons)
        {
            Console.Write("Person " + counter + ": " + person.GetType().Name + " - " + person.Xpos + "," + person.Ypos + " : ");
            if (person is Citizen)
            {
                for(int i = 0; i < ((Citizen)person).belongings.Count; i++)
                    Console.Write(((Citizen)person).belongings[i]);
            }
            Console.WriteLine();
            counter++;
        }


    }

    private static void Collide(List<Person> cops, List<Person> citizens, List<Person> thieves)
    {
        foreach (var cop in cops)
        {
            foreach (var thief in thieves)
            {
                if (cop.Xpos == thief.Xpos && cop.Ypos == thief.Ypos)
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
        //                    city.Matrix[rows, cols] = city.Matrix[rows, cols] == null ? " " : city.Matrix[rows, cols];
        //                }
        //            }
        //            Console.Write(city.Matrix[rows, cols]);
        //            city.Matrix[rows, cols] = null;
        //        }
        //        Console.WriteLine();
        //    }

        //    Collide(cops, citizens, thieves);
        //    p.Movement(persons, city.Matrix);
        //    Thread.Sleep(500);
        //    //Console.ReadKey();
        //    Console.Clear();
        //}