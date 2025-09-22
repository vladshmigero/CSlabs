using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Lab1
{
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
    public static void Main()
    {

        string seqFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\sequences.txt";
        string cmdFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\commands.txt";
        string outFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\genedata.txt";


        string[] sequences = File.ReadAllLines(seqFile);
        string[] commands = File.ReadAllLines(cmdFile);

        var proteins = new List<(string Protein, string Organism, string Acids)>();
        foreach (string line in sequences)
        {
            string[] parts = line.Split('\t');
            if (parts.Length >= 3)
            {
                proteins.Add((parts[0], parts[1], parts[2]));
            }
        }

        var sb = new StringBuilder();
        sb.AppendLine("Vlad Shmigero\nGenetic Searching");
        sb.AppendLine("--------------------------------------------------------------------------");

        File.WriteAllText(outFile, sb.ToString());
        Console.WriteLine("Готово! Результат в genedata.txt");
    }

}
