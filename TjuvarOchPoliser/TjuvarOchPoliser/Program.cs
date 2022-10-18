using TjuvarOchPoliser;



internal class Program
{
    private static void Main(string[] args)
    {
        City city = new();
        List<Person> persons = new();
        Police police = new();
        persons.Add(police);

        while (true)
        {
            for (int rows = 0; rows < city.Matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < city.Matrix.GetLength(1); cols++)
                {
                    if (rows == police.Ypos && cols == police.Xpos)
                    {
                        city.Matrix[rows, cols] = police.Name;
                    }
                    else
                    {
                        city.Matrix[rows, cols] = ".";

                    }
                    Console.Write(city.Matrix[rows, cols]);
                }
                Console.WriteLine();
            }
            police.Movement(persons, city.Matrix);
            Console.ReadKey();
            Console.Clear();
        }
    }
}