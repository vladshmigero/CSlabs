using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSlabs.Labs
{
    public class Lab3
    {
        public class Token
        {
            public string Word { get; private set; }
            public bool Isword { get; private set; }
            public Token (string word, bool isword)
            {
                this.Word = word;
                this.Isword = isword;
            }
            public override string ToString()
            {
                return this.Word;
            }
        }
        public class Sentence
        {
        }
        public class Text
        {

        }




        static void Main(string[] args)
        {
            string inputFile = @"C:\Users\user\source\repos\vladshmigero\CSlabs\CSlabs\CSlabs\TextFile.txt";
            string[] sequences = File.ReadAllLines(inputFile);
            foreach (string line in sequences)
            {
                Console.WriteLine(line);
            }
        }
    }
}
