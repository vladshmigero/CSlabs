using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Program;
public class Lab1()
{
    public static void Main()
    {
        Console.WriteLine("Введите последовательность");
        string s = Console.ReadLine()!;
        Console.WriteLine("Что вы хотите сделать со строкой?\n" +
            " 1: Закодировать\n" +
            " 2: Декодировать ");
        byte a = byte.Parse(Console.ReadLine());
        if (a == 2)
        {
            string decodedSequence = RLDecoding(s);
            Console.WriteLine($"Раскодированная последовательность: {decodedSequence}");
        }
        else if (a == 1)
        {
            string encodedSequence = RLEncoding(s);
            Console.WriteLine($"Закодированная последовательность: {encodedSequence}");

        }
        else
        {
            Console.WriteLine("Неправильный ввод!");
        }
    }

    public static string RLEncoding(string s)
    {
        StringBuilder sb = new();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            int end = i;
            while (end < s.Length && s[i] == s[end])
            {
                end++;
            }
            int length = end - i;
            if (length > 2)
            {
                i += length - 1;
                sb.Append(length);
            }
            sb.Append(c);
        }

        return sb.ToString();
    }

    public static string RLDecoding(string s)
    {
        StringBuilder sb = new();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (char.IsDigit(c))
            {
                int n = c - '0';
                sb.Append(s[i + 1], n);
                i++;
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

}


