using System;
using System.ComponentModel;
using TjuvarOchPoliser;



internal class Program
{
    private static void Main(string[] args)
    {
        City city = new();
        city.CityRun();
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