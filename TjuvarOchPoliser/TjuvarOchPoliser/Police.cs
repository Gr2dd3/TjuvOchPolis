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
        public Police()
        {
            Seized = new List<Thing>();
            Name = "P";
        }

        public string Seize(Person person, Person[,] matrix, int rows, int cols, string action, List<Person> persons, Prison prison)
        {
            Random rnd = new Random();

            // Police möter Thief
            if (person is Police)
            {
                if (((Thief)matrix[rows, cols]).Loot.Count > 0)
                {
                    int items = ((Thief)matrix[rows, cols]).Loot.Count;
                    ((Thief)matrix[rows, cols]).PrisonCount = items * 15;
                    ((Thief)matrix[rows, cols]).XPos = rnd.Next(prison.Matrix.GetLength(1));
                    ((Thief)matrix[rows, cols]).YPos = rnd.Next(prison.Matrix.GetLength(0));

                    ((Police)person).Seized.AddRange(((Thief)matrix[rows, cols]).Loot);
                    ((Thief)matrix[rows, cols]).Loot.Clear();
                    action = "Police arrests thief";

                    // GoToJail();
                    prison.Prisoners.Add((Thief)matrix[rows, cols]);
                    persons.Remove((Thief)matrix[rows, cols]);
                }
            }
            // Thief möter Police
            if (person is Thief)
            {
                if (((Thief)person).Loot.Count > 0)
                {
                    int items = ((Thief)person).Loot.Count;

                    ((Police)matrix[rows, cols]).Seized.AddRange(((Thief)person).Loot);
                    ((Thief)person).Loot.Clear();
                    action = "Police arrests thief";

                    // GoToJail(items);
                    ((Thief)person).PrisonCount = items * 30;
                    ((Thief)person).XPos = rnd.Next(prison.Matrix.GetLength(1));
                    ((Thief)person).YPos = rnd.Next(prison.Matrix.GetLength(0));

                    prison.Prisoners.Add((Thief)person);
                    persons.Remove((Thief)person);
                }
            }
            return action;
        }
    }
}
