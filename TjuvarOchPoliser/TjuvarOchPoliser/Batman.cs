using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TjuvarOchPoliser
{
    internal class Batman : Hero
    {
        public List<Thing> UtilityBelt { get; set; }

        public Batman(Random random) : base(random)
        {
            UtilityBelt = new List<Thing>();
            Counter = 0;
            
            Color = ConsoleColor.DarkGray;
            Name = "B";
        }

        public string KaPow(Thief thief, List<Person> people)
        {
            string action = "";
            if (thief.Loot.Count > 3)
            {
                action = "KaPOW! Thief dies and Batman adds the stuff to his Utility Belt!";
                people.Remove(thief);
            }
            else
            {
                action = "Batman roughs the thief up a bit!! Oh, and he adds the stuff to his Utility Belt!!";
            }

            UtilityBelt.AddRange(thief.Loot);
            thief.Loot.Clear();

            return action;
        }

    }
}

