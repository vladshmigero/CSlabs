using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Povtorenie
{
    class Robot
    {
        public static int count;
        private int ModelNumber;
        private string name;
        private double mass;
        private byte[] cords;
        
        public double Mass
        {
            get
            {
                Console.WriteLine("Результат: ");
                return this.mass;
            }
            set
            {
                this.mass = value;
            }
        }

        public int Width { get; set; }


        public Robot(string name, double mass, byte[] cords)
        {
            Console.Write("Обьект был создан:\n");
            this.setValues(name,mass, cords);
        }
        public Robot() { }
        public void setValues(string name, double mass, byte[] cords)
        {
            this.name = name;
            this.mass = mass;
            this.cords = cords;
        }

        public void printValues()
        {
            Console.Write("Name: " + name + ", Mass: " + mass + "kg" + ", Coordinates: ");
            byte i = 1;
            foreach (byte item in cords)
            {
                Console.Write(item);
                if (i<cords.Length) Console.Write(".");
                i++;
            }
            Console.WriteLine();
        }
        public static void print()
        {
            Console.WriteLine("---------------------------------------------");
        }
    }
}
