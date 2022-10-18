using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Thing
    {
        public string Phone { get; set; }
        public string Keys { get; set; }
        public string Money { get; set; }
        public string Watch { get; set; }
        public Thing(List<string> inventory)
        {
            Phone = "Phone";
            Keys = "Keys";
            Money = "Money";
            Watch = "Watch";
            inventory.Add(Phone);
            inventory.Add(Keys);
            inventory.Add(Money);
            inventory.Add(Watch);
        }

    }
}
