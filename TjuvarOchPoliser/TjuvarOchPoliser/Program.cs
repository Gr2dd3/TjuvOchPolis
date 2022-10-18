using System.ComponentModel;
using TjuvarOchPoliser;



internal class Program
{
    private static void Main(string[] args)
    {
        City city = new();
        List<Person> persons = new();
        Person person1 = new Person();


        for (int i = 0; i < 10; i++)
        {
            Police police = new();
            persons.Add(police);
        }


        while (true)
        {
            for (int rows = 0; rows < city.Matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < city.Matrix.GetLength(1); cols++)
                {
                    foreach (Person person in persons)
                    {
                        if (rows == person.Ypos && cols == person.Xpos)
                        {
                            city.Matrix[rows, cols] = person.Name;
                        }
                        else
                        {
                            city.Matrix[rows, cols] = city.Matrix[rows, cols] == null ? " " : city.Matrix[rows, cols];
                        }
                    }
                    Console.Write(city.Matrix[rows, cols]);
                    city.Matrix[rows, cols] = null;
                }
                Console.WriteLine();
            }
            person1.Movement(persons, city.Matrix);
            Thread.Sleep(500);
            //Console.ReadKey();
            Console.Clear();
        }
    }
}