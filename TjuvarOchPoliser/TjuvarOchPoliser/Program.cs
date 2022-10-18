using TjuvarOchPoliser;



internal class Program
{
    private static void Main(string[] args)
    {
        City city = new();
        string[,] bigCity = new string[25, 100];
        List<Person> persons = new();
        Police police = new();
        persons.Add(police);

        while (true)
        {
            for (int rows = 0; rows < bigCity.GetLength(0); rows++)
            {
                for (int cols = 0; cols < bigCity.GetLength(1); cols++)
                {
                    if (rows == police.Ypos && cols == police.Xpos)
                    {
                        bigCity[rows, cols] = police.Name;
                    }
                    else
                    {
                        bigCity[rows, cols] = ".";

                    }
                    Console.Write(bigCity[rows, cols]);
                }
                Console.WriteLine();
            }
            police.Movement(persons);
            Console.ReadKey();
            Console.Clear();
        }
    }
}