using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Povtorenie
{
    class Killer : Robot
    {
        public int Level { get; set;}
        public Killer() { }
        public Killer(string name, double mass, byte[] cords, int level) : base(name, mass, cords)
        {
            this.Level = level;
            base.printValues();
        }
        public void Lazer()
        {
            Console.WriteLine("Lazer is shooting");
        }
    }
}
