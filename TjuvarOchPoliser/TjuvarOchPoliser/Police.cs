using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TjuvarOchPoliser
{
    internal class Police : Person
    {
        public List<Thing> Seized { get; set; }
        public Police(Random random) : base(random)
        {
            Seized = new List<Thing>();
            Color = ConsoleColor.Blue;
            Name = "P";
        }

        internal void Seize(Thief thief)
        {
            thief.Sentenced = thief.Loot.Count * 30;
            Seized.AddRange(thief.Loot);
            thief.Loot.Clear();
            thief.IsArrested = true;
        }
    }
}
