using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSlabs.Labs
{
    public class lab5
    {
        public abstract class Sladost : IComparable<Sladost>
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
            public int CompareTo(Sladost other)
            {
                return Weight.CompareTo(other.Weight);
            }
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
                return $"Название сладости: {Name}, Вкус: {Vkys}, Вес: {Weight}г, Содержание сахара: {Sugar}г.";
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
                return $"Название сладости: {Name}, Вес: {Weight}г, Содержание сахара: {Sugar}г, C орешками?: {(WithNuts ? "да" : "нет")}, Содержание какао: {CocoaProcent}%.";
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
                return $"Название сладости: {Name}, Вес: {Weight}г, Содержание сахара: {Sugar}г, Цвет: {Color}.";
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
                Console.WriteLine("----------Состав подарка-----------------------------------------");
                foreach (var s in sladosti)
                {
                    Console.WriteLine(s.GetInfo());
                }
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine($"Общий вес подарка: {GetWeight()} г");
            }
            public void SortByWeight()
            {
                sladosti.Sort();
            }
            public void SortBySugar() 
            { 
                sladosti.Sort(new SugarComparer());
            }
            public List<Sladost> PoiskKonfet(int min, int max)
            {
                List<Sladost> result = new List<Sladost>();
                foreach (var s in sladosti)
                {
                    if (s.Sugar >= min && s.Sugar <= max)
                    {
                        result.Add(s);
                    }
                }
                return result;
            }

        }
        public class SugarComparer : IComparer<Sladost> 
        { 
            public int Compare(Sladost x, Sladost y) 
            { 
                return x.Sugar.CompareTo(y.Sugar);
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
            gift.Add(new Marshmallow("Marshmallow", 100, 30, "Белый"));
            Console.WriteLine("До сортировки:");
            gift.ShowInfo();
            Console.WriteLine("\nПосле сортировки по весу:"); 
            gift.SortByWeight(); 
            gift.ShowInfo();
            Console.WriteLine("\nПосле сортировки по сахару:");
            gift.SortBySugar();
            gift.ShowInfo();
            Console.WriteLine("\nВведите диапазон содержания сахара, для поиска конфет:\nОт:");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("До:");
            int y = int.Parse(Console.ReadLine());
            List<Sladost> foundList = gift.PoiskKonfet(x, y);
            if (foundList.Count > 0) 
            {
                Console.WriteLine("Найдены сладости:");
                foreach (var sladost in foundList) 
                { 
                    Console.WriteLine(sladost.GetInfo()); 
                } 
            }
            else 
            { Console.WriteLine("В указанном диапазоне сладостей не найдено."); }
        }
    }
}
