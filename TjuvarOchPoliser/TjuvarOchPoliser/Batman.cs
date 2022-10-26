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

        internal void KaPow(Thief thief)
        {
            thief.Sentenced = thief.Loot.Count * 30;
            UtilityBelt.AddRange(thief.Loot);
            thief.Loot.Clear();
            thief.IsArrested = true;
        }

        internal void DeployBatman(List<Person> people, Batman batman)
        {
            people.Add(batman);
        }
    }
}

