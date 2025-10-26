using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        //short i = Convert.ToInt16(Console.ReadLine());
        //switch (i)
        //{
        //    case 1:
        //        Console.WriteLine("Number is 1");
        //        break;
        //    default:
        //        Console.WriteLine("Wrong number");
        //        break;
        //}
        //for (int i = 6; i > 0; i--)
        //{
        //    for(int j = 0; j < i; j++)
        //    {
        //        Console.Write("0 ");
        //    }
        //    Console.WriteLine();
        //}
        //int[] ints = new int[10];
        //ints[0] = 1;
        //string[] words = new string[] { "book", "bob", "duck" };
        //for (byte i = 0; i < words.Length; i++)
        //{
        //    Console.WriteLine(words[i]);
        //}
        //short[] numbers = new short[10];
        //Random rnd = new Random();
        //int sum = 0;
        //for (int i = 0; i < numbers.Length; i++)
        //{
        //    numbers[i] = Convert.ToInt16(rnd.Next(-100, 100));
        //    Console.Write(numbers[i]+ " ");
        //    sum += numbers[i];
        //}
        //Console.WriteLine("\n" + sum);
        //char[,] symbols = new char[2,3];
        //symbols[0,0] = 'a';
        //int[,] nums = new int[,] {
        //    {4,5,6},
        //    {1,2,3},
        //    {4,2,1}
        //};
        //List<int> nums = new List<int>()
        //{
        //   12, 4, 54, 6, 7, 8, 11, 10,
        //};
        //nums.Add(1);
        //nums.Remove(7);
        //nums.Sort();
        //nums.Reverse();
        //foreach (int x in nums)
        //{
        //    Console.WriteLine(x);
        //}
        //nums.Remove(7);
        //func("dds");
        //string words = "Hello world";
        //func(words);
        //byte[] nums = {1, 2, 3, 4, 5};
        //byte s = func(nums);
        //string word = "hello";
        //word += " world";
        //word = String.Concat(word, "!!");
        //Console.WriteLine(word);
        //Console.Write(String.Compare(word, "hello"));
        string people = "Alex,Josh,John,Bob";
        string[] names = people.Split(new char[] {','});//or people.Split(',');
        people = String.Join(" ", names);
        Console.WriteLine(people.Substring(0, people.Length - 1));
        //foreach (string name in names)
        //    Console.WriteLine(name);
    }

    //public static byte func(byte[] n)
    //{
    //    byte summ = 0;
    //    foreach (byte b in n)
    //    {
    //        summ += b;
    //    }
    //    return summ;
    //}
}