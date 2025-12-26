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

            protected Sladost(string name, double weight, int sugar)
            {
                Name = name;
                Weight = weight;
                Sugar = sugar;
            }
            public abstract string GetInfo();
        }
        public class Candy : Sladost
        {
            public string Vkys { get; set; }
            public Candy(string name, double weight, int sugar, string vkys) : base(name, weight, sugar)
            {
                Vkys = vkys;
            }
            public override string GetInfo()
            {
                return $"Название сладости: {Name}, Вкус: {Vkys}, Вес: {Weight}, Содержание сахара: {Sugar}.";
            }
        }
        
        public class Chocolate : Sladost
        {
            public bool WithNuts { get; set; }
            public int CocoaProcent { get; set; }
            public Chocolate(string name, double weight, int sugar, int cocoa, bool with) : base(name, weight, sugar)
            {
                CocoaProcent = cocoa;
                WithNuts = with;
            }
            public override string GetInfo()
            {
                return $"Название сладости: {Name}, Вес: {Weight}, Содержание сахара: {Sugar}, C орешками?: {(WithNuts ? "да" : "нет")}, Содержание какао: {CocoaProcent}%.";
            }
        }
        public class Marshmallow : Sladost
        {
            public string Color { get; set; }
            public Marshmallow(string name, double weight, int sugar, string color) : base(name, weight, sugar)
            {
                Color = color;
            }
            public override string GetInfo()
            {
                return $"Название сладости: {Name}, Вес: {Weight}, Содержание сахара: {Sugar}, Цвет: {Color}.";
            }
        }
        public class Gift
        {
            private List<Sladost> sladosti = new List<Sladost>();
            public void Add(Sladost s) { sladosti.Add(s); }
            public double GetWeight()
            {
                double weight = 0;
                foreach (var s in sladosti)
                {
                    weight += s.Weight;
                }
                return weight;
            }
            public void ShowInfo()
            {
                Console.WriteLine("Состав подарка:");
                foreach (var s in sladosti)
                {
                   Console.WriteLine(s.GetInfo());
                }
                Console.WriteLine($"Общий вес подарка: {GetWeight()} г");
            }
        }
        static void Main(string[] args)
        {
            Gift gift = new Gift();
            gift.Add(new Candy("Карамелька", 50, 30, "Клубничный"));
            gift.Add(new Candy("Барбариска", 40, 20, "Кислый"));
            gift.Add(new Candy("Леденец", 100, 60, "Кокакола"));
            gift.Add(new Chocolate("Аленка", 100, 40, 70, true));
            gift.Add(new Chocolate("Камунарка", 90, 35, 75, false));
            gift.Add(new Chocolate("Казахстан", 100, 40, 70, false));
            gift.Add(new Marshmallow("Зефирки", 60, 20, "Розовый"));
            gift.Add(new Marshmallow("Зефир", 200, 70, "Белый"));
            gift.Add(new Marshmallow("Marshmallow", 100, 30, "Белый"));
            gift.ShowInfo();
        }
    }
}
