using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Hero : Person
    {
        public int Counter { get; set; }
        
        public Hero(Random random) : base(random)
        {
            Counter = 0;
        }

        internal void HeroSwitch(List<Person> people, Hero hero, Batman batman)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case 'b':
                    batman.DeployBatman(people, batman);
                    hero.Counter = 0;
                    break;

                default:
                    break;

            }
        }

    }
}



