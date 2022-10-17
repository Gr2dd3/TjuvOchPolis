using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Person
    {
        public List<Thing> Inventory { get; set; }
        public int Direction { get; set; }

        public int Xpos { get; set; }
        public int Ypos { get; set; }


        //Kalla på för att fortsätta flytta person
        public virtual List<Person> Movement(List<Person> persons)
        {
            return persons;
        }

        // Creates a random number for Direction property
        // Skapar en random siffra för Direction prop
        public Person()
        {
            Random rnd = new();

            Xpos = rnd.Next(100);
            Ypos = rnd.Next(25);

            Direction = rnd.Next(6);
        }
    }


    internal class Citizen : Person
    {
        public List<string> Belongings { get; set; }

        public override List<Person> Movement(List<Person> persons)
        {
            foreach (Person person in persons)
            {
                switch (person.Direction)
                {
                    case 0:
                        person.Ypos++;
                        break;
                    case 1:
                        person.Ypos++;
                        person.Xpos--;
                        break;
                    case 2:
                        person.Xpos--;
                        break;
                    case 3:
                        person.Ypos--;
                        break;
                    case 4:
                        person.Ypos--;
                        person.Xpos++;
                        break;
                    case 5:
                        person.Xpos++;
                        break;
                }
            }
            return persons;
        }
    }



    internal class Police : Person
    {
        public List<Thing> SeizedGoods { get; set; }

        public override List<Person> Movement(List<Person> polices)
        {
            foreach (Police police in polices)
            {
                switch (police.Direction)
                {
                    case 0:
                        police.Ypos++;
                        break;
                    case 1:
                        police.Ypos++;
                        police.Xpos--;
                        break;
                    case 2:
                        police.Xpos--;
                        break;
                    case 3:
                        police.Ypos--;
                        break;
                    case 4:
                        police.Ypos--;
                        police.Xpos++;
                        break;
                    case 5:
                        police.Xpos++;
                        break;
                }
            }
            return polices;
        }
    }



    internal class Thief : Person
    {
        public List<Thing> StolenGoods { get; set; }

        public override List<Person> Movement(List<Person> persons)
        {
            foreach (Person person in persons)
            {
                switch (person.Direction)
                {
                    case 0:
                        person.Ypos++;
                        break;
                    case 1:
                        person.Ypos++;
                        person.Xpos--;
                        break;
                    case 2:
                        person.Xpos--;
                        break;
                    case 3:
                        person.Ypos--;
                        break;
                    case 4:
                        person.Ypos--;
                        person.Xpos++;
                        break;
                    case 5:
                        person.Xpos++;
                        break;
                }
            }
            return persons;
        }
    }


}
