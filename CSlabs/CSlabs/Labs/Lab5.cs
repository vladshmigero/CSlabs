using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSlabs.Labs
{
    public class lab5
    {
        public abstract class Sladost
        {
            public string Name { get; set; }
            public double Weight { get; set; }
            public int Sugar { get; set; }
            public string Type { get; set; }
            public abstract string GetInfo();
        }
        public class candy : Sladost
        {
            public string Vkys { get; set; }
            public override string GetInfo()
            {
                return $"Название: {Name} Тип: {Type} Вкус: {Vkys} Вес: {Weight} Содержание сахара: {Sugar}";
            }
        }
        public class Gift
        {
            private List<Sladost> sladosti = new List<Sladost>();
        }
        static void Main(string[] args)
        {

        }
    }
}
