﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TjuvarOchPoliser
{
    internal class Person
    {
        public int Direction { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Name { get; set; }

        // Skapar en random siffra för Direction prop
        public Person()
        {
            Random rnd = new();

            XPos = rnd.Next(100);
            YPos = rnd.Next(25);

            Direction = rnd.Next(8);

            Name = "X";
        }

        //Kalla på för att fortsätta flytta person
        public virtual void Movement(List<Person> persons, Person[,] city)
        {
            foreach (Person person in persons)
            {
                switch (person.Direction)
                {
                    case 0:     // Neråt 
                        person.YPos++;
                        if (person.YPos == city.GetLength(0))
                        {
                            person.YPos = 0;
                        }
                        break;
                    case 1:     // Vänster ner 
                        person.YPos++; //ner
                        person.XPos--; //vänster
                        if (person.YPos == city.GetLength(0))
                        {
                            person.YPos = 0;
                            person.XPos += 1;
                        }
                        else if (person.XPos < 0)
                        {
                            person.XPos = city.GetLength(1) - 1;
                            person.YPos -= 1;

                        }
                        break;
                    case 2:     // Vänster 
                        person.XPos--;
                        if (person.XPos < 0)
                        {
                            person.XPos = city.GetLength(1) - 1;
                        }
                        break;
                    case 3:     // Uppåt
                        person.YPos--;
                        if (person.YPos < 0)
                        {
                            person.YPos = city.GetLength(0) - 1;
                        }
                        break;
                    case 4:     // Höger uppåt
                        person.YPos--;
                        person.XPos++;
                        if (person.YPos < 0)
                        {
                            person.YPos = city.GetLength(0) - 1;
                            person.XPos -= 1;
                        }
                        else if (person.XPos == city.GetLength(1))
                        {
                            person.XPos = 0;
                            person.YPos += 1;

                        }
                        break;
                    case 5:     // Höger
                        person.XPos++;
                        if (person.XPos == city.GetLength(1))
                        {
                            person.XPos = 0;
                        }
                        break;
                    case 6: // Höger nedåt
                        person.XPos++;
                        person.YPos++;
                        if (person.XPos == city.GetLength(1))
                        {
                            person.XPos = 0;
                            person.YPos -= 1;
                        }
                        else if (person.YPos == city.GetLength(0))
                        {
                            person.XPos -= 1;
                            person.YPos = 0;
                        }
                        break;
                    case 7: //Vänster uppåt
                        person.XPos--;
                        person.YPos--;
                        if (person.XPos < 0)
                        {
                            person.XPos = city.GetLength(1) - 1;
                            person.YPos += 1;
                        }
                        else if (person.YPos < 0)
                        {
                            person.XPos += 1;
                            person.YPos = city.GetLength(0) - 1;
                        }
                        break;
                }
            }
        }
    }

    internal class Citizen : Person
    {
        public List<Thing> Belongings { get; set; }
        //public List<Thing> belongings = new();
        public Citizen()
        {
            Belongings = new List<Thing>();
            Belongings.Add(new Phone());
            Belongings.Add(new Keys());
            Belongings.Add(new Watch());
            Belongings.Add(new Wallet());

            Name = "C";
        }
    }

    internal class Police : Person
    {
        public List<Thing> Seized { get; set; }
        public Police()
        {
            Seized = new List<Thing>();
            Name = "P";
        }

        public string Seize(Person person, Person[,] Matrix, int rows, int cols, string action)
        {
            
            Person personZ = new();
            // Police möter Thief
            if (person is Police)
            {

                if (((Thief)Matrix[rows, cols]).Loot.Count > 0)
                {

                    ((Police)person).Seized.AddRange(((Thief)Matrix[rows, cols]).Loot);
                    ((Thief)Matrix[rows, cols]).Loot.Clear();
                    Matrix[rows, cols] = personZ;
                    action = "Polis arresterar tjuv";
                  
                    //((Thief)Matrix[rows, cols]).HasLoot = false;
                    // GoToJail();
                }
            }
            // Thief möter Police
            if (person is Thief)
            {

                //Seize();
                if (((Thief)person).Loot.Count > 0)
                {
                    ((Police)Matrix[rows, cols]).Seized.AddRange(((Thief)person).Loot);
                    ((Thief)person).Loot.Clear();
                    Matrix[rows, cols] = personZ;
                    action = "Polis arresterar tjuv";
                    
                    // GoToJail();
                }
            }
            return action;
        }
    }

    internal class Thief : Person
    {
        public List<Thing> Loot { get; set; }
        public bool HasLoot { get; set; }
        public Thief()
        {
            Loot = new List<Thing>();
            Name = "T";
            HasLoot = false;
        }

        public string Rob(Person person, Person[,] Matrix, int rows, int cols, string action)
        {
            Random random = new();
            Person personX = new();
            

            // Citizen möter Thief
            if (person is Citizen)
            {

                //Rob();
                if (((Citizen)person).Belongings.Count > 0)
                {
                    int removeAtIndex = random.Next(((Citizen)person).Belongings.Count - 1);
                    ((Thief)Matrix[rows, cols]).Loot.Add(((Citizen)person).Belongings[removeAtIndex]);
                    ((Citizen)person).Belongings.RemoveAt(removeAtIndex);
                    Matrix[rows, cols] = personX;
                    HasLoot = true;
                    action = "Tjuv rånar medborgare";
                    

                }

            }
            // Thief möter Citizen
            if (person is Thief)
            {

                //Rob();
                if (((Citizen)Matrix[rows, cols]).Belongings.Count > 0)
                {
                    int removeAtIndex = random.Next(((Citizen)Matrix[rows, cols]).Belongings.Count - 1);
                    ((Thief)person).Loot.Add(((Citizen)Matrix[rows, cols]).Belongings[removeAtIndex]);
                    ((Citizen)Matrix[rows, cols]).Belongings.RemoveAt(removeAtIndex);
                    Matrix[rows, cols] = personX;
                    HasLoot = true;
                    action = "Tjuv rånar medborgare";
                    
                }

            }
            return action;

        }
    }
}
