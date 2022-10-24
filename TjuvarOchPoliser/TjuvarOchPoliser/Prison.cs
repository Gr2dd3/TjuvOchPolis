using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvarOchPoliser
{
    internal class Prison
    {
        public List<Thief> Prisoners { get; set; }
        public int PrisonCounter { get; set; }
        public Person[,] Matrix { get; set; }

        public Prison()
        {
            Matrix = new Person[10, 20];
            Prisoners = new List<Thief>();
        }

        public void PutThievesInPrison(List<Person> people, List<Thief> thieves)
        {
            Random random = new();
            foreach (var thief in thieves)
            {
                thief.IsArrested = false;
                thief.XPos = random.Next(Matrix.GetLength(1));
                thief.YPos = random.Next(Matrix.GetLength(0));

                Prisoners.Add(thief);
                people.Remove(thief);
            }
        }

        internal void UpdatePrisonMatrix()
        {
            Random random = new();
            foreach (var prisoner in Prisoners)
            {
                while (Matrix[prisoner.YPos, prisoner.XPos] is not null)
                {
                    prisoner.XPos = random.Next(Matrix.GetLength(1));
                    prisoner.YPos = random.Next(Matrix.GetLength(0));
                }
                Matrix[prisoner.YPos, prisoner.XPos] = prisoner;
            }
        }

        internal List<Thief> GetReleasedPrisoners()
        {
            List<Thief> thieves = new();

            for (int i = 0; i < Prisoners.Count; i++)
            {
                Prisoners[i].Sentenced--;
                if (Prisoners[i].Sentenced is 0)
                {
                    thieves.Add(Prisoners[i]);
                    Prisoners.Remove(Prisoners[i]);
                }
            }
            return thieves;
        }
    }
}
