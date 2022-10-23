using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Thief : Person
    {
        public List<Thing> Loot { get; set; }
        public int Sentenced { get; set; }
        public bool IsArrested { get; set; }

        public Thief(Random random) : base(random)
        {
            Loot = new List<Thing>();
            IsArrested = false;
            Color = ConsoleColor.Red;
            Name = "T";
        }

        public void Rob(Citizen citizen)
        {
            Random random = new();
            int removeAtIndex = random.Next(citizen.Belongings.Count - 1);

            Loot.Add(citizen.Belongings[removeAtIndex]);
            citizen.Belongings.RemoveAt(removeAtIndex);
        }
    }
}
