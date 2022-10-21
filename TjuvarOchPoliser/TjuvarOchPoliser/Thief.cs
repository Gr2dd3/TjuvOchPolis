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
        public int PrisonCount { get; set; }

        public Thief()
        {
            Loot = new List<Thing>();
            Name = "T";
        }

        public string Rob(Person person, Person[,] matrix, int rows, int cols, string action)
        {
            Random random = new();

            // Citizen möter Thief
            if (person is Citizen)
            {
                if (((Citizen)matrix[rows, cols]).Belongings.Count > 0)
                {
                    int removeAtIndex = random.Next(((Citizen)person).Belongings.Count - 1);
                    ((Thief)matrix[rows, cols]).Loot.Add(((Citizen)person).Belongings[removeAtIndex]);
                    ((Citizen)person).Belongings.RemoveAt(removeAtIndex);
                    action = "Citizen gets robbed by thief";
                }
            }

            // Thief möter Citizen
            if (person is Thief)
            {
                if (((Citizen)matrix[rows, cols]).Belongings.Count > 0)
                {
                    int removeAtIndex = random.Next(((Citizen)matrix[rows, cols]).Belongings.Count - 1);
                    ((Thief)person).Loot.Add(((Citizen)matrix[rows, cols]).Belongings[removeAtIndex]);
                    ((Citizen)matrix[rows, cols]).Belongings.RemoveAt(removeAtIndex);
                    action = "Citizen gets robbed by thief";
                }
            }
            return action;
        }
    }
}
