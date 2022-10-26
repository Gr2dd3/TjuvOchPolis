using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace TjuvarOchPoliser
{
    internal class Joker : Hero
    {
        public Joker(Random random) : base(random)
        {
            Counter = 0;
            Color = ConsoleColor.DarkMagenta;
            Name = "?";
        }

        public string TurnEvil(Citizen citizen, Thief thief, List<Person> people)
        {
            string action = "The Joker just turned a citizen into a thief!";
            
            people.Remove(citizen);
            people.Add(thief);

            return action;
        }
    }
}
