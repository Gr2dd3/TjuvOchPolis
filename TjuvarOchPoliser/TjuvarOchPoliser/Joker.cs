using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TjuvarOchPoliser
{
    internal class Joker : Hero
    {

        public Joker(Random random) : base(random)
        {
            Counter = 0;
            Color = ConsoleColor.Magenta;
            Name = "?";
        }

        public void TurnEvil(Citizen citizen, List<Person> people, Random random)
        {
            Thief thief = new(random);
            people.Remove(citizen);
            people.Add(thief);
        }
    }
}
