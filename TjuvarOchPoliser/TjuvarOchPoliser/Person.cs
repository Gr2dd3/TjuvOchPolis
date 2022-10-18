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

            Direction = rnd.Next(6);
        }

        //Kalla på för att fortsätta flytta person
        public virtual void Movement(List<Person> persons, string[,]city)
        {
            foreach (Person person in persons)
            {
                switch (person.Direction)
                {
                    case 0:     // Neråt 
                        person.Ypos++;
                        if (person.Ypos >= city.GetLength(0))
                        {
                            person.Ypos = 0;
                        }
                        break;
                    case 1:     // Vänster ner 
                        person.Ypos++;
                        person.Xpos--;
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
                        break;
                    case 5:     // Höger
                        person.Xpos++;
                        if (person.Xpos >= city.GetLength(1))
                        {
                            person.Xpos = 0;
                        }
                        break;
                }
            }
        }


    }


    internal class Citizen : Person
    {
        public List<string> Belongings { get; set; }

        //public override List<Person> Movement(List<Person> persons)
        //{
        //    foreach (Person person in persons)
        //    {
        //        switch (person.Direction)
        //        {
        //            case 0:
        //                person.Ypos++;
        //                break;
        //            case 1:
        //                person.Ypos++;
        //                person.Xpos--;
        //                break;
        //            case 2:
        //                person.Xpos--;
        //                break;
        //            case 3:
        //                person.Ypos--;
        //                break;
        //            case 4:
        //                person.Ypos--;
        //                person.Xpos++;
        //                break;
        //            case 5:
        //                person.Xpos++;
        //                break;
        //        }
        //    }
        //    return persons;
        //}
    }



    internal class Police : Person
    {
        public List<Thing> SeizedGoods { get; set; }
        public Police()
        {
            Name = "P";
        }

        //public override List<Person> Movement(List<Person> polices)
        //{
        //    foreach (Police police in polices)
        //    {
        //        switch (police.Direction)
        //        {
        //            case 0:
        //                police.Ypos++;
        //                break;
        //            case 1:
        //                police.Ypos++;
        //                police.Xpos--;
        //                break;
        //            case 2:
        //                police.Xpos--;
        //                break;
        //            case 3:
        //                police.Ypos--;
        //                break;
        //            case 4:
        //                police.Ypos--;
        //                police.Xpos++;
        //                break;
        //            case 5:
        //                police.Xpos++;
        //                break;
        //        }
        //    }
        //    return polices;
        //}
    }



    internal class Thief : Person
    {
        public List<Thing> StolenGoods { get; set; }

        //public override List<Person> Movement(List<Person> persons)
        //{
        //    foreach (Person person in persons)
        //    {
        //        switch (person.Direction)
        //        {
        //            case 0:
        //                person.Ypos++;
        //                break;
        //            case 1:
        //                person.Ypos++;
        //                person.Xpos--;
        //                break;
        //            case 2:
        //                person.Xpos--;
        //                break;
        //            case 3:
        //                person.Ypos--;
        //                break;
        //            case 4:
        //                person.Ypos--;
        //                person.Xpos++;
        //                break;
        //            case 5:
        //                person.Xpos++;
        //                break;
        //        }
        //    }
        //    return persons;
        //}
    }


}
