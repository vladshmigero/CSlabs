using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

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
        public class Donut : Sladost
        {
            public string Glaze { get; set; }

            public Donut(string name, double weight, int sugar, string glaze) : base(name, weight, sugar)
            {
                Glaze = glaze;
            }

            public override string GetInfo()
            {
                return $"Название сладости: {Name}, Вес: {Weight}г, Сахар: {Sugar}г, Глазурь: {Glaze}.";
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
            public void LoadFromFile(string filePath)
            {
                string json = File.ReadAllText(filePath);
                var items = JsonSerializer.Deserialize<List<Dto>>(json);

                foreach (var item in items)
                {
                    switch (item.Type)
                    {
                        case "Candy":
                            Add(new Candy(item.Name, item.Weight, item.Sugar, item.Vkys));
                            break;
                        case "Chocolate":
                            Add(new Chocolate(item.Name, item.Weight, item.Sugar, item.CocoaProcent, item.WithNuts));
                            break;
                        case "Marshmallow":
                            Add(new Marshmallow(item.Name, item.Weight, item.Sugar, item.Color));
                            break;
                        case "Donut":
                            Add(new Donut(item.Name, item.Weight, item.Sugar, item.Glaze)); 
                            break;
                    }
                }
            }

        }
        public class Dto
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public double Weight { get; set; }
            public int Sugar { get; set; }
            public string Vkys { get; set; }
            public int CocoaProcent { get; set; }
            public bool WithNuts { get; set; }
            public string Color { get; set; }
            public string Glaze { get; set; }
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
            gift.LoadFromFile(@"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\Labs\Sladosti.json");
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
