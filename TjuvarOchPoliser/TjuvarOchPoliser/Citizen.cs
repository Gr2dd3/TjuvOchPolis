﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
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
}
