using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Lab1
{
     public struct GeneticData
    {
        public string protein;      
        public string organism;     
        public string amino_acids;  
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

    private static bool isEncoded(string s)
    {
        var mas = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        foreach (var m in mas)
        {
            if (s.Contains(m))
            {
                return true;
            }
        }
        return false;
    }
    private static bool IsValid(string a)
    {
        var mas = new[] { 'B', 'J', 'O', 'U', 'X', 'Z' };
        foreach (var c in mas)
        {
            if (a.Contains(c)) {  return false; } 
        }
        return true;
    }


    public static string ProcessSearch(List<GeneticData> proteins, string sub)
    {
        var sb = new StringBuilder();
        if (isEncoded(sub))
        {
            sub = RLDecoding(sub);
        }
            sb.AppendLine(sub + " ");
            sb.AppendLine("organism\t\t\tprotein");

            var found = proteins.FirstOrDefault(p => p.amino_acids.Contains(sub));
            if (found.protein == null)
                sb.AppendLine("NOT FOUND");
            else
                sb.AppendLine(found.organism + "\t\t" + found.protein);

            return sb.ToString();
      
    }

    public static Mode(List<GeneticData> amino_acids)
    {

    }

    public static void Main()
    {
        string seqFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\sequences.txt";
        string cmdFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\commands.txt";
        string outFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\genedata.txt";

        string[] sequences = File.ReadAllLines(seqFile);
        string[] commands = File.ReadAllLines(cmdFile);

        var proteins = new List<GeneticData>();
        foreach (string line in sequences)
        {
            string[] parts = line.Split('\t');
            if (parts.Length >= 3)
            {
                proteins.Add(new GeneticData
                {
                    protein = parts[0],
                    organism = parts[1],
                    amino_acids = parts[2]
                });
            }
        }

        var sb = new StringBuilder();
        sb.AppendLine("Vlad Shmigero\nGenetic Searching");
        sb.AppendLine("--------------------------------------------------------------------------");

        int num = 1;
        foreach (string cmd in commands)
        {
            string[] parts = cmd.Split('\t');
            string action = parts[0];
            sb.Append(num.ToString("D3")).Append("   ").Append(action).Append("   ");

            if (action == "search")
            {
                string encodedSub = parts[1];
                sb.Append(ProcessSearch(proteins, encodedSub));
            }
            else
            {
                sb.AppendLine("UNKNOWN");
            }

            sb.AppendLine("--------------------------------------------------------------------------");
            num++;
        }

        File.WriteAllText(outFile, sb.ToString());
        Console.WriteLine("Готово! Результат в genedata.txt");
    }
}
