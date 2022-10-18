using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Person
    {
        public int Direction { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public string Name { get; set; }

        // Creates a random number for Direction property
        // Skapar en random siffra för Direction prop
        public Person()
        {
            Random rnd = new();

            Xpos = rnd.Next(100);
            Ypos = rnd.Next(25);

            Direction = rnd.Next(8);
        }

        //Kalla på för att fortsätta flytta person
        public virtual void Movement(List<Person> persons, string[,] city)
        {
            foreach (Person person in persons)
            {
                switch (person.Direction)
                {
                    case 0:     // Neråt 
                        person.Ypos++;
                        if (person.Ypos == city.GetLength(0))
                        {
                            person.Ypos = 0;
                        }
                        break;
                    case 1:     // Vänster ner 
                        person.Ypos++; //ner
                        person.Xpos--; //vänster
                        if (person.Ypos == city.GetLength(0))
                        {
                            person.Ypos = 0;
                            person.Xpos += 1;
                        }
                        else if (person.Xpos < 0)
                        {
                            person.Xpos = city.GetLength(1) - 1;
                            person.Ypos -= 1;

                        }
                        break;
                    case 2:     // Vänster 
                        person.Xpos--;
                        if (person.Xpos < 0)
                        {
                            person.Xpos = city.GetLength(1) - 1;
                        }
                        break;
                    case 3:     // Uppåt
                        person.Ypos--;
                        if (person.Ypos < 0)
                        {
                            person.Ypos = city.GetLength(0) - 1;
                        }
                        break;
                    case 4:     // Höger uppåt
                        person.Ypos--;
                        person.Xpos++; 
                        if (person.Ypos < 0)
                        {
                            person.Ypos = city.GetLength(0) - 1;
                            person.Xpos -= 1;
                        }
                        else if (person.Xpos == city.GetLength(1))
                        {
                            person.Xpos = 0;
                            person.Ypos += 1;

                        }
                        break;
                    case 5:     // Höger
                        person.Xpos++;
                        if (person.Xpos == city.GetLength(1))
                        {
                            person.Xpos = 0;
                        }
                        break;
                    case 6: // Höger nedåt
                        person.Xpos++;
                        person.Ypos++;
                        if (person.Xpos == city.GetLength(1))
                        {
                            person.Xpos = 0;
                            person.Ypos -= 1;
                        }
                        else if (person.Ypos == city.GetLength(0))
                        {
                            person.Xpos -= 1;
                            person.Ypos = 0;
                        }
                        break;
                    case 7: //Vänster uppåt
                        person.Xpos--;
                        person.Ypos--;
                        if (person.Xpos < 0)
                        {
                            person.Xpos = city.GetLength(1) - 1;
                            person.Ypos += 1;
                        }
                        else if (person.Ypos < 0)
                        {
                            person.Xpos += 1;
                            person.Ypos = city.GetLength(0) - 1;
                        }
                        break;
                }
            }
        }
    }


    internal class Citizen : Person
    {
        public List<string> Belongings { get; set; }
        public Citizen()
        {
            Belongings = new List<string>();
            Thing things = new(Belongings);
            Name = "M";
        }
    }



    internal class Police : Person
    {
        public List<string> SeizedGoods { get; set; }
        public Police()
        {
            Name = "P";
        }
    }



    internal class Thief : Person
    {
        public List<string> MyProperty { get; set; }
        public List<string> StolenGoods { get; set; }
        public Thief()
        {
            Name = "T";
        }
    }
}
